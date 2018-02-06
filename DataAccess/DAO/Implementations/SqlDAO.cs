using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using log4net;
using RedArbor.Data.Entities;
using System.Reflection;

namespace RedArbor.Data.DAO
{
    /// <summary>
    /// Objeto genérico de acceso a datos en SQL para entidades mantenidas por la API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlDAO<T> where T : class
    {
        string _connectionString;
        string _table;
        string _idField;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Constructor con la cadena de conexión, y datos particulares de propios de la tabla de la entidad
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos</param>
        /// <param name="table">Nombre de la tabla</param>
        /// <param name="idField">Nombre del campo de clave primaria</param>
        protected SqlDAO(string connectionString, string table, string idField)
        {
            _connectionString = connectionString;
            _table = table;
            _idField = idField;
        }

        /// <summary>
        /// Obtiene una entidad por su identificador
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <param name="readMethod">Lector concreto de la entidad</param>
        /// <returns>Entidad</returns>
        protected T Get(int id, Func<SqlDataReader, T> readMethod)
        {
            try
            {
                // Establece conexión con la base de datos
                var connection = new SqlConnection(_connectionString);
                connection.Open();

                // Prepara la consulta
                string query = string.Format("SELECT * FROM {0} WHERE {1} = @Id", _table, _idField);
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                // Lanza la query
                var reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    return readMethod(reader);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("SqlDAO Get({0}) --> {1} - {2}", id, ex.Message, ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Obtiene todos los registros de una entidad
        /// </summary>
        /// <param name="readMethod">Lector concreto de la entidad</param>
        /// <returns>Lista de entidades</returns>
        protected T[] GetAll(Func<SqlDataReader, T> readMethod)
        {
            var result = new List<T>();
            try
            {
                // Establece conexión con la base de datos
                var connection = new SqlConnection(_connectionString);
                connection.Open();

                // Prepara la consulta
                string query = string.Format("SELECT * FROM {0}", _table);
                var command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(readMethod(reader));
                    }
                }
                return result.ToArray();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("SqlDAO GetAll() --> {0} - {1}", ex.Message, ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Guarda una entidad (nueva o modificada)
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <param name="parameters">Lista de parámetros con el valor de sus propiedades</param>
        /// <returns>Identificador de la entidad creada o actualizada</returns>
        protected int Save (int id, SqlParameter[] parameters)
        {
            try
            {
                // Establece conexión con la base de datos
                var connection = new SqlConnection(_connectionString);
                connection.Open();

                // Prepara la query según la situación (nuevo o actualizado)
                var command = new SqlCommand();
                command.Connection = connection;
                if (id == 0)
                {
                    // Inserta nuevo elemento
                    command.CommandText = string.Format("INSERT INTO {0} ({2}) output INSERTED.{1} VALUES ({3})", _table, _idField,
                        string.Join(", ", parameters.Select(p => p.ParameterName.Replace("@", ""))),
                        string.Join(", ", parameters.Select(p => p.ParameterName)));
                }
                else
                {
                    // Actualiza el elemento
                    command.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2} = @_Parameter_Id", _table,
                        string.Join(", ", parameters.Select(p => string.Format("{0} = {1}", p.ParameterName.Replace("@", ""), p.ParameterName))),
                        _idField);
                    command.Parameters.AddWithValue("@_Parameter_Id", id);
                }
                command.Parameters.AddRange(parameters);

                // Lanza la operación
                var result = command.ExecuteScalar();
                if (result != null && id == 0)
                {
                    return Convert.ToInt32(result);
                }
                return id;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("SqlDAO Save() --> {0} - {1}", ex.Message, ex.InnerException);
            }
            return 0;            
        }

        /// <summary>
        /// Elimina el registro de una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns>Indicador de exito de la operación</returns>
        protected bool DeleteDB(int id)
        {
            try
            {
                // Establece conexión con la base de datos
                var connection = new SqlConnection(_connectionString);
                connection.Open();

                // Prepara la consulta
                string query = string.Format("DELETE FROM {0} WHERE {1} = @Id", _table, _idField);
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                // Lanza el borrado
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("SqlDAO Delete({0}) --> {1} - {2}", id, ex.Message, ex.InnerException);
            }
            return false;
        }

        /// <summary>
        /// Comprueba el valor adecuado para insertar en base de datos
        /// </summary>
        /// <param name="value">Valor candidato a insertar</param>
        /// <returns>Valor adaptado a base de datos</returns>
        public object GetDBValue(object value)
        {
            if (value == null)
                return DBNull.Value; // Valores nullables
            return value;
        }
    }
}
