using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionProducto
    {
        public List<Producto> ListaProductos()
        {
            String sql = "select codigo, descripcion, familia, id,paquete,stock, umedida from v_producto_stock;";
            List<Producto> lista = new List<Producto>();
            Conexion con = new Conexion();
            foreach (ProductoBase p in con.ObtenerLista(sql))
            {
                Producto pr = new Producto()
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    Familia = p.Familia,
                    ID = p.ID,
                    Paquete = p.Paquete,
                    Stock = p.Stock,
                    Umedida = p.Umedida
                };
                lista.Add(pr);
            }
            return lista;
        }

        public List<Producto> ListaBusquedaProductos(string palabra, string descripcion)
        {
            if (descripcion!=null)
            {
                if (palabra.Equals("Todo"))
                {
                    return ListaProductosBuscarDesc(descripcion, ListaProductos());
                }
                else
                {
                    return ListaProductosBuscarDesc(descripcion, ListaProductosCentroCosto(palabra));
                }
            }
            else
            {
                descripcion = "";
                if (palabra.Equals("Todo"))
                {
                    return ListaProductosBuscarDesc(descripcion, ListaProductos());
                }
                else
                {
                    return ListaProductosBuscarDesc(descripcion, ListaProductosCentroCosto(palabra));
                }
            }

        }

        public List<Producto> ListaProductosBuscarDesc(string descripcion,List<Producto> lista)
        {
            if(ListaProductos()!=null)
            {
                if (descripcion.Contains(","))
                {
                    string v1 = descripcion.Substring(0, (descripcion.IndexOf(",")));
                    string v2 = descripcion.Substring(descripcion.IndexOf(",") + 1);
                    return lista.Where(fila => (fila.Descripcion.ToLower().Replace(" ", "").Contains(v1.ToLower().Replace(" ", "")) && (fila.Descripcion.ToLower().Replace(" ", "").Contains(v2.ToLower().Replace(" ", ""))))).ToList();
                }
                else
                {
                    return lista.Where(fila => (fila.Descripcion.ToLower().Replace(" ", "").Contains(descripcion.ToLower().Trim()))).ToList();
                }
            }
            else
            {
                return null;
            }
        }

        //public List<Producto> ListarProduDescripcion(string descripcion)
        //{
        //    String sql="";
        //    if(descripcion.Contains(","))
        //    {
        //        string v1 = descripcion.Substring(0, (descripcion.IndexOf(",")));
        //        string v2 = descripcion.Substring(descripcion.IndexOf(",")+1);
        //        sql = "select * from v_producto_stock where (lower(replace(descripcion, ' ','')) like lower(replace('%" + v1 + "%',' ',''))) and (lower(replace(descripcion, ' ','')) like lower(replace('%"+v2+"%',' ','')));";
        //    }
        //    else
        //    {
        //        sql = "select * from v_producto_stock where  (lower(replace(descripcion, ' ','')) like lower(replace('%"+descripcion+"%',' ','')));";
        //    }
        //    List<Producto> lista = new List<Producto>();
        //    Conexion con = new Conexion();
        //    foreach (ProductoBase p in con.ObtenerLista(sql))
        //    {
        //        Producto pr = new Producto()
        //        {
        //            Codigo = p.Codigo,
        //            Descripcion = p.Descripcion,
        //            Familia = p.Familia,
        //            ID = p.ID,
        //            Paquete = p.Paquete,
        //            Stock = p.Stock,
        //            Umedida = p.Umedida
        //        };
        //        lista.Add(pr);
        //    }
        //    return lista;
        //}

        public List<Producto> ListaProductosOrdenar(List<Producto> lista, string palabra)
        {
            switch(palabra)
            {
                case "Stock":
                    return lista.OrderByDescending(fila => fila.Stock).ToList();
                case "Familia":
                    return lista.OrderBy(fila => fila.Familia).ToList();
                case "Umedida":
                    return lista.OrderBy(fila => fila.Umedida).ToList();
                case "Descripcion":
                    return lista.OrderBy(fila => fila.Descripcion).ToList();
                case "Codigo":
                    return lista.OrderBy(fila => fila.Codigo).ToList();
                case "Paquete":
                    return lista.OrderByDescending(fila => fila.Paquete).ToList();
                case "ID":
                    return lista.OrderBy(fila => fila.ID).ToList();
                default:
                    return lista.OrderByDescending(fila => fila.Stock).ToList();
            }
        }

        //public List<Producto> ListaProductosSort(string palabra)
        //{
        //    String sql = "select codigo, descripcion, familia, id,paquete,stock, umedida from v_producto_stock order by "+palabra+" desc;";
        //    List<Producto> lista = new List<Producto>();
        //    Conexion con = new Conexion();
        //    foreach (ProductoBase p in con.ObtenerLista(sql))
        //    {
        //        Producto pr = new Producto()
        //        {
        //            Codigo = p.Codigo,
        //            Descripcion = p.Descripcion,
        //            Familia = p.Familia,
        //            ID = p.ID,
        //            Paquete = p.Paquete,
        //            Stock = p.Stock,
        //            Umedida = p.Umedida
        //        };
        //        lista.Add(pr);
        //    }
        //    return lista;
        //}
        public List<Producto> ListaProductosCentroCosto(string centro_costo)
        {
            String sql = "SELECT a.codigo, a.descripcion, a.familia, a.id, a.paquete, a.stock, a.umedida FROM v_producto_stock a left join tipo_stock b on a.codigo = b.codigo_p where b.centro_costo = '"+centro_costo+"' and a.stock > 0;";
            List<Producto> lista = new List<Producto>();
            Conexion con = new Conexion();
            foreach (ProductoBase p in con.ObtenerLista(sql))
            {
                Producto pr = new Producto()
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    Familia = p.Familia,
                    ID = p.ID,
                    Paquete = p.Paquete,
                    Stock = p.Stock,
                    Umedida = p.Umedida
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
