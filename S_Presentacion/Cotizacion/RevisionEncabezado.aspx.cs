using ClosedXML.Excel;
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
    public partial class Revision : System.Web.UI.Page
    {
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
                if(Session["estado"] != null)
                {
                    Session["estado"] = null;
                }
                else if (Request.QueryString["mensaje"] != null)
                {
                    string mensaje = QueryStringModule.Decrypt(Request.QueryString["mensaje"]);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje.Replace(".", "").Replace("%20", " ") + "');", true);
                }

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
        }

        protected void gvCotizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            TipoMoneda moneda = new TipoMoneda();
            string direccion_logo = "Av. Santa Rosa 4470, San Joaquin, Santiago - Tel: +56225526276";
            switch (e.CommandName)
            {
                case "Select":
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow = gvCotizacion.Rows[index];
                    TableCell corr_id = selectedRow.Cells[0];
                    string correlativo = corr_id.Text;
                    Encabezado encabezado = new Encabezado();
                    Usuario creador = new Usuario();
                    ColeccionDetalle detalles = new ColeccionDetalle();
                    encabezado.Correlativo = Convert.ToInt32(correlativo);
                    encabezado.Read();
                    moneda.Nombre = encabezado.Tipo_moneda;
                    moneda.ReadNombre();
                    creador.Usuario_ = encabezado.Codigo_usuario;
                    creador.Read();
                    List<Detalle> list = detalles.ListaDetalle(correlativo);
                    try
                    {
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
                                    using (var strreader = new StringReader(HTMLPage(list, encabezado, correlativo, creador, moneda).ToString()))
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
                                        xmlparse.Parse(strreader);
                                        xmlparse.Flush();
                                    }
                                    document.Close();
                                }
                            }
                            bytesarray = ms.ToArray();
                            ms.Close();
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment; filename=cotizacion_" + correlativo + "_reimpresion.pdf");
                            Response.Buffer = true;
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(bytesarray);
                            Response.Flush();
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            Response.Close();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        break;
                    }                
                //case "Edit":
                //    try
                //    {
                //        int index2 = Convert.ToInt32(e.CommandArgument);
                //        GridViewRow selectedRow2 = gvCotizacion.Rows[index2];
                //        TableCell correlativo_id = selectedRow2.Cells[0];
                //        //TableCell razon_social = selectedRow2.Cells[1];
                //        //TableCell rut = selectedRow2.Cells[2];
                //        //TableCell contacto = selectedRow2.Cells[3];
                //        //TableCell fecha = selectedRow2.Cells[4];
                //        TableCell telefono = selectedRow2.Cells[5];
                //        TableCell correo = selectedRow2.Cells[6];
                //        //TableCell condicion_pago = selectedRow2.Cells[7];
                //        //TableCell entrega = selectedRow2.Cells[8];
                //        //TableCell direccion = selectedRow2.Cells[9];
                //        //TableCell tipo_moneda = selectedRow2.Cells[10];
                //        TableCell estado = selectedRow2.Cells[11];
                //        //TableCell codigo_usuario = selectedRow2.Cells[12];
                //        //TableCell neto = selectedRow2.Cells[13];
                //        //TableCell iva = selectedRow2.Cells[14];
                //        //TableCell total = selectedRow2.Cells[15];
                //        Encabezado encabezado2 = new Encabezado();
                //        encabezado2.Correlativo = Convert.ToInt32(correlativo_id.Text);
                //        encabezado2.Read();
                //        encabezado2.Telefono = telefono.Text;
                //        encabezado2.Correo = correo.Text;
                //        encabezado2.Estado = estado.Text;
                //        encabezado2.Update();
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                //        break;
                //    }
                    
                //case "Delete":
                //    int index2 = Convert.ToInt32(e.CommandArgument);
                //    GridViewRow selectedRow2 = gvCotizacion.Rows[index2];
                //    TableCell corr_id2 = selectedRow2.Cells[0];
                //    string correlativo2 = corr_id2.Text;
                //    Encabezado encabezado2 = new Encabezado();
                //    encabezado2.Correlativo = Convert.ToInt32(correlativo2);
                //    encabezado2.Delete();
                //   break;
                case "Details":
                    int index3 = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow3 = gvCotizacion.Rows[index3];
                    TableCell corr_id2 = selectedRow3.Cells[0];
                    string correlativo2 = corr_id2.Text;
                    Session["cotizacion"] = correlativo2;
                    Response.Redirect("../Cotizacion/RevisionDetalles.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    break;
                case "Excel":
                    int index4 = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow4 = gvCotizacion.Rows[index4];
                    TableCell corr_id3 = selectedRow4.Cells[0];
                    string correlativo3 = corr_id3.Text;
                    Encabezado encabezado3 = new Encabezado();
                    Usuario creador2 = new Usuario();
                    ColeccionDetalle detalles2 = new ColeccionDetalle();
                    encabezado3.Correlativo = Convert.ToInt32(correlativo3);
                    encabezado3.Read();
                    moneda.Nombre = encabezado3.Tipo_moneda;
                    moneda.ReadNombre();
                    creador2.Usuario_ = encabezado3.Codigo_usuario;
                    creador2.Read();
                    List<Detalle> list2 = detalles2.ListaDetalle(correlativo3);
                    try
                    {
                        XLWorkbook workbook = DocumentoExcel(list2,encabezado3,correlativo3, creador2, moneda);
                        // Prepare the response
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=\"cotizacion_excel_"+correlativo3+".xlsx\"");
                        //Response.AddHeader("content-disposition", "attachment;filename=cotizacion_excel_" + correlativo3 + ".xlsx");
                        //byte[] bytesarray = null;
                        // Flush the workbook to the Response.OutputStream
                        using (var memoryStream = new MemoryStream())
                        {
                            workbook.SaveAs(memoryStream);
                            memoryStream.WriteTo(Response.OutputStream);
                            memoryStream.Close();

                            //bytesarray = memoryStream.ToArray();
                            //Response.End();
                            Response.Buffer = true;
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            //Response.BinaryWrite(bytesarray);
                            Response.Flush();
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            Response.Close();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        break;
                    }
                case "Cotizacion":
                    int index5 = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectedRow5 = gvCotizacion.Rows[index5];
                    TableCell corr_id4 = selectedRow5.Cells[0];
                    string correlativo4 = corr_id4.Text;
                    try
                    {
                        Encabezado encabezado4 = new Encabezado();
                        ColeccionDetalle detalles3 = new ColeccionDetalle();
                        encabezado4.Correlativo = Convert.ToInt32(correlativo4);
                        encabezado4.Read();
                        List<Detalle> list3 = detalles3.ListaDetalle(correlativo4);

                        Session["encabezado"] = encabezado4;
                        Session["detalle"] = list3;
                        Response.Redirect("../Cotizacion/EditorCotizacion.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        break;                       
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        break;
                    }
                default:
                    break;
            }
        }

        private StringBuilder HTMLPage(List<Detalle> detalle, Encabezado obj, string correlativo,Usuario usuario, TipoMoneda moneda)
        {
            string voltaje = " La regulacion de voltaje permitido del cabezal es de _____ ";
            DateTime fecha = Convert.ToDateTime(obj.Fecha);
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
            foreach (Detalle p in detalle)
            {
                string cantidad = Convert.ToInt32(p.Cantidad).ToString("###,###").Replace(",", ".");
                string precio = Convert.ToDouble(p.PrecioUnitario.ToString().Replace(".", ",")).ToString("###,##0.##");
                string subtotal = Convert.ToDouble(p.Subtotal.ToString().Replace(".", ",")).ToString("###,##0.##");
                sb.Append("<tr>");
                sb.Append("<td><p>" + cantidad + "</p></td>");
                sb.Append("<td><p>" + p.Descripcion + "</p></td>");
                sb.Append("<td><p>" + moneda.Simbolo + "" + precio + "</p></td>");
                sb.Append("<td><p>" + p.Codigo + "</p></td>");
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
            sb.Append("<td colspan=" + "" + "2" + "" + " ><b>Total</b><br/><b><h5>(Cotizacion realizada en " + obj.Tipo_moneda.ToString() + ")" + " Valores no incluyen IVA" + "</h5></b></td>");
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

        public XLWorkbook DocumentoExcel(List<Detalle> detalle, Encabezado obj, string correlativo, Usuario usuario,TipoMoneda moneda)
        {
            string voltaje = " La regulacion de voltaje permitido del cabezal es de _____ ";
            StringBuilder sb = new StringBuilder();
            XLWorkbook workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Cotizacion");
            int indexcell = 1;
            // Inicio WorkSheet
            worksheet.Range("A1" + ":" + "J8").Merge();
            var image = worksheet.AddPicture(Server.MapPath("~/pdf/logo_excel.png")).MoveTo(worksheet.Cell("A1").Address);
            var cellA = "A";
            var cellB = "B";
            var cellC = "C";
            var cellD = "D";
            var cellE = "E";
            var cellF = "F";
            var cellG = "G";
            var cellH = "H";
            var cellI = "I";
            var cellJ = "J";
            indexcell += 8;
            //Linea 1 de encabezado
            cellA += indexcell.ToString(CultureInfo.InvariantCulture);
            cellE += indexcell.ToString(CultureInfo.InvariantCulture);
            cellF += indexcell.ToString(CultureInfo.InvariantCulture);
            cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
            worksheet.Range(cellA + ":" + cellE).Merge();
            worksheet.Range(cellF + ":" + cellJ).Merge();
            worksheet.Cell(cellA).Value = "Razon Social: " + obj.Razon_social;
            worksheet.Cell(cellF).Value = "Telefono: " + obj.Telefono;
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            //Linea 2 de encabezado
            indexcell += 1;
            cellA += indexcell.ToString(CultureInfo.InvariantCulture);
            cellE += indexcell.ToString(CultureInfo.InvariantCulture);
            cellF += indexcell.ToString(CultureInfo.InvariantCulture);
            cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
            worksheet.Range(cellA + ":" + cellE).Merge();
            worksheet.Range(cellF + ":" + cellJ).Merge();
            worksheet.Cell(cellA).Value = "Rut: " + obj.Rut;
            worksheet.Cell(cellF).Value = "Correo: " + obj.Correo;
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            //Linea 3 de encabezado
            indexcell += 1;
            cellA += indexcell.ToString(CultureInfo.InvariantCulture);
            cellE += indexcell.ToString(CultureInfo.InvariantCulture);
            cellF += indexcell.ToString(CultureInfo.InvariantCulture);
            cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
            worksheet.Range(cellA + ":" + cellE).Merge();
            worksheet.Range(cellF + ":" + cellJ).Merge();
            worksheet.Cell(cellA).Value = "Contacto: " + obj.Contacto;
            worksheet.Cell(cellF).Value = "Entrega: " + obj.Entrega;
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            //Linea 4 de encabezado
            indexcell += 1;
            cellA += indexcell.ToString(CultureInfo.InvariantCulture);
            cellE += indexcell.ToString(CultureInfo.InvariantCulture);
            cellF += indexcell.ToString(CultureInfo.InvariantCulture);
            cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
            worksheet.Range(cellA + ":" + cellE).Merge();
            worksheet.Range(cellF + ":" + cellJ).Merge();
            worksheet.Cell(cellA).Value = "Direccion: " + obj.Direccion;
            worksheet.Cell(cellF).Value = "Condicion de Pago: " + obj.CondicionPago;
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            //Linea 1 de detalle -- header tabla
            indexcell += 2;
            cellA += indexcell.ToString(CultureInfo.InvariantCulture);
            cellB += indexcell.ToString(CultureInfo.InvariantCulture);
            cellG += indexcell.ToString(CultureInfo.InvariantCulture);
            cellH += indexcell.ToString(CultureInfo.InvariantCulture);
            cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
            indexcell += 1;
            cellF += indexcell.ToString(CultureInfo.InvariantCulture);           
            cellI += indexcell.ToString(CultureInfo.InvariantCulture);               
            
            worksheet.Range(cellA + ":" + cellA.Remove(1,2)+indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
            worksheet.Cell(cellA).Style.Alignment.WrapText = true;
            //worksheet.Cell(cellA).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellA).Style.Border.TopBorderColor = XLColor.Black;
            //worksheet.Cell(cellA).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellA).Style.Border.BottomBorderColor = XLColor.Black;
            //worksheet.Cell(cellA).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellA).Style.Border.LeftBorderColor = XLColor.Black;
            //worksheet.Cell(cellA).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellA).Style.Border.RightBorderColor = XLColor.Black;
            worksheet.Range(cellB + ":" + cellF).Merge();
            worksheet.Cell(cellB).Style.Alignment.WrapText = true;
            //worksheet.Cell(cellB).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellB).Style.Border.TopBorderColor = XLColor.Black;
            //worksheet.Cell(cellB).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellB).Style.Border.BottomBorderColor = XLColor.Black;
            //worksheet.Cell(cellB).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellB).Style.Border.LeftBorderColor = XLColor.Black;
            //worksheet.Cell(cellB).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellB).Style.Border.RightBorderColor = XLColor.Black;
            worksheet.Range(cellG + ":" + cellG.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
            worksheet.Cell(cellG).Style.Alignment.WrapText = true;
            //worksheet.Cell(cellG).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellG).Style.Border.TopBorderColor = XLColor.Black;
            //worksheet.Cell(cellG).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellG).Style.Border.BottomBorderColor = XLColor.Black;
            //worksheet.Cell(cellG).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellG).Style.Border.LeftBorderColor = XLColor.Black;
            //worksheet.Cell(cellG).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellG).Style.Border.RightBorderColor = XLColor.Black;
            worksheet.Range(cellH + ":" + cellI).Merge();
            worksheet.Cell(cellH).Style.Alignment.WrapText = true;
            //worksheet.Cell(cellH).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellH).Style.Border.TopBorderColor = XLColor.Black;
            //worksheet.Cell(cellH).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellH).Style.Border.BottomBorderColor = XLColor.Black;
            //worksheet.Cell(cellH).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellH).Style.Border.LeftBorderColor = XLColor.Black;
            //worksheet.Cell(cellH).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellH).Style.Border.RightBorderColor = XLColor.Black;
            worksheet.Range(cellJ + ":" + cellJ.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
            worksheet.Cell(cellJ).Style.Alignment.WrapText = true;
            //worksheet.Cell(cellJ).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellJ).Style.Border.TopBorderColor = XLColor.Black;
            //worksheet.Cell(cellJ).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellJ).Style.Border.BottomBorderColor = XLColor.Black;
            //worksheet.Cell(cellJ).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellJ).Style.Border.LeftBorderColor = XLColor.Black;
            //worksheet.Cell(cellJ).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            //worksheet.Cell(cellJ).Style.Border.RightBorderColor = XLColor.Black;

            worksheet.Cell(cellA).Value = "Cant.";
            worksheet.Cell(cellB).Value = "Descripcion";
            worksheet.Cell(cellG).Value = "Precio Unit.";
            worksheet.Cell(cellH).Value = "Codigo";
            worksheet.Cell(cellJ).Value = "Subtotal";
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            // Linea 2 de detalle -- detalles
            for (int index =0; index < detalle.Count;index++)
            {
                indexcell += 1;
                cellB += indexcell.ToString(CultureInfo.InvariantCulture);
                cellH += indexcell.ToString(CultureInfo.InvariantCulture);
                cellA += indexcell.ToString(CultureInfo.InvariantCulture);
                cellG += indexcell.ToString(CultureInfo.InvariantCulture);
                cellJ += indexcell.ToString(CultureInfo.InvariantCulture);
                indexcell += 1;
                cellF += indexcell.ToString(CultureInfo.InvariantCulture);
                cellI += indexcell.ToString(CultureInfo.InvariantCulture);

                worksheet.Range(cellA + ":" + cellA.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
                worksheet.Cell(cellA).Style.Alignment.WrapText = true;
                worksheet.Range(cellB + ":" + cellF).Merge();
                worksheet.Cell(cellB).Style.Alignment.WrapText = true;
                worksheet.Range(cellG + ":" + cellG.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
                worksheet.Cell(cellG).Style.Alignment.WrapText = true;
                worksheet.Range(cellH + ":" + cellI).Merge();
                worksheet.Cell(cellH).Style.Alignment.WrapText = true;
                worksheet.Range(cellJ + ":" + cellJ.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
                worksheet.Cell(cellJ).Style.Alignment.WrapText = true;

                worksheet.Cell(cellA).Value = detalle[index].Cantidad;
                worksheet.Cell(cellB).Value = detalle[index].Descripcion;
                worksheet.Cell(cellG).Value = moneda.Simbolo + ""+detalle[index].PrecioUnitario;
                worksheet.Cell(cellH).Value = detalle[index].Codigo;
                worksheet.Cell(cellJ).Value = moneda.Simbolo + ""+detalle[index].Subtotal;
                cellA = "A";
                cellB = "B";
                cellC = "C";
                cellD = "D";
                cellE = "E";
                cellF = "F";
                cellG = "G";
                cellH = "H";
                cellI = "I";
                cellJ = "J";
            }
            //Fin detalle
            //Inicio pie de pagina
            worksheet.Range("A38:F48").Merge();
            worksheet.Cell("A38").Style.Alignment.WrapText = true;
            worksheet.Cell("A38").Style.Font.FontSize = 6;
            worksheet.Range("G38:I40").Merge();
            worksheet.Cell("G38").Style.Alignment.WrapText = true;
            worksheet.Range("G41:I48").Merge();
            worksheet.Cell("G41").Style.Alignment.WrapText = true;
            worksheet.Range("J38:J40").Merge();
            worksheet.Cell("J38").Style.Alignment.WrapText = true;
            worksheet.Range("J41:J48").Merge();
            worksheet.Cell("J41").Style.Alignment.WrapText = true;
            //worksheet.Range(cellB + ":" + cellF).Merge();
            //worksheet.Cell(cellB).Style.Alignment.WrapText = true;
            //worksheet.Range(cellG + ":" + cellG.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
            //worksheet.Cell(cellG).Style.Alignment.WrapText = true;
            //worksheet.Range(cellH + ":" + cellI).Merge();
            //worksheet.Cell(cellH).Style.Alignment.WrapText = true;
            //worksheet.Range(cellJ + ":" + cellJ.Remove(1, 2) + indexcell.ToString(CultureInfo.InvariantCulture)).Merge();
            //worksheet.Cell(cellJ).Style.Alignment.WrapText = true;
            sb.Append("Datos Bancarios:\n");
            sb.Append("Rut: 76.076.930-4 Productos y Asesorias Computacionales Ltda.\n");
            sb.Append("Banco Santander - Cta.Cte 01-92646-2\n");
            sb.Append("Condiciones Generales\n" +
                "*Validez de la Cotización: 10 días.\n" +
                "*Por razones técnicas los pedidos se consideran cumplidos con una diferencia de +/- 10% respecto a las cantidades solicitadas.\n" +
                //".</h4>" +
                "*Cualquier modificación a la orden de compra deberá ser informada, por escrito, a ATC Ingeniería en un plazo\n" +
                "maximo de 48 horas dsede su emisión.\n" +
                "*Los precios ofertados consideran entrega dentro de la Región Metropolitana, salvo acuerdo expreso entre las partes\n" +
                "*Todas las matrices producto del proceso de impresión son propiedad de ATC Ingenieria.\n" +
                "*Los gastos incurridos en desarrollos solicitados y que finalmente no lleguen a fabricarse serán facturados al cliente.\n" +
                "*ATC Ingeniería no aceptará devoluciones transcurridos 15 días corridos desde la entrega. Asi como tambien sólo.\n" +
                "*La garantía del cabezal 3 meses." + voltaje + "\n" +
                "*Garantía de Equipo 1 año.\n" +
                "*Mantención garantía 3 meses.");
            worksheet.Cell("A38").Value = moneda.Simbolo+""+sb.ToString();
            worksheet.Cell("G38").Value = "Neto";
            worksheet.Cell("J38").Value = moneda.Simbolo + ""+obj.Neto;
            sb = new StringBuilder();
            sb.Append("Total\n");
            sb.Append("Cotizacion realizada en "+moneda.Nombre);
            worksheet.Cell("G41").Value = sb.ToString();
            worksheet.Cell("J41").Value = moneda.Simbolo + "" + obj.Total;
            //worksheet.Cell(cellA).Value = detalle[index].Cantidad;
            //worksheet.Cell(cellB).Value = detalle[index].Descripcion;
            //worksheet.Cell(cellG).Value = detalle[index].PrecioUnitario;
            //worksheet.Cell(cellH).Value = detalle[index].Codigo;
            //worksheet.Cell(cellJ).Value = detalle[index].Subtotal;
            cellA = "A";
            cellB = "B";
            cellC = "C";
            cellD = "D";
            cellE = "E";
            cellF = "F";
            cellG = "G";
            cellH = "H";
            cellI = "I";
            cellJ = "J";
            // Fin pie de pagina
            // Fin WorkSheet
            return workbook;
        }

        //public Stream GetStream(XLWorkbook excelWorkbook)
        //{
        //    Stream fs = new MemoryStream();
        //    excelWorkbook.SaveAs(fs);
        //    fs.Position = 0;
        //    return fs;
        //}

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            Session["estado"] = ddlFiltroEstados.Text;
            gvCotizacion.DataSourceID = "";
            gvCotizacion.DataSource = odsEstadosFiltro;
            gvCotizacion.DataBind();
            Session["estado"] = null;
        }

        protected void gvCotizacion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCotizacion.EditIndex = e.NewEditIndex;
            Session["estado"] = ddlFiltroEstados.Text;
            gvCotizacion.DataSourceID = "";
            gvCotizacion.DataSource = odsEstadosFiltro;
            gvCotizacion.DataBind();
            Session["estado"] = null;
        }

        protected void gvCotizacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCotizacion.EditIndex = -1;
            Session["estado"] = ddlFiltroEstados.Text;
            gvCotizacion.DataSourceID = "";
            gvCotizacion.DataSource = odsEstadosFiltro;
            gvCotizacion.DataBind();
            Session["estado"] = null;
        }

        protected void gvCotizacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //    try
            //    {
            if(e.NewValues["Estado"]!=null)
            {
                int correlativo_id = Convert.ToInt32(e.Keys["Correlativo"]);
                string telefono = e.NewValues["Telefono"].ToString();
                string correo = e.NewValues["Correo"].ToString();
                string estado = e.NewValues["Estado"].ToString();
                string observacion_estado = e.NewValues["Observacion_estado"].ToString();
                //GridViewRow selectedRow2 = gvCotizacion.Rows[index];
                //TableCell correlativo_id = selectedRow2.Cells[0];
                //TableCell razon_social = selectedRow2.Cells[1];
                //TableCell rut = selectedRow2.Cells[2];
                //TableCell contacto = selectedRow2.Cells[3];
                //TableCell fecha = selectedRow2.Cells[4];
                //TableCell telefono = selectedRow2.Cells[5];
                //TableCell correo = selectedRow2.Cells[6];
                //TableCell condicion_pago = selectedRow2.Cells[7];
                //TableCell entrega = selectedRow2.Cells[8];
                //TableCell direccion = selectedRow2.Cells[9];
                //TableCell tipo_moneda = selectedRow2.Cells[10];
                //TableCell estado = selectedRow2.Cells[11];
                //TableCell codigo_usuario = selectedRow2.Cells[12];
                //TableCell neto = selectedRow2.Cells[13];
                //TableCell iva = selectedRow2.Cells[14];
                //TableCell total = selectedRow2.Cells[15];
                Encabezado encabezado2 = new Encabezado();
                encabezado2.Correlativo = correlativo_id;
                if (encabezado2.Read())
                {
                    encabezado2.Telefono = telefono;
                    encabezado2.Correo = correo;
                    encabezado2.Estado = estado;
                    encabezado2.Observacion_estado = observacion_estado;
                    if (encabezado2.Update())
                    {
                        gvCotizacion.EditIndex = -1;
                        Session["estado"] = ddlFiltroEstados.Text;
                        gvCotizacion.DataSourceID = "";
                        gvCotizacion.DataSource = odsEstadosFiltro;
                        gvCotizacion.DataBind();
                        Session["estado"] = null;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Hubo un error en la modificacion del encabezado. Mensaje:" + encabezado2.UpdateMensaje() + "');", true);
                        gvCotizacion.EditIndex = -1;
                        Session["estado"] = ddlFiltroEstados.Text;
                        gvCotizacion.DataSourceID = "";
                        gvCotizacion.DataSource = odsEstadosFiltro;
                        gvCotizacion.DataBind();
                        Session["estado"] = null;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Problema en leer cotizacion" + "');", true);
                    gvCotizacion.EditIndex = -1;
                    Session["estado"] = ddlFiltroEstados.Text;
                    gvCotizacion.DataSourceID = "";
                    gvCotizacion.DataSource = odsEstadosFiltro;
                    gvCotizacion.DataBind();
                    Session["estado"] = null;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Estado obligatorio" + "');", true);
            }
            //}
            //catch (Exception ex)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            //    gvCotizacion.EditIndex = -1;
            //}
        }
    }
}