using RedArbor.Data.Helpers;
using RedArbor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RedArbor.Data.DAO
{
    /// <summary>
    /// Objeto de acceso a persistencia para la entidad Employee
    /// </summary>
    public class EmployeeDAO: SqlDAO<Employee>, IEmployeeDAO
    {
        /// <summary>
        /// Constructor en el que se inyecta el connection string y se especifica Tabla y campo de clave primaria a la clase genérica de SQL
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a base de datos</param>
        public EmployeeDAO(string connectionString) : base(connectionString, "Employees", "Id")
        {
        }

        /// <summary>
        /// Obtiene la lista de todos los employees
        /// </summary>
        /// <returns>Lista de employees</returns>
        public Employee[] GetAll()
        {
            var list = base.GetAll(ReadEmployee);
            return list;
        }

        /// <summary>
        /// Obtiene los datos de un employee
        /// </summary>
        /// <param name="id">Identificador del employee</param>
        /// <returns>Datos del employee</returns>
        public Employee Get(int id)
        {
            return base.Get(id, ReadEmployee);
        }

        /// <summary>
        /// Guarda los datos de un employee (crea o actualiza) 
        /// </summary>
        /// <param name="employee">Datos del employee</param>
        /// <returns>Datos del employee guardado</returns>
        public Employee Save(Employee employee)
        {
            // TODO: Automatizar auditoría
            if (employee.Id == 0)
                employee.AuditCreation();
            else
                employee.AuditUpdate();

            // Guardado y actualización de la entidad con el posible nuevo id obtenido
            int newId = base.Save(employee.Id, Parametrize(employee));
            employee.Id = newId;

            return employee;
        }

        /// <summary>
        /// Elimina los datos de un employee
        /// </summary>
        /// <param name="id">Identificador del employee a eliminar</param>
        /// <returns>Indicador de exito de la operación</returns>
        public bool Delete(int id)
        {
            return base.DeleteDB(id);
        }

        /// <summary>
        /// Lee una entidad de employee de un reader de base de datos
        /// </summary>
        /// <param name="reader">SqlReader con la información</param>
        /// <returns>Datos de la entidad Employee</returns>
        protected Employee ReadEmployee(SqlDataReader reader)
        {
            var employee = new Employee();

            if (reader != null)
            {
                employee.Id = Convert.ToInt32(reader["Id"]);
                employee.CompanyId = Convert.ToInt32(reader["CompanyId"]);
                if (reader["CreatedOn"] != DBNull.Value) employee.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                if (reader["DeletedOn"] != DBNull.Value) employee.DeletedOn = Convert.ToDateTime(reader["DeletedOn"]);
                employee.Email = Convert.ToString(reader["Email"]);
                employee.Fax = Convert.ToString(reader["Fax"]);
                employee.Name = Convert.ToString(reader["Name"]);
                if (reader["Lastlogin"] != DBNull.Value) employee.Lastlogin = Convert.ToDateTime(reader["Lastlogin"]);
                employee.Password = CryptHelper.Decrypt(Convert.ToString(reader["Password"]));
                employee.PortalId = Convert.ToInt32(reader["PortalId"]);
                employee.RoleId = Convert.ToInt32(reader["RoleId"]);
                employee.StatusId = Convert.ToInt32(reader["StatusId"]);
                if (reader["Telephone"] != DBNull.Value) employee.Telephone = reader["Telephone"].ToString();
                if (reader["UpdatedOn"] != DBNull.Value) employee.UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                employee.Username = Convert.ToString(reader["Username"]);
            }
            return employee;
        }

        /// <summary>
        /// Crea los parámetros SQL de todas las propiedades de la entidad Employee
        /// </summary>
        /// <param name="employee">Datos de la entidad employee</param>
        /// <returns>Lista de parámetros SQL</returns>
        private SqlParameter[] Parametrize(Employee employee)
        {
            var parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@CompanyId", employee.CompanyId));
            parameterList.Add(new SqlParameter("@CreatedOn", base.GetDBValue(employee.CreatedOn)));
            parameterList.Add(new SqlParameter("@DeletedOn", base.GetDBValue(employee.DeletedOn)));
            parameterList.Add(new SqlParameter("@Email", employee.Email));
            parameterList.Add(new SqlParameter("@Fax", base.GetDBValue(employee.Fax)));
            parameterList.Add(new SqlParameter("@Name", employee.Name));
            parameterList.Add(new SqlParameter("@Lastlogin", base.GetDBValue(employee.Lastlogin)));
            parameterList.Add(new SqlParameter("@Password", CryptHelper.Encrypt(employee.Password)));
            parameterList.Add(new SqlParameter("@PortalId", employee.PortalId));
            parameterList.Add(new SqlParameter("@RoleId", employee.RoleId));
            parameterList.Add(new SqlParameter("@StatusId", employee.StatusId));
            parameterList.Add(new SqlParameter("@Telephone", employee.Telephone));
            parameterList.Add(new SqlParameter("@UpdatedOn", employee.UpdatedOn));
            parameterList.Add(new SqlParameter("@Username", employee.Username));

            return parameterList.ToArray();
        }
    }
}
