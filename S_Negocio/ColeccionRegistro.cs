using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionRegistro
    {
        public List<Registro> Movimientos(string fecha_ini, string fecha_fin)
        {
            String sql = "select a.id, b.descripcion ,a.codigo, fecha, ingreso, egreso, hora from registro as a left join productos as b on a.codigo = b.codigo where a.fecha between '" + fecha_ini+"' and '"+fecha_fin+"';";
            List<Registro> lista = new List<Registro>();
            Conexion con = new Conexion();
            try
            {
                foreach (RegistroBase p in con.ObtenerRegistro(sql))
                {
                    Registro pr = new Registro()
                    {
                        Descripcion = p.descripcion,
                        Codigo = p.codigo,
                        Egreso = p.egreso,
                        Fecha = p.fecha,
                        Hora = p.hora,
                        Ingreso = p.ingreso,
                        ID = p.id
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
    }
}
