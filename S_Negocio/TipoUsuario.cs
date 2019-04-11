using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class TipoUsuario
    {
        private int _id;
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from tipo_usuario where descripcion = '{0}';", _descripcion));
                Conexion con = new Conexion();
                TipoUsuarioBase pd = con.ObtenerTipoUsuario(sb.ToString());
                this._id = pd.Id;
                this._descripcion = pd.Descripcion;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadID()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from tipo_usuario where id = '{0}';", _id));
                Conexion con = new Conexion();
                TipoUsuarioBase pd = con.ObtenerTipoUsuario(sb.ToString());
                this._id = pd.Id;
                this._descripcion = pd.Descripcion;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReturnAdminPrivileges(Usuario usuario)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from tipo_usuario where id = 1;"));
                Conexion con = new Conexion();
                TipoUsuarioBase pd = con.ObtenerTipoUsuario(sb.ToString());
                this._id = pd.Id;
                this._descripcion = pd.Descripcion;
                if(usuario.Tipo_usuario.Equals(this._descripcion))
                {
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
        public bool Create()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into tipo_usuario (descripcion) values ('{0}');", _descripcion));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update tipo_usuario set descripcion = '{0}' where id = {1};", _descripcion,_id));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
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
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from tipo_usuario where id = {0};",_id));
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
