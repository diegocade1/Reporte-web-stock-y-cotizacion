using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class Usuario
    {
        private string _usuario;
        private string _password;
        private string _nombre;
        private string _apellido;
        private string _cargo;
        private int _id;
        private string _tipo_usuario;
        private string _estado;

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string Tipo_usuario
        {
            get { return _tipo_usuario; }
            set { _tipo_usuario = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Usuario_
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string query = "select * from usuarios where usuario = @usuario;";
                Conexion con = new Conexion();
                UsuarioBase usuario = con.ObtenerUsuario(query, _usuario);
                this._id = usuario.Id;
                this._apellido = usuario.Apellido;
                this._cargo = usuario.Cargo;
                this._nombre = usuario.Nombre;
                this._password = usuario.Password;
                this._usuario = usuario.Usuario;
                this._tipo_usuario = usuario.Tipo_usuario;
                this._estado = usuario.Estado;
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool Create()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into usuarios (usuario, password,nombre,apellido,cargo,tipo_usuario,estado) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", _usuario, _password, _nombre, _apellido, _cargo,_tipo_usuario,_estado));
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

        public bool Update()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update usuarios set password = '{0}',nombre='{1}',apellido='{2}',cargo = '{3}', tipo_usuario = '{4}', estado = '{5}' where id = {6};", _password, _nombre, _apellido, _cargo,_tipo_usuario,_estado,_id));
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
                sb.Append(string.Format("delete from usuarios where id = {0};", _id));
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
