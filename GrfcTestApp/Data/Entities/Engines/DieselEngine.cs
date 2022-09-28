using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities.Engines
{
    /// <summary>
    /// Дизельный двигатель
    /// </summary>
    public class DieselEngine: CombustionEngine
    {
        public override string ToString() => "Дизельный двигатель";
    }
}
