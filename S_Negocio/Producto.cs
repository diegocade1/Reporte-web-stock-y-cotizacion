using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    [Serializable]
    public class Producto
    {
        private int _id;
        private string _descripcion;
        private string _codigo;
        private int _stock;
        private string _familia;
        private double _paquete;
        private string _umedida;

        public string Umedida
        {
            get { return _umedida; }
            set { _umedida = value; }
        }

        public double Paquete
        {
            get { return _paquete; }
            set { _paquete = value; }
        }

        public string Familia
        {
            get { return _familia; }
            set { _familia = value; }
        }

        public int Stock
        {
            get { return _stock; }
            set { _stock = value;}
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int ID
        {
            get { return _id;}
            set { _id = value;}
        }

        public Producto()
        {
            _id = 0;
            _codigo = string.Empty;
            _descripcion = string.Empty;
            _familia = string.Empty;
            _paquete = 0;
            _stock = 0;
            _umedida = string.Empty;
        }

        public bool Create()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into productos (CODIGO, DESCRIPCION,FAMILIA,PAQUETE,UMEDIDA) values ('{0}','{1}','{2}',{3},'{4}');",_codigo,_descripcion,_familia,_paquete,_umedida));
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
                sb.Append(string.Format("select * from productos where CODIGO = '{0}';", _codigo));
                Conexion con = new Conexion();
                ProductoBase pd = con.ObtenerProducto(sb.ToString());
                this._id = pd.ID;
                this._codigo = pd.Codigo;
                this._descripcion = pd.Descripcion;
                this._familia = pd.Familia;
                this._paquete = pd.Paquete;
                this._umedida = pd.Umedida;
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
                    sb.Append(string.Format("delete from productos where CODIGO = '{0}';", _codigo));
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
                    sb.Append(string.Format("update productos SET CODIGO = '"+ newcodigo +"', DESCRIPCION = '{0}', PAQUETE = {1}, FAMILIA = '{2}', UMEDIDA = '{3}' where CODIGO = '{4}';",_descripcion,_paquete,_familia,_umedida,_codigo));
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
