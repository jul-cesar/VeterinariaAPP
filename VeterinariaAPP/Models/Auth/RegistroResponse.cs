

namespace VeterinariaAPP.Models.Auth;

    public class RegistroResponse
    {
        public string Message { get; set; }
        public UsuarioData? Data { get; set; }
    }

    public class UsuarioData
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
    }


