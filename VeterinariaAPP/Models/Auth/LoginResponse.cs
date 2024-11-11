

using Newtonsoft.Json;

namespace VeterinariaAPP.Models.Auth;

    public class LoginResponse
    {
    [JsonProperty("message")]

    public string Message { get; set; }

    [JsonProperty("token")]

    public string Token { get; set; }   
    }

public class ErrorLoginResponse
{
    [JsonProperty("message")]

    public string Message { get; set; }
}

