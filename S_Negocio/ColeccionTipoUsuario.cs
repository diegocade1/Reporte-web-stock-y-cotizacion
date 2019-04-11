using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionTipoUsuario
    {
        public List<TipoUsuario> ListaTipoUsuarios()
        {
            String sql = "SELECT * from tipo_usuario;";
            List<TipoUsuario> lista = new List<TipoUsuario>();
            Conexion con = new Conexion();
            foreach (TipoUsuarioBase p in con.ObtenerListaTipoUsuario(sql))
            {
                TipoUsuario pr = new TipoUsuario()
                {
                    ID = p.Id,
                    Descripcion = p.Descripcion,
                };
                lista.Add(pr);
            }
            return lista;
        }

        public bool Create(TipoUsuario usuario)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into tipo_usuario (descripcion) values ('{0}');", usuario.Descripcion));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(TipoUsuario usuario)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update tipo_usuario set descripcion = '{0}' where id = {1};", usuario.Descripcion, usuario.ID));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(TipoUsuario usuario)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from tipo_usuario where id = {0};", usuario.ID));
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
