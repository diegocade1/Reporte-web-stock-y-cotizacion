using S_Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class Encabezado
    {
        private int _correlativo;
        private string _rut;
        private string _razon_social;
        private string _fecha;
        private string _telefono;
        private string _contacto;
        private string _correo;
        private string _condicion_pago;
        private string _entrega;
        private double _neto;
        private double _iva;
        private double _total;
        private string _codigo_usuario;
        private string _estado;
        private string _direccion;
        private string _tipo_moneda;
        private string _observacion_estado;

        public string Observacion_estado
        {
            get { return _observacion_estado; }
            set { _observacion_estado = value; }
        }

        public string Tipo_moneda
        {
            get { return _tipo_moneda; }
            set { _tipo_moneda = value; }
        }


        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }


        public string Codigo_usuario
        {
            get { return _codigo_usuario; }
            set { _codigo_usuario = value; }
        }

        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }


        public double Iva
        {
            get { return _iva; }
            set { _iva = value; }
        }


        public double Neto
        {
            get { return _neto; }
            set { _neto = value; }
        }


        public string Entrega
        {
            get { return _entrega; }
            set { _entrega = value; }
        }


        public string CondicionPago
        {
            get { return _condicion_pago; }
            set { _condicion_pago = value; }
        }


        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string Razon_social
        {
            get { return _razon_social; }
            set { _razon_social = value; }
        }

        public string Rut
        {
            get { return _rut; }
            set { _rut = value; }
        }

        public int Correlativo
        {
            get { return _correlativo; }
            set { _correlativo = value; }
        }

        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from encabezado where correlativo_id = '{0}';", _correlativo));
                Conexion con = new Conexion();
                EncabezadoBase pd = con.ObtenerEncabezado(sb.ToString());
                this._correlativo = pd.correlativo;
                this._codigo_usuario = pd.codigo_usuario;
                this._condicion_pago = pd.condicion_pago;
                this._contacto = pd.contacto;
                this._correo = pd.correo;
                this._direccion = pd.direccion;
                this._entrega = pd.entrega;
                this._estado = pd.estado;
                this._observacion_estado = pd.observacion_estado;
                this._fecha = pd.fecha;
                this._iva = pd.iva;
                this._neto = pd.neto;
                this._razon_social = pd.razon_social;
                this._rut = pd.rut;
                this._telefono = pd.telefono;
                this._tipo_moneda = pd.tipo_moneda;
                this._total = pd.total;
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
                //sb.Append(string.Format("insert into encabezado(rut,razon_social,fecha,telefono,contacto,correo,condicion_pago,entrega,neto,iva,total,codigo_usuario,estado,direccion,tipo_moneda) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}','{13}','{14}');" + " \r\n SELECT LAST_INSERT_ID();", _rut, _razon_social, _fecha, _telefono, _contacto, _correo, _condicion_pago, _entrega, _neto.ToString().Replace(",", "."), _iva, _total.ToString().Replace(",", "."), _codigo_usuario, _estado, _direccion, _tipo_moneda));
                sb.Append(string.Format("insert into encabezado(rut,razon_social,fecha,telefono,contacto,correo,condicion_pago,entrega,neto,iva,total,codigo_usuario,estado,direccion,tipo_moneda,observacion_estado) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}','{13}','{14}','{15}');", _rut, _razon_social, _fecha, _telefono, _contacto, _correo, _condicion_pago, _entrega, _neto.ToString().Replace(",", "."), _iva, _total.ToString().Replace(",", "."), _codigo_usuario, _estado, _direccion, _tipo_moneda,_observacion_estado));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                _correlativo = Convert.ToInt32(LastID());
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from encabezado where correlativo_id = {0};",_correlativo));
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

        public bool Update()
        {
            try
            {
                //DateTime fecha = Convert.ToDateTime(_fecha, CultureInfo.InvariantCulture);
                DateTime fecha = Convert.ToDateTime(_fecha); ;
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update encabezado set rut = '{0}', razon_social = '{1}', fecha = '{2}', telefono = '{3}', contacto = '{4}',correo = '{5}',condicion_pago = '{6}', entrega = '{7}', neto = {8},iva = {9},total = {10},codigo_usuario = '{11}',estado = '{12}',direccion = '{13}',tipo_moneda = '{14}', observacion_estado = '{15}' where correlativo_id = {16};", _rut, _razon_social, fecha.ToString("yyyy-MM-dd"), _telefono, _contacto, _correo, _condicion_pago, _entrega, _neto.ToString().Replace(",", "."), _iva, _total.ToString().Replace(",", "."), _codigo_usuario, _estado, _direccion, _tipo_moneda, _observacion_estado,_correlativo));
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

        public string UpdateMensaje()
        {
            try
            {
                DateTime fecha = Convert.ToDateTime(_fecha, CultureInfo.InvariantCulture);
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("update encabezado set rut = '{0}', razon_social = '{1}', fecha = '{2}', telefono = '{3}', contacto = '{4}',correo = '{5}',condicion_pago = '{6}', entrega = '{7}', neto = {8},iva = {9},total = {10},codigo_usuario = '{11}',estado = '{12}',direccion = '{13}',tipo_moneda = '{14}', observacion_estado = '{15}' where correlativo_id = {16};", _rut, _razon_social, fecha.ToString("yyyy-MM-dd"), _telefono, _contacto, _correo, _condicion_pago, _entrega, _neto.ToString().Replace(",", "."), _iva, _total.ToString().Replace(",", "."), _codigo_usuario, _estado, _direccion, _tipo_moneda, _observacion_estado, _correlativo));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return "OK";
            }
            catch (Exception ex)
            {               
                return ex.Message.ToString();
            }
        }

        public string LastID()
        {
            try
            {
                Conexion con = new Conexion();
                string last_insert_id = "SELECT LAST_INSERT_ID(); " + "\n"+ " ";
                string resultado = con.EjecutarComandos2(last_insert_id);
                return resultado;
            }
            catch
            {
                return null;
            }
        }
    }
    public class Detalle
    {
        private int _id;
        private int _correlativo;
        private long _cantidad;
        private string _descripcion;
        private double _precio_unitario;
        private string _codigo;
        private double _subtotal;


        public double Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public double PrecioUnitario
        {
            get { return _precio_unitario; }
            set { _precio_unitario = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public long Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public int Correlativo
        {
            get { return _correlativo; }
            set { _correlativo = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Create()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("insert into detalle(correlativo,cantidad,descripcion,precio_unitario,codigo,subtotal) values ({0},{1},'{2}',{3},'{4}',{5});"
                    , _correlativo, _cantidad, _descripcion, _precio_unitario.ToString().Replace(",", "."), _codigo, _subtotal.ToString().Replace(",", ".")));
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
                sb.Append(string.Format("update detalle set correlativo = {0}, cantidad = {1}, descripcion = '{2}', precio_unitario = {3}, codigo = '{4}', subtotal = {5} where id = {5};"
                    , _correlativo, _cantidad, _descripcion, _precio_unitario.ToString().Replace(",", "."), _codigo, _subtotal.ToString().Replace(",", "."),_id));
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
                sb.Append(string.Format("delete from detalle where id = {0};"
                    , _id));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LimpiarDetalleCorrelativo()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from detalle where correlativo = {0};"
                    , _correlativo));
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

    public class EstadoCotizacion
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
                sb.Append(string.Format("select * from estados_cotizacion where id = {0} and descripcion = '{1}';", _id, _descripcion));
                Conexion con = new Conexion();
                EstadoCotizacionBase pd = con.ObtenerEstadoCotizacion(sb.ToString());
                this._id = pd.Id;
                this._descripcion = pd.Descripcion;
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
                sb.Append(string.Format("insert into estados_cotizacion (descripcion) values ('{0}');", _descripcion));
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
                sb.Append(string.Format("update estados_cotizacion SET descripcion = '{0}' where id = {1};", _descripcion, _id));
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
                    sb.Append(string.Format("delete from estados_cotizacion where id = {0};", _id));
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
