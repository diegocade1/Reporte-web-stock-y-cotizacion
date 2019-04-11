using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class StockBase
    {
        public int Id { set; get; }
        public string Codigo { set; get; }
        public int Stock { set; get; }
        public string Centro_costo { set; get; }
        public string Ubicaciones { set; get; }

    }

    public class StockDescripcionBase : StockBase
    {
        public string Descripcion { get; set; }
    }
}
