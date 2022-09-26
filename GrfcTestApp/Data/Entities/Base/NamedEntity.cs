using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities.Base
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public class NamedEntity:Entity
    {
        [Index(IsUnique =true)]
        [Required]
        public string Name { get; set; }
    }
}
