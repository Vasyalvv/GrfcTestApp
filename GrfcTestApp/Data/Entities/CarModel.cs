using GrfcTestApp.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities
{
    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public class CarModel: NamedEntity
    {
        [Required]
        public CarMark CarMark { get; set; }

        [Required]
        public EngineType EngineType { get; set; }
    }
}
