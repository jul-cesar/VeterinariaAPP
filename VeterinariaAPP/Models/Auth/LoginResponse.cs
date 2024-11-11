

using Newtonsoft.Json;

namespace VeterinariaAPP.Models.Auth;

public class LoginResponse
{
    [JsonProperty("message")]

    public string Message { get; set; }

    [JsonProperty("token")]

    public string Token { get; set; }
    [JsonProperty("data")]


    public Data Data {get; set;} 
    }


public class Data
{
    [JsonProperty("id_usuario")]

    public string Id_Usuario { get; set; }
    [JsonProperty("nombre")]

    public string Nombre { get; set; }
    [JsonProperty("email")]

    public string Email { get; set; }
}

public class ErrorLoginResponse
{
    [JsonProperty("message")]

    public string Message { get; set; }
}

