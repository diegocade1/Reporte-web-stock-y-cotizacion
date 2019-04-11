using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class TipoMoneda
    {
        private int _id;
        private string _nombre;
        private string _simbolo;

        public string Simbolo
        {
            get { return _simbolo; }
            set { _simbolo = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
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
                sb.Append(string.Format("select * from tipo_moneda where id = {0};", _id, _nombre));
                Conexion con = new Conexion();
                TipoMonedaBase pd = con.ObtenerMoneda(sb.ToString());
                this._id = pd.Id;
                this._nombre = pd.Nombre;
                this._simbolo = pd.Simbolo;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadNombre()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from tipo_moneda where nombre = '{0}';",_nombre));
                Conexion con = new Conexion();
                TipoMonedaBase pd = con.ObtenerMoneda(sb.ToString());
                this._id = pd.Id;
                this._nombre = pd.Nombre;
                this._simbolo = pd.Simbolo;
                return true;
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
                sb.Append(string.Format("insert into tipo_moneda (nombre,simbolo) values ('{0}','{1}');", _nombre,_simbolo));
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
                sb.Append(string.Format("update tipo_moneda SET nombre = '{0}', simbolo = '{1}' where id = {2};", _nombre,_simbolo, _id));
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
                if (Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("delete from tipo_moneda where id = {0};", _id));
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
    }
}
