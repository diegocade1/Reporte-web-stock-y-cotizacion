using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class ProductoBase
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public string Familia { get; set; }
        public double Paquete { get; set; }
        public string Umedida {get; set;}
    }
}
