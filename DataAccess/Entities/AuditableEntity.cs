using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Data.Entities
{
    /// <summary>
    /// Datos comunes en una entidad auditable
    /// </summary>
    public class AuditableEntity
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Establece la información relativa a la creación
        /// </summary>
        public void AuditCreation()
        {
            CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// Establece la información relativa a la actualización
        /// </summary>
        public void AuditUpdate()
        {
            UpdatedOn = DateTime.Now;
        }

        /// <summary>
        /// Establece la información relativa a la eliminación
        /// </summary>
        public void AuditDelete()
        {
            DeletedOn = DateTime.Now;
        }

    }
}
