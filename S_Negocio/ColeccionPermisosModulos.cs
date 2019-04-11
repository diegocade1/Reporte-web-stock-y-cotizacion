using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionPermisosModulos
    {
        public List<PermisosModulos> ListaProductos()
        {
            String sql = "select * from permisos_modulos;";
            List<PermisosModulos> lista = new List<PermisosModulos>();
            Conexion con = new Conexion();
            foreach (PermisosModulosBase p in con.ObtenerListaModulosUsuario(sql))
            {
                PermisosModulos pr = new PermisosModulos()
                {
                    ID_tipo_usuario = p.Id_tipo_usuario,
                    Modulos = p.Modulos
                };
                lista.Add(pr);
            }
            return lista;
        }

        public bool Create(PermisosModulos permisos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into permisos_modulos(id_tipo_usuario, modulos) values ({0},'{1}');", permisos.ID_tipo_usuario, permisos.Modulos));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(PermisosModulos permisos)
        {
            try
            {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("delete from permisos_modulos where id_tipo_usuario = '{0}';", permisos.ID_tipo_usuario));
                    Conexion con = new Conexion();
                    con.EjecutarComandos(sb.ToString());
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(PermisosModulos permisos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update permisos_modulos SET modulos = '{0}' where id_tipo_usuario = '{1}';", permisos.Modulos, permisos.ID_tipo_usuario));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
