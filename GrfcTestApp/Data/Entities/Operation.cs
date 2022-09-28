using GrfcTestApp.Data.Entities.Base;
using GrfcTestApp.Data.Entities.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities
{
    /// <summary>
    /// Описание работ выполняемых при обслуживании автомобиля
    /// </summary>
    public class Operation:Entity
    {
        /// <summary>
        /// Описание производимых работ
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Тип двигателя к которому применимы работы
        /// </summary>
        [Required]
        public EngineBase EngineType { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
    }
}
