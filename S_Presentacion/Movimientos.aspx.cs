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
    public partial class Movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                string cadena = "Debe iniciar sesion primero.";
                int count = cadena.Count();
                string mensaje = QueryStringModule.Encrypt(cadena);
                Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtFechaFin.Value != "")
            {
                if(txtFechaIni.Value != "")
                {
                    Session["fechaIni"] = txtFechaIni.Value;
                    Session["fechaFin"] = txtFechaFin.Value;
                    if (odsRegistro != null)
                    {
                        lblMensaje.Visible = false;
                        //lvRegistro.DataSource = odsRegistro;
                        //lvRegistro.DataBind();
                        lvRegistro.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Visible = true;
                        lvRegistro.Visible = false;
                    }
                    txtFechaFin.Value = null;
                    txtFechaIni.Value = null;
                }
                else
                {
                    string mensaje = "Debe seleccionar fecha de inicio antes de consultar";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    txtFechaIni.Focus();
                } 
            }
            else
            {
                string mensaje = "Debe seleccionar fecha de fin antes de consultar";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                txtFechaFin.Focus();
            }
        }
    }
}