using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class TextFile
    {
        public void Lectura(string ruta)
        {
            StreamReader lista = new StreamReader(ruta);
            String linea;
            String[] arr;
            Producto obj = new Producto();
            Stock obj2 = new Stock();
            while (lista.EndOfStream == false)
            {
                linea = lista.ReadLine();
                arr = linea.Split(';');
                obj.Codigo = arr[0];
                obj.Descripcion = arr[1];
                obj.Paquete = Convert.ToDouble(arr[2]);
                obj.Familia = arr[3];
                obj.Umedida = arr[4];
                obj2.Codigo = obj.Codigo;
                obj2.CentroCosto = arr[5];
                obj2.Ubicacion = arr[6];
                obj2.Stocks = Convert.ToInt32(arr[7]);
                if(obj.Read())
                {
                    if(obj2.Read())
                    {
                        obj2.Update();
                        new Exception ("Stock Ingresado con Exito");
                    }
                    else
                    {
                        obj2.Create();
                        new Exception("Stock Creado e Ingresado con Exito");
                    }
                }
                else
                {
                    obj.Create();
                    obj2.Create();
                    new Exception("Producto y Stock Creado e Ingresado con Exito");
                }
            }
        }
    }
}
