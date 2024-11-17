

using Newtonsoft.Json;

namespace VeterinariaAPP.Models.Auth.Citas;

    public class Cita
    {
    public string id_cita { get; set; }

    public string id_usuario { get; set; }
        public string id_mascota { get; set; }
        public string id_servicio { get; set; }
        
        public string id_disponibilidad { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string fecha_creacion { get; set; }
    public class ErrorResponse
    {
        public List<ErrorMessage> Messages { get; set; }
    }

    public class ErrorMessage
    {

        [JsonProperty("field")]

        public string Field { get; set; }
        [JsonProperty("message")]

        public string Message { get; set; }
    }

    public class SingleErrorResponse
    {
        [JsonProperty("message")]

        public string Message { get; set; }
    }



}

