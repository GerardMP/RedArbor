using RedArbor.Data.Entities;
using System;

namespace RedArbor.Data.Entities
{
    /// <summary>
    /// Datos de la entidad Employee
    /// </summary>
    public class Employee: AuditableEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Telephone { get; set; }
        public string Username { get; set; }
    }
}
