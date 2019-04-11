using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class Conexion
    {
        private static string _conexion;

        public static string Conection
        {
            get { return _conexion; }
            set { _conexion = value; }
        }

        private MySqlConnection con;
        public Conexion()
        {
            con = new MySqlConnection();
            //con.ConnectionString = "Server=localhost;Port=3306;" + "Database=Bodega;Uid=admin;Pwd=57706897";
            //con.ConnectionString = "Server=192.168.1.33;Port=3306;" + "Database=control_bodega;Uid=bodega;Pwd=atc2018;SslMode=none";
            //con.ConnectionString = "Server=192.168.1.30;Port=3306;" + "Database=bodega;Uid=atc;Pwd=123atc;SslMode=none";
            con.ConnectionString = Conection;
        }

        public void EjecutarComandos(String comando)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(comando, con);
            sqlcmd.ExecuteNonQuery();
            con.Close();
        }

        public string EjecutarComandos2(String comando)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(comando, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.Read())
            {
                string v = reader.GetInt32("last_insert_id()").ToString();
                con.Close();
                return v;
            }
            else
            {
                return null;
            }
        }

        public UsuarioBase ObtenerUsuario(string comando, string usuario)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(comando, con);
            sqlcmd.Parameters.AddWithValue("@usuario", usuario);
            //sqlcmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            UsuarioBase elemento = new UsuarioBase();
            if (reader.Read())
            {
                elemento.Usuario = reader.GetString("usuario");
                elemento.Password = reader.GetString("password");
                elemento.Nombre = reader.GetString("nombre");
                elemento.Apellido = reader.GetString("apellido");
                elemento.Cargo = reader.GetString("cargo");
                elemento.Id = reader.GetInt32("id");
                elemento.Tipo_usuario = reader.GetString("tipo_usuario");
                elemento.Estado = reader.GetString("estado");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
                
        }

        public ProductoBase ObtenerProducto(String consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            ProductoBase elemento = new ProductoBase();
            if (reader.Read())
            {
                elemento.ID = reader.GetInt32("ID");
                elemento.Descripcion = reader.GetString("Descripcion");
                elemento.Codigo = reader.GetString("Codigo");
                elemento.Paquete = reader.GetDouble("Paquete");
                elemento.Umedida = reader.GetString("Umedida");
                elemento.Familia = reader.GetString("Familia");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public CodigosProductosBase ObtenerCodigoProducto(string comando)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(comando, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            CodigosProductosBase elemento = new CodigosProductosBase();
            if (reader.Read())
            {
                elemento.Ancho = reader.GetString("ancho");
                elemento.Aro = reader.GetString("aro");
                elemento.Avance = reader.GetString("avance");
                elemento.Codigo = reader.GetString("codigo");
                elemento.Colores = reader.GetString("colores");
                elemento.Id = reader.GetInt32("id");
                elemento.Etiqueta_x_rollo = reader.GetString("etiquetaxrollo");
                elemento.Familia = reader.GetString("familia");
                elemento.Largo = reader.GetString("largo");
                elemento.Marca = reader.GetString("marca");
                elemento.Material = reader.GetString("material");
                elemento.Modelo = reader.GetString("modelo");
                elemento.Observacion = reader.GetString("observacion");
                elemento.Salida = reader.GetString("salida");
                elemento.Tipo_producto = reader.GetString("tipo_producto");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }

        }

        public StockBase ObtenerStock(String consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            StockBase elemento = new StockBase();
            if (reader.Read())
            {
                elemento.Id = reader.GetInt32("id");
                elemento.Codigo = reader.GetString("codigo_p");
                elemento.Stock = reader.GetInt32("stock");
                elemento.Centro_costo = reader.GetString("centro_costo");
                elemento.Ubicaciones = reader.GetString("ubicacion");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public TipoMonedaBase ObtenerMoneda(String consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            TipoMonedaBase elemento = new TipoMonedaBase();
            if (reader.Read())
            {
                elemento.Id = reader.GetInt32("id");
                elemento.Nombre = reader.GetString("nombre");
                elemento.Simbolo = reader.GetString("simbolo");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public EncabezadoBase ObtenerEncabezado(String consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            EncabezadoBase elemento = new EncabezadoBase();
            if (reader.Read())
            {
                elemento.codigo_usuario = reader.GetString("codigo_usuario");
                elemento.condicion_pago = reader.GetString("condicion_pago");
                elemento.contacto = reader.GetString("contacto");
                elemento.correlativo = reader.GetInt32("correlativo_id");
                elemento.correo = reader.GetString("correo");
                elemento.direccion = reader.GetString("direccion");
                elemento.entrega = reader.GetString("entrega");
                elemento.estado = reader.GetString("estado");
                elemento.fecha = reader.GetString("fecha");
                elemento.iva = reader.GetDouble("iva");
                elemento.neto = reader.GetDouble("neto");
                elemento.razon_social = reader.GetString("razon_social");
                elemento.rut = reader.GetString("rut");
                elemento.telefono = reader.GetString("telefono");
                elemento.tipo_moneda = reader.GetString("tipo_moneda");
                elemento.total = reader.GetDouble("total");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public TipoUsuarioBase ObtenerTipoUsuario(string consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            TipoUsuarioBase elemento = new TipoUsuarioBase();
            if (reader.Read())
            {
                elemento.Id = reader.GetInt32("id");
                elemento.Descripcion = reader.GetString("descripcion");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public CentroCostoBase ObtenerCentroCosto(String consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            CentroCostoBase elemento = new CentroCostoBase();
            if (reader.Read())
            {
                elemento.Id = reader.GetInt32("id");
                elemento.Descripcion = reader.GetString("descripcion");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public PermisosModulosBase ObtenerModulosUsuario(string consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            PermisosModulosBase elemento = new PermisosModulosBase();
            if (reader.Read())
            {
                elemento.Id_tipo_usuario = reader.GetInt32("id_tipo_usuario");
                elemento.Modulos = reader.GetString("modulos");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public EstadoCotizacionBase ObtenerEstadoCotizacion(string consulta)
        {
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            EstadoCotizacionBase elemento = new EstadoCotizacionBase();
            if (reader.Read())
            {
                elemento.Id = reader.GetInt32("id");
                elemento.Descripcion = reader.GetString("descripcion");
                con.Close();
                return elemento;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public List<ProductoBase> ObtenerLista(String consulta)
        {
            List<ProductoBase> listatemp = new List<ProductoBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    ProductoBase elemento = new ProductoBase();
                    elemento.ID = reader.GetInt32("ID");
                    elemento.Descripcion = reader.GetString("Descripcion");
                    elemento.Codigo = reader.GetString("Codigo");
                    elemento.Paquete = reader.GetDouble("Paquete");
                    elemento.Familia = reader.GetString("Familia");
                    elemento.Umedida = reader.GetString("Umedida");
                    elemento.Stock = reader.GetInt32("Stock");
                    listatemp.Add(elemento);
                }

                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public List<ReporteBase> ObtenerReporte(String consulta)
        {
            List<ReporteBase> listatemp = new List<ReporteBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    ReporteBase elemento = new ReporteBase();
                    elemento.Codigo = reader.GetString("codigo_p");
                    elemento.Descripcion = reader.GetString("descripcion");
                    elemento.Valor_total = reader.GetInt32("valor_total");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch(Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<StockBase> ObtenerStockCentroCosto(String consulta)
        {
            List<StockBase> listatemp = new List<StockBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    StockBase elemento = new StockBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Codigo = reader.GetString("codigo_p");
                    elemento.Centro_costo = reader.GetString("Centro_costo");
                    elemento.Stock = reader.GetInt32("Stock");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch(Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<StockDescripcionBase> ObtenerStockDescripcionCentroCosto(String consulta)
        {
            List<StockDescripcionBase> listatemp = new List<StockDescripcionBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    StockDescripcionBase elemento = new StockDescripcionBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Codigo = reader.GetString("codigo_p");
                    elemento.Centro_costo = reader.GetString("Centro_costo");
                    elemento.Stock = reader.GetInt32("Stock");
                    elemento.Descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<StockBase> ObtenerStockUbicaciones(String consulta)
        {
            List<StockBase> listatemp = new List<StockBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    StockBase elemento = new StockBase();
                    elemento.Ubicaciones = reader.GetString("Ubicacion");
                    elemento.Stock = reader.GetInt32("Stock");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }

        }
        public List<RegistroBase> ObtenerRegistro(String consulta)
        {
            DateTime fecha;
            List<RegistroBase> listatemp = new List<RegistroBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    RegistroBase elemento = new RegistroBase();
                    elemento.codigo = reader.GetString("codigo");
                    elemento.id = reader.GetInt32("id");
                    elemento.ingreso = reader.GetInt32("ingreso");
                    elemento.egreso = reader.GetInt32("egreso");
                    fecha = reader.GetDateTime("fecha");
                    elemento.fecha = fecha.ToString("dd-MM-yyyy");
                    elemento.hora = reader.GetString("hora");
                    elemento.descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<EncabezadoBase> ObtenerEncabezados(String consulta)
        {
            DateTime fecha;
            List<EncabezadoBase> listatemp = new List<EncabezadoBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    EncabezadoBase elemento = new EncabezadoBase();
                    elemento.codigo_usuario = reader.GetString("codigo_usuario");
                    elemento.condicion_pago = reader.GetString("condicion_pago");
                    elemento.contacto = reader.GetString("contacto");
                    elemento.correlativo = reader.GetInt32("correlativo_id");
                    fecha = reader.GetDateTime("fecha");
                    elemento.fecha = fecha.ToString("dd-MM-yyyy");
                    elemento.correo = reader.GetString("correo");
                    elemento.direccion = reader.GetString("direccion");
                    elemento.entrega = reader.GetString("entrega");
                    elemento.estado = reader.GetString("estado");
                    elemento.observacion_estado = reader.GetString("observacion_estado");
                    elemento.iva = reader.GetDouble("iva");
                    elemento.neto = reader.GetDouble("neto");
                    elemento.razon_social = reader.GetString("razon_social");
                    elemento.rut = reader.GetString("rut");
                    elemento.telefono = reader.GetString("telefono");
                    elemento.tipo_moneda = reader.GetString("tipo_moneda");
                    elemento.total = reader.GetDouble("total");

                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<DetalleBase> ObtenerDetalles(String consulta)
        {
            List<DetalleBase> listatemp = new List<DetalleBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    DetalleBase elemento = new DetalleBase();
                    elemento.id = reader.GetInt32("id");
                    elemento.correlativo = reader.GetInt32("correlativo");
                    elemento.cantidad = reader.GetInt64("cantidad");
                    elemento.descripcion = reader.GetString("descripcion");
                    elemento.precio_unitario = reader.GetDouble("precio_unitario");
                    elemento.codigo = reader.GetString("codigo");
                    elemento.subtotal = reader.GetDouble("subtotal");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public List<TipoMonedaBase> ObtenerMonedas(String consulta)
        {
            List<TipoMonedaBase> listatemp = new List<TipoMonedaBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    TipoMonedaBase elemento = new TipoMonedaBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Nombre = reader.GetString("nombre");
                    elemento.Simbolo = reader.GetString("simbolo");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public List<TipoCotizacionBase> ObtenerTipoCotizaciones(String consulta)
        {
            List<TipoCotizacionBase> listatemp = new List<TipoCotizacionBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    TipoCotizacionBase elemento = new TipoCotizacionBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public List<CentroCostoBase> ObtenerListaCentroCostos(String consulta)
        {
            List<CentroCostoBase> listatemp = new List<CentroCostoBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    CentroCostoBase elemento = new CentroCostoBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<TipoUsuarioBase> ObtenerListaTipoUsuario(String consulta)
        {
            List<TipoUsuarioBase> listatemp = new List<TipoUsuarioBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    TipoUsuarioBase elemento = new TipoUsuarioBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public List<UsuarioBase> ObtenerListaUsuario(String consulta)
        {
            List<UsuarioBase> listatemp = new List<UsuarioBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    UsuarioBase elemento = new UsuarioBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Usuario = reader.GetString("usuario");
                    elemento.Password = reader.GetString("password");
                    elemento.Nombre = reader.GetString("nombre");
                    elemento.Apellido = reader.GetString("apellido");
                    elemento.Tipo_usuario = reader.GetString("tipo_usuario");
                    elemento.Cargo = reader.GetString("cargo");
                    elemento.Estado = reader.GetString("estado");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<PermisosModulosBase> ObtenerListaModulosUsuario(String consulta)
        {
            List<PermisosModulosBase> listatemp = new List<PermisosModulosBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    PermisosModulosBase elemento = new PermisosModulosBase();
                    elemento.Id_tipo_usuario = reader.GetInt32("id_tipo_usuario");
                    elemento.Modulos = reader.GetString("modulos");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
        public Dictionary<string,string> ObtenerListaCodigosProductos(string consulta)
        {
            Dictionary<string, string> listatemp = new Dictionary<string, string>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    CodigosProductosBase elemento = new CodigosProductosBase();
                    elemento.Tipo_producto = reader.GetString("tipo_producto");
                    elemento.Ancho = reader.GetString("ancho");
                    elemento.Aro = reader.GetString("aro");
                    elemento.Avance = reader.GetString("avance");
                    elemento.Codigo = reader.GetString("codigo");
                    elemento.Colores = reader.GetString("colores");
                    elemento.Etiqueta_x_rollo = reader.GetString("etiquetaxrollo");
                    elemento.Familia = reader.GetString("familia");
                    elemento.Id = reader.GetInt32("id");
                    elemento.Largo = reader.GetString("largo");
                    elemento.Marca = reader.GetString("marca");
                    elemento.Material = reader.GetString("material");
                    elemento.Modelo = reader.GetString("modelo");
                    elemento.Observacion = reader.GetString("observacion");
                    elemento.Salida = reader.GetString("salida");
                    switch (elemento.Tipo_producto)
                    {
                        case "Etiqueta":
                            listatemp.Add(elemento.Id.ToString(), "Etiqueta Ancho " + elemento.Ancho + " Avance " + elemento.Avance + " Material " + elemento.Material +
                                " Aro " + elemento.Aro + " Etiq por Rollo " + elemento.Etiqueta_x_rollo + " Colores " + elemento.Colores + " Salida " + elemento.Salida + " Observacion " + elemento.Observacion + "");
                            break;
                        case "Cinta":
                            listatemp.Add(elemento.Id.ToString(), "Cinta Ancho " + elemento.Ancho + " Largo " + elemento.Largo + " Material " + elemento.Material + " Aro " + elemento.Aro + " Observacion " + elemento.Observacion + "");
                            break;
                        case "Hardware":
                            listatemp.Add(elemento.Id.ToString(), "Hardware Marca " + elemento.Marca + " Modelo " + elemento.Modelo + " Familia " + elemento.Familia + " Observacion " + elemento.Observacion + "");
                            break;
                        default: break;
                    }   
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }

        public List<EstadoCotizacionBase> ObtenerListaEstadosCotizacion(String consulta)
        {
            List<EstadoCotizacionBase> listatemp = new List<EstadoCotizacionBase>();
            con.Open();
            MySqlCommand sqlcmd = new MySqlCommand(consulta, con);
            MySqlDataReader reader = sqlcmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    EstadoCotizacionBase elemento = new EstadoCotizacionBase();
                    elemento.Id = reader.GetInt32("id");
                    elemento.Descripcion = reader.GetString("descripcion");
                    listatemp.Add(elemento);
                }
                con.Close();
                return listatemp;
            }
            catch (Exception ex)
            {
                con.Close();
                ex.Message.ToString();
                return null;
            }
        }
    }
}
