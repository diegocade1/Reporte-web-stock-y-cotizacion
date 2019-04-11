using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace S_Presentacion
{
    public partial class GraficaPorProducto : System.Web.UI.Page
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
                Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "C2";
                Chart1.ChartAreas[0].RecalculateAxesScale();
                Chart1.Series[0].Label = "#VALY{C2}";
                Chart1.Series[0].PostBackValue = "#VALX";

                if (Session["centrocosto"] == null)
                {
                    Response.Redirect("Grafica.aspx");
                }
                else
                {
                    ColeccionReporte reporte = new ColeccionReporte();
                    int tamaño = reporte.ListaPorProducto(Session["centrocosto"].ToString()).Count * 25;
                    Chart1.Height = new Unit(tamaño, UnitType.Pixel);
                    ChartArea CA = Chart1.ChartAreas[0];
                    CA.Position = new ElementPosition(0, 0, 100, 100);
                    lblCentroC.Text = Session["centrocosto"].ToString();
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

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            Session["codigo"] = e.PostBackValue;
            Response.Redirect("Consulta.aspx");
        }
    }
}