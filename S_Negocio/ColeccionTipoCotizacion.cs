using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
        public class ColeccionTipoCotizacion
        {
            public List<TipoCotizacion> ListaTipoCotizaciones()
            {
                String sql = "select * from tipo_cotizacion;";
                List<TipoCotizacion> lista = new List<TipoCotizacion>();
                Conexion con = new Conexion();
                foreach (TipoCotizacionBase p in con.ObtenerTipoCotizaciones(sql))
                {
                    TipoCotizacion pr = new TipoCotizacion()
                    {
                        ID = p.Id,
                        Descripcion = p.Descripcion

                    };
                    lista.Add(pr);
                }
                return lista;
            }
        }
}
