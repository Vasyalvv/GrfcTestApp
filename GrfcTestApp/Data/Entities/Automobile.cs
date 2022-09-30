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
    /// Автомобиль
    /// </summary>
    public class Automobile:Entity
    {
        /// <summary>
        /// Регистрационный номер автомобиля
        /// </summary>
        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public CarModel CarModel { get; set; }

        public override string ToString()
        {
            return RegistrationNumber;
        }
    }
}
