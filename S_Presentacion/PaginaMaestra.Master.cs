using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace S_Presentacion
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Conection obj = new Conection();
            obj.set_ConexionString(ConfigurationManager.ConnectionStrings["MyConexion"].ConnectionString);
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                TipoUsuario tipo = new TipoUsuario();
                PermisosModulos permisos = new PermisosModulos();
                tipo.Descripcion = usuario.Tipo_usuario;
                tipo.Read();
                permisos.ID_tipo_usuario = tipo.ID;
                permisos.Read();
                lblUsuario.InnerHtml = "Hola, " + usuario.Nombre + " " + usuario.Apellido;
                DisponibilidadModulos(permisos.Modulos);
            }
        }

        private void DisponibilidadModulos(string modulos)
        {
            string[] array = modulos.Split(',');
            int index;
            string key;
            //foreach (string key in Request.Form.AllKeys)
            //{
            //    if (key.Contains(array[index]))
            //    {
            //        this.GetElementsByTagName("input")
            //        //Request.Form[key];
            //        index++;
            //    }
            //}
            //foreach (Control ctrl in nav1.Controls)
            //{
            //    if (!ctrl is HtmlAnchor)
            //    {
            //        string url = ((HtmlAnchor)ctrl).HRef;
            //        if (url == GetCurrentPage())  // <-- you'd need to write that
            //            ctrl.Parent.Attributes.Add("class", "active");
            //    }
            //}
            for (index = 0; index < array.Length; index++)
            {
                key = "Funcion"+index+"";
                Page.ClientScript.RegisterStartupScript(GetType(), key, "Modulos(" + array[index] + ");", true);
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), index.ToString(), "Modulos(" + array[index] + ")", true);
            }
            //if (modulos.Contains("6") && modulos.Contains("7"))
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "Cotizacion", "Modulos(menuCotizacion)", true);
            //}
            //if (modulos.Contains("8") && modulos.Contains("9") && modulos.Contains("10"))
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "Usuarios", "Modulos(menuUsuarios)", true);
            //}
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Modulos(" + array[4] + ")", true);
        }
    }
}