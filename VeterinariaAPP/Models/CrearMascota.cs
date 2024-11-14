

using Newtonsoft.Json;

namespace VeterinariaAPP.Models;

    public class CrearMascota
    {



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
       
    }

