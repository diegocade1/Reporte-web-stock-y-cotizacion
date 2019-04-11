using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion.Uploads
{
    public partial class Archivo : System.Web.UI.Page
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

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            TextFile obj = new TextFile();
            HttpPostedFile file = Request.Files["txtArchivo"];

            //check file was submitted
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                string fname = Path.GetFileName(file.FileName);
                string ruta = Server.MapPath("/") + "Archivos\\" + fname;
                file.SaveAs(ruta);
                obj.Lectura(ruta);
                }
                catch(Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                string mensaje = "Seleccione un archivo valido";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }
    }
}