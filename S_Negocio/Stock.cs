using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class Stock
    {
        private string _codigo;
        private int _id;
        private string _centro_costos;
        private int _stock;
        private string _ubicacion;

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }


        public int Stocks
        {
            get { return _stock; }
            set { _stock = value; }
        }


        public string CentroCosto
        {
            get { return _centro_costos; }
            set { _centro_costos = value; }
        }


        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from tipo_stock where CODIGO_P = '{0}' and centro_costo = '{1}' and ubicacion = '{2}';", _codigo,_centro_costos,_ubicacion));
                Conexion con = new Conexion();
                StockBase pd = con.ObtenerStock(sb.ToString());
                this._id = pd.Id;
                this._codigo = pd.Codigo;
                this._stock = pd.Stock;
                this._ubicacion = pd.Ubicaciones;
                this._centro_costos = pd.Centro_costo;
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
                sb.Append(string.Format("insert into tipo_stock (CODIGO_P,STOCK,UBICACION,CENTRO_COSTO) values ('{0}',{1},'{2}','{3}');", _codigo, _stock, _ubicacion, _centro_costos));
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
                sb.Append(string.Format("update tipo_stock SET STOCK = STOCK + {0} where CODIGO_P = '{1}' and UBICACION = '{2}' and CENTRO_COSTO = '{3}';", _stock, _codigo, _ubicacion,_centro_costos));
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

    public class StockDescripcion : Stock
    {
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

    }
}
