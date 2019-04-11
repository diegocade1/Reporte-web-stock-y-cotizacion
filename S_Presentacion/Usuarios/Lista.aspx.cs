using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion.Usuarios
{
    public partial class Lista : System.Web.UI.Page
    {
        private TipoUsuario tipo = new TipoUsuario();
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
                Usuario usuario = (Usuario)Session["usuario"];
                if (!tipo.ReturnAdminPrivileges(usuario))
                {
                    string cadena = "Pagina%20disponible%20para%20administradores%20solamente";
                    int count = cadena.Count();
                    string mensaje = QueryStringModule.Encrypt(cadena);
                    Response.Redirect("../Inicio.aspx" + mensaje.Replace("enc", "mensaje"), false);
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
        }
    }
}