using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VeterinariaAPP.Models;

    public class Mascota
    {

    [JsonProperty("id_mascota")]

    public string IdMascota { get; set; }
    [JsonProperty("id_usuario")]

    public string IdUsuario { get; set; }
    [JsonProperty("nombre")]

    public string Nombre { get; set; }
    [JsonProperty("especie")]

    public string Especie { get; set; }
    [JsonProperty("raza")]

    public string Raza { get; set; }
    [JsonProperty("edad")]

    public int Edad { get; set; }
    [JsonProperty("notas_medicas")]

    public string NotasMedicas { get; set; }
    [JsonProperty("fecha_registro")]

    public DateTime FechaRegistro { get; set; }

    public override string ToString()
    {
        return Nombre;
    }
}


