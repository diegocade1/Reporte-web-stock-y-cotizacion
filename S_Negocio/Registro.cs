using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class Registro
    {
        private int _id;
        private string _codigo;
        private int _ingreso;
        private int _egreso;
        private string _fecha;
        private string _hora;
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        public string Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }


        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


        public int Egreso
        {
            get { return _egreso; }
            set { _egreso = value; }
        }


        public int Ingreso
        {
            get { return _ingreso; }
            set { _ingreso = value; }
        }


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }


        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Validar()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}
