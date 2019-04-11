using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class RegistroBase
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }
        public int ingreso { get; set; }
        public int egreso { get; set; }
        public string hora { get; set; }
    }
}
