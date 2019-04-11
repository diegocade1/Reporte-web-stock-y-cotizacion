using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionReporte
    {
        public List<Reporte> ListaPorProducto(string centro_costo)
        {
            String sql = "select codigo_p,descripcion, sum(coalesce((stock*precio_producto),0)) as valor_total from tipo_stock as a left join v_productos_ficha as b on a.codigo_p = b.codigo_producto left join productos as c on a.codigo_p = c.codigo where a.centro_costo = '" + centro_costo + "' and b.precio_producto > 0 and a.stock > 0 group by codigo_p;";
            List<Reporte> lista = new List<Reporte>();
            Conexion con = new Conexion();
            foreach (ReporteBase r in con.ObtenerReporte(sql))
            {
                Reporte pr = new Reporte()
                {
                    Codigo = r.Codigo,
                    Descripcion = r.Descripcion,
                    Valortotal = r.Valor_total
                };
                lista.Add(pr);
            }
            return lista;
        }
        public List<Reporte> ListaTotal()
        {
            String sql = "SELECT 'Precio Total Inventario' as codigo_p,'descripcion' as descripcion,sum(coalesce(stock*precio_producto,0)) as valor_total from tipo_stock as a left join v_productos_ficha as b on a.codigo_p = b.codigo_producto left join productos as c on a.codigo_p = c.codigo where b.precio_producto > 0 and a.stock > 0;";
            List<Reporte> lista = new List<Reporte>();
            Conexion con = new Conexion();
            try
            {
                foreach (ReporteBase r in con.ObtenerReporte(sql))
                {
                    Reporte pr = new Reporte()
                    {
                        Codigo = r.Codigo,
                        Valortotal = r.Valor_total
                    };
                    lista.Add(pr);
                }
                return lista;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<Reporte> ListaPorCentroCosto()
        {
            String sql = "SELECT 'codigo' as codigo_p,sum(coalesce(stock*precio_producto,0)) as valor_total, centro_costo as descripcion from tipo_stock as a left join v_productos_ficha as b on a.codigo_p = b.codigo_producto left join productos as c on a.codigo_p = c.codigo where b.precio_producto > 0 and a.stock > 0 group by centro_costo;";
            List<Reporte> lista = new List<Reporte>();
            Conexion con = new Conexion();
            foreach (ReporteBase r in con.ObtenerReporte(sql))
            {
                Reporte pr = new Reporte()
                {
                    Descripcion = r.Descripcion,
                    Valortotal = r.Valor_total
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
