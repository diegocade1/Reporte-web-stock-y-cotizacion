using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Datos
{
    public class UsuarioBase
    {
        public int Id { set; get; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Tipo_usuario { set; get; }
        public string Estado { set; get; }
    }
}
