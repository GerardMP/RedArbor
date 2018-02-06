using RedArbor.Data.DAO;
using RedArbor.Data.Entities;
using System.Web.Http;
using System.Web.Http.Results;

namespace RedArbor.WebApi.Controllers
{
    /// <summary>
    /// Controlador que atiende a las llamadas a la API /api/redarbor
    /// </summary>
    public class RedArborController : ApiController
    {
        IEmployeeDAO _dbAccess;

        /// <summary>
        /// Constructor para inyección
        /// </summary>
        public RedArborController(IEmployeeDAO employeeDAO)
        {
            _dbAccess = employeeDAO;
        }

        /// <summary>
        /// Petición GET para obtener la lista de employees
        /// </summary>
        /// <returns>Lista de Employees</returns>
        public IHttpActionResult Get()
        {
            var employeeList = _dbAccess.GetAll();

            if (employeeList == null)
                return new BadRequestResult(Request);

            return Json(employeeList);
        }

        /// <summary>
        /// Petición GET para obtener un employee concreto
        /// </summary>
        /// <param name="id">Identificador del employee a recuperar</param>
        /// <returns>Datos del employee</returns>
        public IHttpActionResult Get(int id)
        {
            var employee = _dbAccess.Get(id);

            if (employee == null)
                return new NotFoundResult(Request);

            return Json(employee);
        }

        /// <summary>
        /// Petición POST para crear un nuevo employee
        /// </summary>
        /// <param name="employee">Datos del employee en el cuerpo de la petición</param>
        /// <returns>Datos del employee creado</returns>
        public IHttpActionResult Post([FromBody]Employee employee)
        {
            employee.Id = 0; // Aseguramos que siempre se cree
            var result = _dbAccess.Save(employee);

            if (result.Id == 0)
                return new BadRequestResult(Request);

            return Json(result);
        }

        /// <summary>
        /// Petición PUT para actualizar los datos de un employee
        /// </summary>
        /// <param name="id">Identificador del employee a actualizar</param>
        /// <param name="employee">Datos modificados del employee</param>
        public IHttpActionResult Put(int id, [FromBody]Employee employee)
        {
            if (id != employee.Id || id == 0)
                return new BadRequestResult(Request);

            var result = _dbAccess.Save(employee);

            if (result == null)
                return new BadRequestResult(Request);

            return new OkResult(Request);
        }

        /// <summary>
        /// Petición DELETE para eliminar un employee
        /// </summary>
        /// <param name="id">Identificador del employee a eliminar</param>
        public IHttpActionResult Delete(int id)
        {
            if (_dbAccess.Delete(id))
                return new OkResult(Request);

            return new BadRequestResult(Request);
        }
    }
}
