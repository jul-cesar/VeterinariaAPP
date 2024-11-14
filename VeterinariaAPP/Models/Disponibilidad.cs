using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaAPP.Models
{
    public class Disponibilidad
    {
        public string id_disponibilidad { get; set; }
        public string id_servicio { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}
