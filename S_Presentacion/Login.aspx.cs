using S_Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Conection obj = new Conection();
            obj.set_ConexionString(ConfigurationManager.ConnectionStrings["MyConexion"].ConnectionString);
            if(Session["usuario"]!=null)
            {
                Response.Redirect("Inicio.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Request.QueryString["mensaje"] != null)
                {
                    string mensaje = QueryStringModule.Decrypt(Request.QueryString["mensaje"]);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje.Replace(".", "").Replace("%20", " ") + "');", true);
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Usuario_ = txtUsuario.Value;

                if (usuario.Read())
                {
                    if (usuario.Password.Equals(txtPassword.Value))
                    {
                        if(usuario.Estado != "Habilitado")
                        {
                            string cadena = "El%20Usuario%20esta%20deshabilitado.";
                            int count = cadena.Count();
                            string mensaje = QueryStringModule.Encrypt(cadena);
                            Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            Session["usuario"] = usuario;
                            Response.Redirect("Inicio.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        string cadena = "Password Incorrecto.";
                        int count = cadena.Count();
                        string mensaje = QueryStringModule.Encrypt(cadena);
                        Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    string cadena = "Usuario Invalido";
                    int count = cadena.Count();
                    string mensaje = QueryStringModule.Encrypt(cadena);
                    Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
        }
    }
}