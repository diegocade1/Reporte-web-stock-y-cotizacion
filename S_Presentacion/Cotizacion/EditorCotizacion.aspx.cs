using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion.Cotizacion
{
    public partial class EditorCotizacion : System.Web.UI.Page
    {
        public List<string> detallestring;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                string cadena = "Debe iniciar sesion primero.";
                int count = cadena.Count();
                string mensaje = QueryStringModule.Encrypt(cadena);
                Response.Redirect("../Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if(Session["encabezado"]!= null && Session["detalle"]!=null)
                {
                    //TipoMoneda moneda = new TipoMoneda();
                    Encabezado encabezado = (Encabezado)Session["encabezado"];
                    List<Detalle> detalles = (List<Detalle>)Session["detalle"];
                    txtCondicionesPago.Value = encabezado.CondicionPago;
                    txtContacto.Value = encabezado.Contacto;
                    txtCorreo.Value = encabezado.Correo;
                    txtDireccion.Value = encabezado.Direccion;
                    txtEntrega.Value = encabezado.Entrega;
                    txtFecha_.Value = DateTime.Parse(encabezado.Fecha).ToShortDateString();
                    txtIVA.Value = encabezado.Iva.ToString();
                    //moneda.Nombre = encabezado.Tipo_moneda;
                    //moneda.ReadNombre();
                    txtMoneda.DataBind();
                    System.Web.UI.WebControls.ListItem select = txtMoneda.Items.FindByText(encabezado.Tipo_moneda);
                    select.Selected = true;
                    //txtMoneda.Value = encabezado.Tipo_moneda;
                    txtNeto.Value = encabezado.Neto.ToString();
                    txtNombre.Value = encabezado.Razon_social;
                    txtRut.Value = encabezado.Rut;
                    txtTelefono.Value = encabezado.Telefono;
                    txtTotal.Value = encabezado.Total.ToString();
                    detallestring = ListaDetallesString(detalles);
                    objetoDetalles.Value = ListaStringToString(detallestring);

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //if (!this.IsPostBack)
                    //{
                    Session["Reset"] = true;
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                    int timeout = (int)section.Timeout.TotalMinutes * 60000;
                    ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                    //}
                }
                else
                {
                    string cadena = "Seleccionar cotizacion primero.";
                    //string cadena = "Debe iniciar sesion primero.";
                    int count = cadena.Count();
                    string mensaje = QueryStringModule.Encrypt(cadena);
                    Response.Redirect("../Inicio.aspx"+mensaje.Replace("enc","mensaje"), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            { 
            string direccion_logo = "Av. Santa Rosa 4470, San Joaquin, Santiago - Tel: +56225526276";
            TipoMoneda moneda = new TipoMoneda();
            Usuario creador = new Usuario();
            moneda.ID = Convert.ToInt32(Request.Form.Get(txtMoneda.UniqueID));
            moneda.Read();
            Encabezado encabezado = (Encabezado)Session["encabezado"];
            string correo = Request.Form.Get(txtCorreo.UniqueID);
            string rut = Request.Form.Get(txtRut.UniqueID);
            string razon = Request.Form.Get(txtNombre.UniqueID);
            string contacto = Request.Form.Get(txtContacto.UniqueID);
            string condicionpago = Request.Form.Get(txtCondicionesPago.UniqueID);
            string entrega = Request.Form.Get(txtEntrega.UniqueID);
            string direccion = Request.Form.Get(txtDireccion.UniqueID);
            string fecha = Request.Form.Get(txtFecha_.UniqueID);
            string neto = Request.Form.Get(txtNeto.UniqueID);
            string telefono = Request.Form.Get(txtTelefono.UniqueID);
            string total = Request.Form.Get(txtTotal.UniqueID);
            Detalle detalle = new Detalle();
            detalle.Correlativo = encabezado.Correlativo;
            encabezado.CondicionPago = condicionpago;
            encabezado.Contacto = contacto;
            encabezado.Correo = correo;
            encabezado.Entrega = entrega;
            encabezado.Direccion = direccion;
            encabezado.Estado = "Pendiente";
            encabezado.Observacion_estado = " ";
            //encabezado.Fecha = fecha.ToString("dd/MM/yyyy");
            encabezado.Fecha = fecha;
            //encabezado.Iva = Convert.ToInt32(txtIVA.Value);
            encabezado.Tipo_moneda = moneda.Nombre;
            encabezado.Iva = 0;
            encabezado.Neto = Convert.ToDouble(neto.ToString().Replace(".", ","));
            encabezado.Razon_social = razon;
            encabezado.Rut = rut;
            encabezado.Telefono = telefono;
            encabezado.Total = Convert.ToDouble(total.ToString().Replace(".", ","));
            creador.Usuario_ = encabezado.Codigo_usuario;
            creador.Read();
                if (encabezado.Update())
                {
                    detalle.LimpiarDetalleCorrelativo();
                    List<string> lista = ListValues();
                    string correlativo = encabezado.Correlativo.ToString();
                    CreacionDetalle(correlativo, lista);
                    byte[] bytesarray = null;
                    using (var ms = new MemoryStream())
                    {
                        using (var document = new Document(PageSize.A4, 20f, 20f, 45f, 35f)) //PageSize.A4, 20f, 20f, 65f, 35f--PageSize.A4, 20f, 20f, 75f, 35f
                        {
                            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
                            {
                                document.Open();
                                PdfContentByte canvas = writer.DirectContent;
                                writer.CompressionLevel = 0;
                                canvas.SaveState();
                                canvas.BeginText();
                                canvas.MoveText(118, 773);
                                canvas.SetFontAndSize(BaseFont.CreateFont(), 8);
                                canvas.ShowText(direccion_logo);
                                canvas.EndText();
                                canvas.RestoreState();
                                using (var strreader = new StringReader(HTMLPage(ListValues(), encabezado, correlativo, creador, moneda).ToString()))
                                {
                                    var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/pdf/logo.png"));
                                    logo.ScaleAbsoluteWidth(450);
                                    logo.ScaleAbsoluteHeight(100);
                                    logo.SetAbsolutePosition(0, 750);
                                    document.Add(logo);
                                    //set factories
                                    HtmlPipelineContext htmlcontext = new HtmlPipelineContext(null);
                                    htmlcontext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                                    //set css
                                    ICSSResolver cssresolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                                    cssresolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath("~/pdf/estilo.css"), true);
                                    //export
                                    IPipeline pipeline = new CssResolverPipeline(cssresolver, new HtmlPipeline(htmlcontext, new PdfWriterPipeline(document, writer)));
                                    var worker = new XMLWorker(pipeline, true);
                                    var xmlparse = new XMLParser(true, worker);
                                    //var xmlparse = new XMLParser();
                                    xmlparse.Parse(strreader);
                                    xmlparse.Flush();
                                }
                                document.Close();
                            }
                        }
                        bytesarray = ms.ToArray();
                        ms.Close();
                        // clears all content output from the buffer stream                 
                        Response.Clear();
                        // gets or sets the http mime type of the output stream.
                        Response.ContentType = "application/pdf";
                        // adds an http header to the output stream
                        Response.AddHeader("content-disposition", "attachment; filename=cotizacion_" + correlativo + ".pdf");

                        //gets or sets a value indicating whether to buffer output and send it after
                        // the complete response is finished processing.
                        Response.Buffer = true;
                        // sets the cache-control header to one of the values of system.web.httpcacheability.
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        // writes a string of binary characters to the http output stream. it write the generated bytes .
                        Response.BinaryWrite(bytesarray);
                        // sends all currently buffered output to the client, stops execution of the
                        // page, and raises the system.web.httpapplication.endrequest event.

                        Response.Flush(); // sends all currently buffered output to the client.
                        Response.SuppressContent = true;  // gets or sets a value indicating whether to send http content to the client.
                        HttpContext.Current.ApplicationInstance.CompleteRequest(); // causes asp.net to bypass all events and filtering in the http pipeline chain of execution and directly execute the endrequest event.
                                                                                   // closes the socket connection to a client. it is a necessary step as you must close the response after doing work.its best approach.
                        Response.Close();
                    }
                    Session["encabezado"] = null;
                    Session["detalle"] = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Cotizacion modificada" + "');", true);
                }
        }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
        }
    }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarCodigo.Value != "")
            {
                Session["palabra"] = txtBuscarCodigo.Value;
                gvCodigos.DataSourceID = "";
                gvCodigos.DataSource = odsBusquedaCodigo;
                gvCodigos.DataBind();
                txtBuscarCodigo.Value = "";
                btnQuitarBusqueda.Visible = true;
                Session["palabra"] = null;
            }
            else
            {
                string mensaje = "Ingrese una palabra correcta";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void btnQuitarBusqueda_Click(object sender, EventArgs e)
        {
            gvCodigos.DataSourceID = "";
            gvCodigos.DataSource = odsCodigos;
            gvCodigos.DataBind();
            btnQuitarBusqueda.Visible = false;
        }

        protected void gvCodigos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCodigos.PageIndex = e.NewPageIndex;
            gvCodigos.DataBind();
        }

        private StringBuilder HTMLPage(List<string> detalle, Encabezado obj, string correlativo, Usuario usuario, TipoMoneda moneda)
        {
            string voltaje = " La regulacion de voltaje permitido del cabezal es de _____ ";
            DateTime fecha = Convert.ToDateTime(obj.Fecha);
            string linea;
            string[] arr;
            //string img = System.Web.HttpContext.Current.Server.MapPath("~/pdf/logo.png");
            //string css = System.Web.HttpContext.Current.Server.MapPath("~/pdf/estilo.css");
            StringBuilder sb = new StringBuilder();
            //sb.Append("<!doctype html>");
            //sb.Append("<html>");
            //    sb.Append("<head>");
            //        sb.Append("<title>PDF</title>");
            //        sb.Append("<meta charset="+""+"utf - 8"+""+"/>");
            //        sb.Append("<link rel="+""+"stylesheet"+""+" href="+""+img+""+"/>");
            //    sb.Append("</head>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<body>");
            sb.Append("<div class=" + "" + "contenedor1" + "" + " >");
            //sb.Append("<img class="+""+"imagen"+""+" src="+""+css+""+" alt="+""+"Logotipo"+""+"/>");
            sb.Append("<div class=" + "" + "cotizacion" + "" + " >");
            sb.Append("<h2>Cotizacion N°: " + correlativo + "</h2>");
            sb.Append("<h3>" + fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es-CL")) + ", " + fecha.Day + " de " + fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CL")) + " del " + fecha.Year + "</h3>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("</div>");
            sb.Append("<header>");
            sb.Append("<div class=" + "" + "derecha" + "" + " >");
            sb.Append("<h1><b>Razon Social:</b> " + obj.Razon_social + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Rut:</b> " + obj.Rut + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Contacto:</b> " + obj.Contacto + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Direccion:</b> " + obj.Direccion + "</h1>");
            sb.Append("<hr/>");
            sb.Append("</div>");
            sb.Append("<div class=" + "" + "izquierda" + "" + " >");
            sb.Append("<h1><b>Telefono:</b> " + obj.Telefono + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Correo:</b> " + obj.Correo + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Entrega:</b> " + obj.Entrega + "</h1>");
            sb.Append("<hr/>");
            sb.Append("<h1><b>Condicion de Pago:</b> " + obj.CondicionPago + "</h1>");
            sb.Append("<hr/>");
            sb.Append("</div>");
            sb.Append("</header>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<div class=" + "" + "principal" + "" + " >");
            sb.Append("<table class=" + "" + "tablaFormulario" + "" + " >");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th class=" + "" + "cantidad" + "" + " ><b>Cant.</b></th>");
            sb.Append("<th><b>Descripcion</b></th>");
            sb.Append("<th class=" + "" + "normal" + "" + " ><b>Precio Unit.</b></th>");
            sb.Append("<th class=" + "" + "codigo" + "" + " ><b>Codigo</b></th>");
            sb.Append("<th class=" + "" + "normal" + "" + " ><b>Subtotal</b></th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < detalle.Count; i++)
            {
                linea = detalle[i].ToString();
                arr = linea.Split(';');
                string cantidad = Convert.ToInt32(arr[0]).ToString("###,###").Replace(",", ".");
                string precio = Convert.ToDouble(arr[2].Replace(".", ",")).ToString("###,##0.##");
                string subtotal = Convert.ToDouble(arr[4].Replace(".", ",")).ToString("###,##0.##");
                sb.Append("<tr>");
                sb.Append("<td><p>" + cantidad + "</p></td>");
                sb.Append("<td><p>" + arr[1] + "</p></td>");
                sb.Append("<td><p>" + moneda.Simbolo + "" + precio + "</p></td>");
                sb.Append("<td><p>" + arr[3] + "</p></td>");
                sb.Append("<td><p>" + moneda.Simbolo + "" + subtotal + "</p></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            /*
                            sb.Append("<tfoot>");
                                sb.Append("<tr>");
                                    sb.Append("<td colspan="+""+"2"+""+ " rowspan="+""+"3"+""+" >" +
                                        "<h4><b>Condiciones Generales</b></h4>"+
                                        "<h4>*Validez de la Cotización: 10 días.</h4>" +
                                        "<h4>*Por razones técnicas los pedidos se consideran cumplidos con una diferencia de +/- 10% respecto</h4>" +
                                        "<h4>a las cantidades solicitadas.</h4>" +
                                        "<h4>*Cualquier modificación a la orden de compra deberá ser informada, por escrito, a ATC Ingeniería en un plazo</h4>" +
                                        "<h4>maximo de 48 horas dsede su emisión</h4>" +
                                        "<h4>*Los precios ofertados consideran entrega dentro de la Región Metropolitana, salvo acuerdo expreso entre las partes</h4>" +
                                        "<h4>*Todas las matrices producto del proceso de impresión son propiedad de ATC Ingenieria.</h4>" +
                                        "<h4>*Los gastos incurridos en desarrollos solicitados y que finalmente no lleguen a fabricarse serán facturados al cliente.</h4>" +
                                        "<h4>*ATC Ingeniería no aceptará devoluciones transcurridos 15 días corridos desde la entrega. Asi como tambien sólo.</h4>" +
                                        "<h4>se recibirán las mercaderias que se encuentren en las condiciones en las que fueron despachadas.</h4>" +
                                        "</td>");
                                    sb.Append("<td colspan=" + "" + "2" + "" + " ><b>Neto</b></td>");
                                    sb.Append("<td><p>"+obj.Neto.ToString("###,###").Replace(",", ".") + "</p></td>");
                                sb.Append("</tr>");
                                //sb.Append("<tr>");
                                    ////sb.Append("<td colspan=" + "" + "2" + "" + "></td>");
                                    //sb.Append("<td colspan=" + "" + "2" + "" + " ><b>19% IVA</b></td>");
                                    //sb.Append("<td><p>"+obj.Iva.ToString("###,###").Replace(",", ".") + "</p></td>");
                                //sb.Append("</tr>");
                                sb.Append("<tr>");
                                    //sb.Append("<td colspan=" + "" + "2" + "" + "></td>");
                                    sb.Append("<td colspan=" + "" + "2" + "" + " rowspan="+""+"1"+""+" ><b>Total</b></td>");
                                    sb.Append("<td><p>"+obj.Total.ToString("###,###").Replace(",", ".") + "</p></td>");
                                sb.Append("</tr>");
                            sb.Append("</tfoot>");
            */
            sb.Append("</table>");
            sb.Append("</div>");
            double limite = detalle.Count * 1.75;
            limite = 17.5 - limite;
            for (double i = 1.75; i <= limite; i = i + 1.75)
            {
                if (i.ToString().Contains("75"))
                {
                    sb.Append("<br/>");
                }
                else
                {
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                }

            }
            sb.Append("<div class=" + "" + "principal" + "" + " >");
            sb.Append("<h6>Datos Bancarios:</h6>");
            sb.Append("<h6>Rut: 76.076.930-4 Productos y Asesorias Computacionales Ltda.</h6>");
            sb.Append("<h6>Banco Santander - Cta.Cte 01-92646-2</h6>");
            sb.Append("<table class=" + "" + "tablaFormulario" + "" + " >");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th></th>");
            sb.Append("<th></th>");
            sb.Append("<th class=" + "" + "normal" + "" + " ></th>");
            sb.Append("<th class=" + "" + "codigo" + "" + " ></th>");
            sb.Append("<th class=" + "" + "normal" + "" + " ></th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tr>");
            sb.Append("<td colspan=" + "" + "2" + "" + " rowspan=" + "" + "3" + "" + " >" +
                "<h4><b>Condiciones Generales</b></h4>" +
                "<h4>*Validez de la Cotización: 10 días.</h4>" +
                "<h4>*Por razones técnicas los pedidos se consideran cumplidos con una diferencia de +/- 10% respecto a las cantidades solicitadas.</h4>" +
                //"<h4>.</h4>" +
                "<h4>*Cualquier modificación a la orden de compra deberá ser informada, por escrito, a ATC Ingeniería en un plazo</h4>" +
                "<h4>maximo de 48 horas dsede su emisión</h4>" +
                "<h4>*Los precios ofertados consideran entrega dentro de la Región Metropolitana, salvo acuerdo expreso entre las partes</h4>" +
                "<h4>*Todas las matrices producto del proceso de impresión son propiedad de ATC Ingenieria.</h4>" +
                "<h4>*Los gastos incurridos en desarrollos solicitados y que finalmente no lleguen a fabricarse serán facturados al cliente.</h4>" +
                "<h4>*ATC Ingeniería no aceptará devoluciones transcurridos 15 días corridos desde la entrega. Asi como tambien sólo.</h4>" +
                "<h4>*La garantía del cabezal 3 meses." + voltaje + "</h4>" +
                "<h4>*Garantía de Equipo 1 año.</h4>" +
                "<h4>*Mantención garantía 3 meses.</h4>" +
                "</td>");
            sb.Append("<td colspan=" + "" + "2" + "" + " ><b>Neto</b></td>");
            sb.Append("<td><p>" + moneda.Simbolo + "" + obj.Neto.ToString("###,###.##") + "</p></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            //sb.Append("<td colspan=" + "" + "2" + "" + "></td>");
            //sb.Append("<td colspan=" + "" + "2" + "" + " ><b>19% IVA</b></td>");
            sb.Append("<td colspan=" + "" + "2" + "" + " ></td>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            //sb.Append("<td colspan=" + "" + "2" + "" + "></td>");
            sb.Append("<td colspan=" + "" + "2" + "" + " ><b>Total</b><br/><b><h5>(Cotizacion realizada en " + obj.Tipo_moneda.ToString() + ")"+ " Valores no incluyen IVA" + "</h5></b></td>");
            sb.Append("<td><p>" + moneda.Simbolo + "" + obj.Total.ToString("###,###.##") + "</p></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("<footer>");
            sb.Append("<h5>Sin otro particular y quedando a su entera disposición, le saluda cordialmente</h5>");
            sb.Append("<h5>" + usuario.Nombre + " " + usuario.Apellido + "</h5>");
            sb.Append("<h5>" + usuario.Cargo + "</h5>");
            sb.Append("</footer>");
            sb.Append("</div>");
            sb.Append("</body>");
            //sb.Append("</html>");
            return sb;
        }

        private void CreacionCodigo(string descripcion, string codigo_id)
        {
            CodigosProductos codigos = new CodigosProductos();
            int id;
            string linea;
            if (descripcion != "")
            {
                int.TryParse(codigo_id, out id);
                codigos.ID = id;
                if (!codigos.Read())
                {
                    if (descripcion.Contains("Etiqueta"))
                    {
                        //int strLength = descripcion.Length;
                        //for (int i = 0; i < strLength; i++)
                        //{
                        //    descripcion = descripcion.Replace(" ", "");
                        //}
                        //descripcion = descripcion.replace(" ", "");
                        linea = descripcion.Replace("Etiqueta ", "");
                        linea = linea.Replace("Ancho ", "");
                        linea = linea.Replace(" Avance ", ";");
                        linea = linea.Replace(" Material ", ";");
                        linea = linea.Replace(" Aro ", ";");
                        linea = linea.Replace(" Etiq por Rollo ", ";");
                        linea = linea.Replace(" Colores ", ";");
                        linea = linea.Replace(" Salida ", ";");
                        //linea = linea.Replace(" Observacion ", ";") + ";" + codigo;
                        linea = linea.Replace(" Observacion ", ";");
                        string[] array = linea.Split(';');
                        codigos.Ancho = array[0];
                        codigos.Avance = array[1];
                        codigos.Material = array[2];
                        codigos.Aro = array[3];
                        codigos.Etiqueta_x_rollo = array[4];
                        codigos.Colores = array[5];
                        codigos.Salida = array[6];
                        codigos.Observacion = array[7];
                        codigos.Codigo = " ";
                        codigos.Tipo_producto = "Etiqueta";
                        //-----------------------------------
                        codigos.Familia = "";
                        codigos.Largo = "";
                        codigos.Marca = "";
                        codigos.Modelo = "";
                        codigos.CreateEtiqueta();
                        //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                    }
                    else if (descripcion.Contains("Cinta"))
                    {
                        //int strLength = descripcion.Length;
                        //for (int i = 0; i < strLength; i++)
                        //{
                        //    descripcion = descripcion.Replace(" ", "");
                        //}
                        //descripcion = descripcion.replace(" ", "");
                        linea = descripcion.Replace("Cinta ", "");
                        linea = linea.Replace("Ancho ", "");
                        linea = linea.Replace(" Largo ", ";");
                        linea = linea.Replace(" Material ", ";");
                        linea = linea.Replace(" Aro ", ";");
                        //linea = linea.Replace(" Observacion ", ";") + ";" + codigo;
                        linea = linea.Replace(" Observacion ", ";");
                        string[] array = linea.Split(';');
                        codigos.Ancho = array[0];
                        codigos.Largo = array[1];
                        codigos.Material = array[2];
                        codigos.Aro = array[3];
                        codigos.Observacion = array[4];
                        codigos.Codigo = " ";
                        codigos.Tipo_producto = "Cinta";
                        //-------------------------------------------
                        codigos.Avance = "";
                        codigos.Colores = "";
                        codigos.Etiqueta_x_rollo = "";
                        codigos.Familia = "";
                        codigos.Marca = "";
                        codigos.Modelo = "";
                        codigos.Salida = "";
                        codigos.CreateCinta();
                        //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                    }
                    else if (descripcion.Contains("Hardware"))
                    {
                        //int strLength = descripcion.Length;
                        //for (int i = 0; i < strLength; i++)
                        //{
                        //    descripcion = descripcion.Replace(" ", "");
                        //}
                        //descripcion = descripcion.replace(" ", "");
                        linea = descripcion.Replace("Hardware ", "");
                        linea = linea.Replace("Marca ", "");
                        linea = linea.Replace(" Modelo ", ";");
                        linea = linea.Replace(" Familia ", ";");
                        //linea = linea.Replace(" Observacion ", ";") + ";" + codigo;
                        linea = linea.Replace(" Observacion ", ";");
                        string[] array = linea.Split(';');
                        codigos.Marca = array[0];
                        codigos.Modelo = array[1];
                        codigos.Familia = array[2];
                        codigos.Observacion = array[3];
                        codigos.Codigo = " ";
                        codigos.Tipo_producto = "Hardware";
                        //-------------------------------------------
                        codigos.Avance = "";
                        codigos.Colores = "";
                        codigos.Etiqueta_x_rollo = "";
                        codigos.Largo = "";
                        codigos.Ancho = "";
                        codigos.Aro = "";
                        codigos.Salida = "";
                        codigos.Material = "";
                        codigos.CreateHardware();
                        //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                    }
                }
            }
            else
            {
                string mensaje = "El producto: " + descripcion + " no ha sido registrado porque no tienen un codigo valido, la cotizacion sera generada con el producto sin el codigo";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        private List<string> ListaDetallesString(List<Detalle> lista)
        {
            List<string> listatemp = new List<string>();
            string linea;
            foreach(Detalle detalle in lista)
            {
                linea = detalle.Cantidad + ";" + detalle.Descripcion + ";" + detalle.PrecioUnitario + ";" + detalle.Codigo + ";" + detalle.Subtotal;
                listatemp.Add(linea);
                linea = "";
            }
            return listatemp;
        }

        private string ListaStringToString(List<string> lista)
        {
            int intCount;
            string strFinal = "";

            for (intCount = 0; intCount <= lista.Count - 1; intCount++)
            {
                if (intCount > 0)
                {
                    strFinal += "~";
                }

                strFinal += lista[intCount].ToString();
            }

            return strFinal;
        }

        private void PoblarDetallesTabla(List<string> detalles)
        {
            int index = 0;
            string key;
            foreach(string linea in detalles)
            {
                index++;
                key = "onload" + index + "";
                //Page.ClientScript.RegisterStartupScript(GetType(), key, "PoblarDetalles(" + linea + ");", true);
                //Page.ClientScript.RegisterStartupScript(GetType(), key, "window.onload = function(){ PoblarDetalles(" + linea + ") };", true);
                //Page.ClientScript.RegisterClientScriptBlock(GetType(),"onload", "window.onload = function(){ PoblarDetalles(" + linea + "); }", true);
                //ScriptManager.RegisterStartupScript(this, GetType(),key, "window.onload = function(){ PoblarDetalles(" + linea + ") };", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), key, "$(function () { PoblarDetalles(" + linea + "); });", true);
            }
        }

        private List<string> ListValues()
        {
            List<string> listValues = new List<string>();
            string linea = "";
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("txtCantidad"))
                {
                    linea = linea + Request.Form[key] + ";";
                }
                else if (key.Contains("txtDescripcion"))
                {
                    linea = linea + Request.Form[key] + ";";
                }
                else if (key.Contains("txtPrecioUnit"))
                {
                    linea = linea + Request.Form[key] + ";";
                }
                else if (key.Contains("txtCodigo"))
                {
                    linea = linea + Request.Form[key] + ";";
                }
                else if (key.Contains("txtSubTotal"))
                {
                    linea = linea + Request.Form[key];
                    listValues.Add(linea);
                    linea = "";
                }
            }
            return listValues;
        }
        private void CreacionDetalle(string correlativo, List<string> lista)
        {
            Detalle obj2 = new Detalle();
            string linea2;
            string[] arr;
            for (int i = 0; i < lista.Count; i++)
            {
                linea2 = lista[i].ToString();
                arr = linea2.Split(';');
                obj2.Correlativo = Convert.ToInt32(correlativo);
                obj2.Cantidad = Convert.ToInt32(arr[0]);
                obj2.Descripcion = arr[1];
                obj2.PrecioUnitario = Convert.ToDouble(arr[2].ToString().Replace(".", ","));
                //obj2.PrecioUnitario = Convert.ToDouble(arr[2].ToString());
                obj2.Codigo = arr[3];
                obj2.Subtotal = Convert.ToDouble(arr[4].ToString().Replace(".", ","));
                //obj2.Subtotal = Convert.ToDouble(arr[4].ToString());
                CreacionCodigo(obj2.Descripcion, obj2.Codigo);
                obj2.Create();
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (txtBuscarCliente.Value != "")
            {
                Session["palabraCliente"] = txtBuscarCliente.Value;
                gvClientes.DataSourceID = "";
                gvClientes.DataSource = odsBusquedaCliente;
                gvClientes.DataBind();
                txtBuscarCodigo.Value = "";
                btnQuitarFiltroCliente.Visible = true;
                Session["palabraCliente"] = null;
            }
            else
            {
                string mensaje = "Ingrese una palabra correcta";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void btnQuitarFiltroCliente_Click(object sender, EventArgs e)
        {
            gvClientes.DataSourceID = "";
            gvClientes.DataSource = odsClientes;
            gvClientes.DataBind();
            btnQuitarFiltroCliente.Visible = false;
        }
    }
}