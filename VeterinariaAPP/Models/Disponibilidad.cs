using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VeterinariaAPP.Models
{
    public class Disponibilidad
    {
        public string id_disponibilidad { get; set; }
        public string id_servicio { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public override string ToString()
        {
            return fecha;
        }
    }

   
}
