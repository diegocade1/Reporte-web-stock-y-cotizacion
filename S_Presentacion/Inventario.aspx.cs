using S_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S_Presentacion
{
    public partial class Inventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescripcion.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            if (Session["usuario"]==null)
            {
                string cadena = "Debe iniciar sesion primero.";
                int count = cadena.Count();
                string mensaje = QueryStringModule.Encrypt(cadena);
                Response.Redirect("Login.aspx" + mensaje.Replace("enc", "mensaje"), false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Session["sortPalabra"] != null && Session["sortLista"] != null)
                {
                    btnQuitar.Visible = true;
                    dvProductos.DataSourceID = "";
                    dvProductos.DataSource = odsSort;
                    dvProductos.DataBind();
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

            Session["sortPalabra"] = null;
            Session["sortLista"] = null;
        }

        private List<T> DataTableToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        if(pro.Name != "Stock")
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                        }
                        else if (row[pro.Name].ToString().Contains(","))
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name].ToString().Replace(",", ""), pI.PropertyType));
                        }
                        else if (row[pro.Name].ToString().Contains("."))
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name].ToString().Replace(".", ""), pI.PropertyType));
                        }
                        else
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                        }
                    }
                }
                return objT;
            }).ToList();
        }

        private DataTable GridToTable(GridView obj)
        {
            DataTable dt = new DataTable();

            if (obj.HeaderRow != null)
            {

                for (int i = 0; i < obj.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(obj.Columns[i].HeaderText);
                }
            }

            foreach (GridViewRow row in dvProductos.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = HttpUtility.HtmlDecode(row.Cells[i].Text.Replace("&#160;", ""));
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //if(txtDescripcion.Text != "")
            //{
                //DataTable dt = GridToTable(dvProductos);
                //List<Producto> lista = ConvertToList<Producto>(dt);
                DataTable dt = GridToTable(dvProductos);
                List<Producto> lista = DataTableToList<Producto>(dt);
                btnQuitar.Visible = true;
                Session["filtroPalabra"] = ddlCentroCosto.SelectedItem.Text;
                Session["filtroDescripcion"] = txtDescripcion.Text;
                //Session["filtroLista"] = lista;
                dvProductos.DataSourceID = "";
                dvProductos.DataSource = odsFiltro;
                dvProductos.DataBind();

            //}
            //else
            //{
            //    string mensaje = "Debe ingresar un filtro antes de buscar";
            //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            //}
            
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            Session["filtroPalabra"] = null;
            //Session["filtroLista"] = null;
            Response.Redirect("Inventario.aspx",false);
            Context.ApplicationInstance.CompleteRequest();
        }

        //private string ConvertSortDirectionToSql(SortDirection sortDirection)
        //{
        //    string newSortDirection = String.Empty;

        //    switch (sortDirection)
        //    {
        //        case SortDirection.Ascending:
        //            newSortDirection = "ASC";
        //            break;

        //        case SortDirection.Descending:
        //            newSortDirection = "DESC";
        //            break;
        //    }

        //    return newSortDirection;
        //}

        protected void dvProductos_Sorting(object sender, GridViewSortEventArgs e)
        {
            //DataTable dt = new DataTable();

            ////if (dvProductos.HeaderRow != null)
            ////{

            ////    for (int i = 0; i < dvProductos.HeaderRow.Cells.Count; i++)
            ////    {
            ////        dt.Columns.Add(dvProductos.Columns[i].HeaderText);
            ////    }
            ////}

            ////foreach (GridViewRow row in dvProductos.Rows)
            ////{
            ////    DataRow dr;
            ////    dr = dt.NewRow();

            ////    for (int i = 0; i < row.Cells.Count; i++)
            ////    {
            ////        dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");
            ////    }
            ////    dt.Rows.Add(dr);
            ////}

            //if (dt != null)
            //{
            //    DataView dataView = new DataView(dt);
            //    dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
            //    dvProductos.DataSourceID = "";
            //    dvProductos.DataSource = dataView;
            //    dvProductos.DataBind();
            //}
            DataTable dt = GridToTable(dvProductos);
            List<Producto> lista = DataTableToList<Producto>(dt);
            Session["sortPalabra"] = e.SortExpression;
            Session["sortLista"] = lista;
            Response.Redirect("Inventario.aspx");
        }

        protected void dvProductos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = dvProductos.Rows[e.NewSelectedIndex];
            Session["codigo"] = row.Cells[2].Text;
            Response.Redirect("Consulta.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnCentroCosto_Click(object sender, EventArgs e)
        {
            //Session["filtroCentoCosto"] = ddlCentroCosto.SelectedItem.Text;
            //btnQuitar.Visible = true;
            //dvProductos.DataSourceID = "";
            //dvProductos.DataSource = odsFiltroCentroCosto;
            //dvProductos.DataBind();
        }
    }
}