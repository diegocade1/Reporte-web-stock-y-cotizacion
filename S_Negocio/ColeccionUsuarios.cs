using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionUsuarios
    {
        public List<Usuario> ListaUsuarios()
        {
            String sql = "SELECT * from usuarios;";
            List<Usuario> lista = new List<Usuario>();
            Conexion con = new Conexion();
            foreach (UsuarioBase p in con.ObtenerListaUsuario(sql))
            {
                Usuario pr = new Usuario()
                {
                    ID = p.Id,
                    Apellido = p.Apellido,
                    Cargo = p.Cargo,
                    Estado = p.Estado,
                    Nombre = p.Nombre,
                    Password = p.Password,
                    Tipo_usuario = p.Tipo_usuario,
                    Usuario_ = p.Usuario
                };
                lista.Add(pr);
            }
            return lista;
        }
        public bool Update(Usuario obj)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update usuarios set password = '{0}',nombre='{1}',apellido='{2}',cargo = '{3}', tipo_usuario = '{4}', estado = '{5}' where id = {6};", obj.Password, obj.Nombre, obj.Apellido, obj.Cargo, obj.Tipo_usuario, obj.Estado, obj.ID));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool Delete(Usuario obj)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from usuarios where id = {0};", obj.ID));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}
