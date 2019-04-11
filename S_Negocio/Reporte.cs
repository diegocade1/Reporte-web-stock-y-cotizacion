using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class Reporte
    {
        private string _codigo;
        private string _descripcion;
        private int _valortotal;

        public int Valortotal
        {
            get { return _valortotal; }
            set { _valortotal = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }
}
