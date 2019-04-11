using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class ColeccionStock
    {
        public List<Stock> StockTipoStock()
        {
            String sql = "select * from tipo_stock;";
            List<Stock> lista = new List<Stock>();
            Conexion con = new Conexion();
            foreach (StockBase p in con.ObtenerStockCentroCosto(sql))
            {
                Stock pr = new Stock()
                {
                    Codigo = p.Codigo,
                    ID = p.Id,
                    Ubicacion = p.Ubicaciones,
                    CentroCosto = p.Centro_costo,
                    Stocks = p.Stock
                };
                lista.Add(pr);
            }
            return lista;
        }
        public List<Stock> StockProductoCentroCosto(string codigo)
        {
            String sql = "select id,codigo_p,centro_costo, sum(stock) as stock  from tipo_stock where codigo_p = '" + codigo + "' group by centro_costo;";
            List<Stock> lista = new List<Stock>();
            Conexion con = new Conexion();
            foreach (StockBase p in con.ObtenerStockCentroCosto(sql))
            {
                Stock pr = new Stock()
                {
                    ID = p.Id,
                    Codigo = p.Codigo,
                    CentroCosto = p.Centro_costo,
                    Stocks = p.Stock 
                };
                lista.Add(pr);
            }
            return lista;
        }
        public List<Stock> StockProductoUbicaciones(string codigo, string centro_costo)
        {
            String sql = "select ubicacion, stock  from tipo_stock where codigo_p='"+codigo+"' and centro_costo='"+centro_costo+"' and stock > 0";
            List<Stock> lista = new List<Stock>();
            Conexion con = new Conexion();
            foreach (StockBase p in con.ObtenerStockUbicaciones(sql))
            {
                Stock pr = new Stock()
                {
                    Ubicacion = p.Ubicaciones,
                    Stocks = p.Stock
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
    public class ColeccionStockDescripcion
    {
        public List<StockDescripcion> StockProductoCentroCosto(string codigo)
        {
            String sql = "select a.id as 'id' ,b.descripcion as 'descripcion' ,a.codigo_p as 'codigo_p' ,a.centro_costo as 'Centro_costo', sum(a.stock) as 'Stock'  from tipo_stock a left join productos b on a.codigo_p = b.codigo where codigo_p = '" + codigo + "' group by centro_costo;";
            List<StockDescripcion> lista = new List<StockDescripcion>();
            Conexion con = new Conexion();
            foreach (StockDescripcionBase p in con.ObtenerStockDescripcionCentroCosto(sql))
            {
                StockDescripcion pr = new StockDescripcion()
                {
                    ID = p.Id,
                    Codigo = p.Codigo,
                    CentroCosto = p.Centro_costo,
                    Stocks = p.Stock,
                    Descripcion = p.Descripcion
                };
                lista.Add(pr);
            }
            return lista;
        }
    }
}
