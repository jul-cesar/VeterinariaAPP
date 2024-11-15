﻿

using Newtonsoft.Json;

namespace VeterinariaAPP.Models
{
    public class Servicio
    {
        [JsonProperty("id_servicio")]
        public string IdServicio { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; } 

        [JsonProperty("duracion")]
        public int Duracion { get; set; }

        [JsonProperty("recomendaciones")]
        public string? Recomendaciones { get; set; }
    }
}


