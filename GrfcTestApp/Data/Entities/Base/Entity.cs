using GrfcTestApp.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class Entity:IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
