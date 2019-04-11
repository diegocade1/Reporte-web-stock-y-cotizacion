using S_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Negocio
{
    public class CentroCosto
    {
        private int _id;
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool Read()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("SELECT * from centro_costos where id = '{0}';", _id));
                Conexion con = new Conexion();
                CentroCostoBase pd = con.ObtenerCentroCosto(sb.ToString());
                this._id = pd.Id;
                this._descripcion = pd.Descripcion;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
