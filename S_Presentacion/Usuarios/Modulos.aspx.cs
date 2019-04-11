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
    public partial class Modulos : System.Web.UI.Page
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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            TipoUsuario tipo = new TipoUsuario();
            PermisosModulos permisos = new PermisosModulos();
            permisos.ID_tipo_usuario = Convert.ToInt32(txtID.Value);
            permisos.Modulos = txtModulos.Value;
            tipo.ID = permisos.ID_tipo_usuario;
            if (tipo.ReadID())
            {
                if (permisos.Read())
                {
                    string mensaje = "El tipo de usuario especificado ya tiene asignado modulos";
                    txtID.Value = "";
                    txtModulos.Value = "";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                }
                else
                {
                    permisos.Create();
                    string mensaje = "Los modulos han sido asignados correctamente";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    Response.Redirect(Request.RawUrl, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                string mensaje = "El tipo de usuario especificado no existe";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                txtID.Value = "";
                txtModulos.Value = "";
            }
        }
    }
}