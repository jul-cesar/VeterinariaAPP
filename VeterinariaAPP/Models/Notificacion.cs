
namespace VeterinariaAPP.Models;

    public class Notificacion
    {
        public string id_usuario { get; set; }
    public string id_notificacion { get; set; }
    public string tipo { get; set; }
    public string mensaje { get; set; }
    public DateTime fecha_envio { get; set; }
    public bool leido { get; set; }


}

