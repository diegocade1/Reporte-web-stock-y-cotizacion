using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace S_Presentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["usuario"]==null)
            {
                string cadena = "Debe iniciar sesion primero.";
                int count = cadena.Count();
                string mensaje = QueryStringModule.Encrypt(cadena);
                Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Request.QueryString["mensaje"] != null)
                {
                    string mensaje = QueryStringModule.Decrypt(Request.QueryString["mensaje"]);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje.Replace(".", "").Replace("%20"," ") + "');", true);
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
    }
}