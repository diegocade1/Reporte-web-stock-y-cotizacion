using S_Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionEncabezado
    {
        public List<Encabezado> ListaEncabezados()
        {
            String sql = "select * from encabezado;";
            List<Encabezado> lista = new List<Encabezado>();
            Conexion con = new Conexion();
            foreach (EncabezadoBase p in con.ObtenerEncabezados(sql))
            {
                Encabezado pr = new Encabezado()
                {
                    Codigo_usuario = p.codigo_usuario,
                    CondicionPago = p.condicion_pago,
                    Contacto = p.contacto,
                    Correlativo = p.correlativo,
                    Correo = p.correo,
                    Direccion = p.direccion,
                    Entrega = p.entrega,
                    Estado = p.estado,
                    Observacion_estado = p.observacion_estado,
                    Fecha = p.fecha,
                    Iva = p.iva,
                    Neto = p.neto,
                    Razon_social = p.razon_social,
                    Rut = p.rut,
                    Telefono = p.telefono,
                    Tipo_moneda = p.tipo_moneda,
                    Total = p.total
                };
                lista.Add(pr);
            }
            return lista;
        }

        public List<Encabezado> ListaEncabezadoClientes()
        {
            String sql = "select * from encabezado group by rut;";
            List<Encabezado> lista = new List<Encabezado>();
            Conexion con = new Conexion();
            foreach (EncabezadoBase p in con.ObtenerEncabezados(sql))
            {
                Encabezado pr = new Encabezado()
                {
                    Codigo_usuario = p.codigo_usuario,
                    CondicionPago = p.condicion_pago,
                    Contacto = p.contacto,
                    Correlativo = p.correlativo,
                    Correo = p.correo,
                    Direccion = p.direccion,
                    Entrega = p.entrega,
                    Estado = p.estado,
                    Observacion_estado = p.observacion_estado,
                    Fecha = p.fecha,
                    Iva = p.iva,
                    Neto = p.neto,
                    Razon_social = p.razon_social,
                    Rut = p.rut,
                    Telefono = p.telefono,
                    Tipo_moneda = p.tipo_moneda,
                    Total = p.total
                };
                lista.Add(pr);
            }
            return lista;
        }

        public List<Encabezado> ListaEncabezadoPendientes()
        {
            return ListaEncabezados().Where(fila => (fila.Estado.ToLower().Replace(" ", "").Contains("pendiente"))).ToList();
        }

        public List<Encabezado> ListaEncabezadoFiltro(string filtro)
        {
            return ListaEncabezados().Where(fila => (fila.Estado.ToLower().Replace(" ", "").Contains(filtro.ToLower()))).ToList();
        }

        public List<Encabezado> ListaEncabezadoClienteFiltro(string filtro)
        {
            return ListaEncabezadoClientes().Where(fila => (fila.Razon_social.ToLower().Replace(" ", "").Contains(filtro.ToLower()))).ToList();
        }

        public bool Update(Encabezado obj)
        {
            DateTime fecha = Convert.ToDateTime(obj.Fecha, CultureInfo.InvariantCulture);
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("update encabezado set rut = '{0}', razon_social = '{1}', fecha = '{2}', telefono = '{3}', contacto = '{4}',correo = '{5}',condicion_pago = '{6}', entrega = '{7}', neto = {8},iva = {9},total = {10},codigo_usuario = '{11}',estado = '{12}',direccion = '{13}',tipo_moneda = '{14}' where correlativo_id = {15};", obj.Rut, obj.Razon_social, fecha.ToString("yyyy-MM-dd"), obj.Telefono, obj.Contacto, obj.Correo, obj.CondicionPago, obj.Entrega, obj.Neto.ToString().Replace(",", "."), obj.Iva, obj.Total.ToString().Replace(",", "."), obj.Codigo_usuario, obj.Estado, obj.Direccion, obj.Tipo_moneda, obj.Correlativo));
                sb.Append(string.Format("update encabezado set rut = '{0}', razon_social = '{1}', fecha = '{2}', telefono = '{3}', contacto = '{4}',correo = '{5}',condicion_pago = '{6}', entrega = '{7}', neto = {8},iva = {9},total = {10},codigo_usuario = '{11}',estado = '{12}',direccion = '{13}',tipo_moneda = '{14}', observacion_estado = '{15}' where correlativo_id = {16};", obj.Rut, obj.Razon_social, fecha.ToString("yyyy-MM-dd"), obj.Telefono, obj.Contacto, obj.Correo, obj.CondicionPago, obj.Entrega, obj.Neto, obj.Iva, obj.Total, obj.Codigo_usuario, obj.Estado, obj.Direccion, obj.Tipo_moneda,obj.Observacion_estado ,obj.Correlativo));
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

        public bool Delete(Encabezado obj)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from encabezado where correlativo_id = {0};", obj.Correlativo));
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

    public class ColeccionDetalle
    {
        public List<Detalle> ListaDetalle(string correlativo)
        {
            String sql = "select * from detalle where correlativo ="+ correlativo +";";
            List<Detalle> lista = new List<Detalle>();
            Conexion con = new Conexion();
            foreach (DetalleBase p in con.ObtenerDetalles(sql))
            {
                Detalle pr = new Detalle()
                {
                    Cantidad = p.cantidad,
                    Codigo = p.codigo,
                    Correlativo = p.correlativo,
                    Descripcion = p.descripcion,
                    ID = p.id,
                    PrecioUnitario = p.precio_unitario,
                    Subtotal = p.subtotal
                };
                lista.Add(pr);
            }
            return lista;
        }

        public bool Update(Detalle obj)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("update detalle set correlativo = {0}, cantidad = {1}, descripcion = '{2}', precio_unitario = {3}, codigo = '{4}', subtotal = {5} where id = {6};"
                //    , obj.Correlativo, obj.Cantidad, obj.Descripcion, obj.PrecioUnitario.ToString().Replace(",", "."), obj.Codigo, obj.Subtotal.ToString().Replace(",", "."), obj.ID));
                sb.Append(string.Format("update detalle set correlativo = {0}, cantidad = {1}, descripcion = '{2}', precio_unitario = {3}, codigo = '{4}', subtotal = {5} where id = {6};"
    , obj.Correlativo, obj.Cantidad, obj.Descripcion, obj.PrecioUnitario, obj.Codigo, obj.Subtotal, obj.ID));
                Conexion con = new Conexion();
                con.EjecutarComandos(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Detalle obj)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("delete from detalle where id = {0};"
                    ,obj.ID));
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

    public class ColeccionEstadosCotizacion
    {
        public List<EstadoCotizacion> ListaDetalle()
        {
            String sql = "select * from estados_cotizacion;";
            List<EstadoCotizacion> lista = new List<EstadoCotizacion>();
            Conexion con = new Conexion();
            foreach (EstadoCotizacionBase p in con.ObtenerListaEstadosCotizacion(sql))
            {
                EstadoCotizacion pr = new EstadoCotizacion()
                {
                    ID = p.Id,
                    Descripcion = p.Descripcion
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
