using GrfcTestApp.Data.Entities.Base;
using GrfcTestApp.Data.Entities.Engines;
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
    public class CarModel: Entity
    {
        [Required]
        [Index("IX_NameAndCarMark", 1)]
        public string Name { get; set; }

        [Index("IX_NameAndCarMark", 2)]
        public int CarMark_Id { get; set; }

        [Required]
        [ForeignKey(nameof(CarMark_Id))]        
        public CarMark CarMark { get; set; }

        [Required]
        public EngineBase EngineType { get; set; }
    }
}
