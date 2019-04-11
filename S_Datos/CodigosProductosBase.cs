using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class CodigosProductosBase
    {
        public int Id { set; get; }
        public string Codigo { set; get; }
        public string Tipo_producto { set; get; }
        public string Marca { set; get; }
        public string Modelo { set; get; }
        public string Familia { set; get; }
        public string Ancho { set; get; }
        public string Largo { set; get; }
        public string Material { set; get; }
        public string Aro { set; get; }
        public string Avance { set; get; }
        public string Etiqueta_x_rollo { set; get; }
        public string Colores { set; get; }
        public string Salida { set; get; }
        public string Observacion { set; get; }
    }
}
