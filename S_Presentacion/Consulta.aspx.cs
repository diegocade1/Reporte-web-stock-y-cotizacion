using S_Datos;
using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCodigo.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
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
                if (Session["codigo"] != null)
                {
                    txtCodigo.Text = Session["codigo"].ToString();
                    btnBuscar_Click(sender, e);
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
                //else
                //{
                //    Session["Reset"] = true;
                //    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                //    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                //    int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                //    ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                //}
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["codigo"] = txtCodigo.Text;
            gvInformacion.DataSourceID = "";
            gvInformacion.DataSource = odsCentroCostos;
            gvInformacion.DataBind();
            gvInformacion.Visible = true;
            txtCodigo.Text = "";
            Session["codigo"] = null;
        }

        protected void gvInformacion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvInformacion.Rows[e.NewSelectedIndex];
            Session["codigo"] = row.Cells[2].Text;
            Session["centrocosto"] = row.Cells[3].Text;
            gvUbicaciones.DataSourceID = "";
            gvUbicaciones.DataSource = odsUbicacion;
            gvUbicaciones.DataBind();
            gvUbicaciones.Visible = true;
            lblUbicaciones.Visible = true;
            Session["codigo"] = null;
            Session["centrocosto"] = null;
        }
    }
}