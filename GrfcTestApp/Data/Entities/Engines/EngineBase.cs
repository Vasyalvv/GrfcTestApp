using GrfcTestApp.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data.Entities.Engines
{
    /// <summary>
    /// Двигатель
    /// </summary>
    public class EngineBase: NamedEntity
    {
        public override string ToString() => "Двигатель";
    }
}
