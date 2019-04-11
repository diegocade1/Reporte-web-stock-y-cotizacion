using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class EncabezadoBase
    {
        public int correlativo { set; get; }
        public string rut { set; get; }
        public string razon_social { set; get; }
        public string fecha { set; get; }
        public string telefono { set; get; }
        public string contacto { set; get; }
        public string correo { set; get; }
        public string condicion_pago { set; get; }
        public string direccion { set; get; }
        public string entrega { set; get; }
        public string tipo_moneda { set; get; }
        public double neto { set; get; }
        public double iva { set; get; }
        public double total { set; get; }
        public string codigo_usuario { set; get; }
        public string estado { set; get; }
        public string observacion_estado { set; get; }
    }

    public class DetalleBase
    {
        public int id { set; get; }
        public int correlativo { set; get; }
        public long cantidad { set; get; }
        public string descripcion { set; get; }
        public double precio_unitario { set; get; }
        public string codigo { set; get; }
        public double subtotal { set; get; }
    }

    public class EstadoCotizacionBase
    {
        public int Id { set; get; }
        public string Descripcion { set; get; }
    }
}
