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
    /// Техническое обслуживание автомобиля
    /// </summary>
    public class Maintenance : Entity
    {
        /// <summary>
        /// Дата и время проведения работ по обслуживанию автомобиля
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public Automobile Automobile { get; set; }

        /// <summary>
        /// Перечень работ выполненных при обслуживании автомобиля
        /// </summary>
        [Required]
        public ICollection<Operation> Operation { get; set; }

        public override string ToString()
        {
            return Automobile.RegistrationNumber;
        }
    }
}
