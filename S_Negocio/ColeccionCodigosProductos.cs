using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionCodigosProductos
    {
        public Dictionary<string, string> ListaCodigosProductos()
        {
            String sql = "SELECT * FROM codigo_productos order by id desc;";
            Conexion con = new Conexion();
            Dictionary<string, string> lista = con.ObtenerListaCodigosProductos(sql);
            return lista;
        }

        public Dictionary<string, string> ListaCodigosProductosCodigo(string codigo)
        {
            Dictionary<string, string> lista = new Dictionary<string, string>();
            try
            {
                foreach (KeyValuePair<string, string> codigos in ListaCodigosProductos())
                {
                    if (codigos.Key.ToLower().Contains(codigo.ToLower().Replace(" ","")))
                    {
                        lista.Add(codigos.Key, codigos.Value);
                    }
                }
                return lista;
            }
            catch
            {
                return null;
            }

        }
        public Dictionary<string, string> ListaCodigosProductosDescripcion(string descripcion)
        {
            Dictionary<string, string> lista = new Dictionary<string, string>();
            try
            {
                foreach (KeyValuePair<string, string> codigos in ListaCodigosProductos())
                {
                    if (codigos.Value.Replace(" ", "").ToLower().Contains(descripcion.ToLower().Replace(" ","")))
                    {
                        lista.Add(codigos.Key, codigos.Value);
                    }
                }
                return lista;
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, string> BusquedaGeneral(string palabra)
        {
            if (ListaCodigosProductosCodigo(palabra).Any())
            {
                return ListaCodigosProductosCodigo(palabra);
            }
            else if (ListaCodigosProductosDescripcion(palabra).Any())
            {
                return ListaCodigosProductosDescripcion(palabra);
            }
            else
            {
                return null;
            }
        }
    }
}
