using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionCentroCosto
    {
        public List<CentroCosto> ListaProductosCentroCosto()
        {
            String sql = "SELECT * from centro_costos;";
            List<CentroCosto> lista = new List<CentroCosto>();
            Conexion con = new Conexion();
            foreach (CentroCostoBase p in con.ObtenerListaCentroCostos(sql))
            {
                CentroCosto pr = new CentroCosto()
                {
                    ID = p.Id,
                    Descripcion = p.Descripcion,
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
