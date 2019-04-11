using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class PermisosModulos
    {
        private int _id_tipo_usuario;
        private string _modulos;

        public string Modulos
        {
            get { return _modulos; }
            set { _modulos = value; }
        }

        public int ID_tipo_usuario
        {
            get { return _id_tipo_usuario; }
            set { _id_tipo_usuario = value; }
        }

        public bool Create()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into permisos_modulos(id_tipo_usuario, modulos) values ({0},'{1}');", _id_tipo_usuario, _modulos));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from permisos_modulos where id_tipo_usuario = '{0}';", _id_tipo_usuario));
                Conexion con = new Conexion();
               PermisosModulosBase pd = con.ObtenerModulosUsuario(sb.ToString());
                this._id_tipo_usuario = pd.Id_tipo_usuario;
                this.Modulos = pd.Modulos;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                if (Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("delete from permisos_modulos where id_tipo_usuario = '{0}';", _id_tipo_usuario));
                    Conexion con = new Conexion();
                    con.EjecutarComandos(sb.ToString());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string newcodigo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update permisos_modulos SET modulos = '{0}' where id_tipo_usuario = '{1}';", _modulos, _modulos));
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
