using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionTipoMoneda
    {
        public List<TipoMoneda> ListaTipoMonedas()
        {
            string sql = "select * from tipo_moneda;";
            List<TipoMoneda> lista = new List<TipoMoneda>();
            Conexion con = new Conexion();
            foreach (TipoMonedaBase p in con.ObtenerMonedas(sql))
            {
                TipoMoneda pr = new TipoMoneda()
                {
                    ID = p.Id,
                    Nombre = p.Nombre,
                    Simbolo = p.Simbolo                   
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
