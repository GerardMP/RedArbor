using RedArbor.Data.Entities;

namespace RedArbor.Data.DAO
{
    /// <summary>
    /// Contrato de capa de acceso a datos para la entidad Employee
    /// </summary>
    public interface IEmployeeDAO
    {
        /// <summary>
        /// Obtiene la lista de todos los employees
        /// </summary>
        /// <returns>Lista de employees</returns>
        Employee[] GetAll();

        /// <summary>
        /// Obtiene los datos de un employee
        /// </summary>
        /// <param name="id">Identificador del employee</param>
        /// <returns>Datos del employee</returns>
        Employee Get(int id);

        /// <summary>
        /// Guarda los datos de un employee (crea o actualiza) 
        /// </summary>
        /// <param name="employee">Datos del employee</param>
        /// <returns>Datso del employee guardado</returns>
        Employee Save(Employee employee);

        /// <summary>
        /// Elimina los datos de un employee
        /// </summary>
        /// <param name="id">Identificador del employee a eliminar</param>
        /// <returns>Indicador de exito de la operación</returns>
        bool Delete(int id);
    }
}
