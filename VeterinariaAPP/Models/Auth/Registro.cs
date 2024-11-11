

namespace VeterinariaAPP.Models.Auth;

using Newtonsoft.Json;

public class Registro
{
    [JsonProperty("nombre")]
    public string Nombre { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }
}


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


