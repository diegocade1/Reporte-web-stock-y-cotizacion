using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class CodigosProductos
    {
        private int _id;
        private string _codigo;
        private string _tipo_producto;
        private string  _modelo;
        private string _marca;
        private string _familia;
        private string  _ancho;
        private string _largo;
        private string _material;
        private string _aro;
        private string _avance;
        private string _etiqueta_x_rollo;
        private string _colores;
        private string  _salida;
        private string _observacion;

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        public string  Salida
        {
            get { return _salida; }
            set { _salida = value; }
        }

        public string Colores
        {
            get { return _colores; }
            set { _colores = value; }
        }

        public string Etiqueta_x_rollo
        {
            get { return _etiqueta_x_rollo; }
            set { _etiqueta_x_rollo = value; }
        }

        public string Avance
        {
            get { return _avance; }
            set { _avance = value; }
        }

        public string Aro
        {
            get { return _aro; }
            set { _aro = value; }
        }

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public string Largo
        {
            get { return _largo; }
            set { _largo = value; }
        }

        public string  Ancho
        {
            get { return _ancho; }
            set { _ancho = value; }
        }

        public string Familia
        {
            get { return _familia; }
            set { _familia = value; }
        }

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public string  Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public string Tipo_producto
        {
            get { return _tipo_producto; }
            set { _tipo_producto = value; }
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
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
                //sb.Append(string.Format("select * from codigo_productos where CODIGO = '{0}';", _codigo));
                sb.Append(string.Format("select * from codigo_productos where id = '{0}';", _id));
                Conexion con = new Conexion();
                CodigosProductosBase pd = con.ObtenerCodigoProducto(sb.ToString());
                this._id = pd.Id;
                //this._codigo = pd.Codigo;
                this._ancho = pd.Ancho;
                this._familia = pd.Familia;
                this._aro = pd.Aro;
                this._avance = pd.Avance;
                this._colores = pd.Colores;
                this._etiqueta_x_rollo = pd.Etiqueta_x_rollo;
                this._largo = pd.Largo;
                this._marca = pd.Marca;
                this._material = pd.Material;
                this._modelo = pd.Modelo;
                this._observacion = pd.Observacion;
                this._salida = pd.Salida;
                this._tipo_producto = pd.Tipo_producto;
                return true;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
        public bool CreateEtiqueta()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into codigo_productos(codigo,tipo_producto,ancho,avance,aro,material,etiquetaxrollo,colores,observacion,salida,marca,modelo,familia,largo) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}');", _codigo, _tipo_producto, _ancho, _avance, _aro, _material, _etiqueta_x_rollo, _colores, _observacion, _salida,_marca,_modelo,_familia,_largo));
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

        public bool CreateCinta()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into codigo_productos(codigo,ancho,largo,material,aro,observacion,tipo_producto,marca,modelo,familia,avance,etiquetaxrollo,colores,salida) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}');", _codigo, _ancho, _largo,_material, _aro,_observacion,_tipo_producto,_marca,_modelo,_familia,_avance,_etiqueta_x_rollo,_colores,_salida));
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

        public bool CreateHardware()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into codigo_productos(codigo,marca,modelo,familia,observacion,tipo_producto,ancho,largo,material,aro,avance,etiquetaxrollo,colores,salida) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}');", _codigo, _marca, _modelo, _familia, _observacion,_tipo_producto,_ancho,_largo,_material,_aro,_avance,_etiqueta_x_rollo,_colores,_salida));
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
    }
}
