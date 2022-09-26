using GrfcTestApp.Data.Entities.Base;
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
        public EngineType EngineType { get; set; }
    }
}
