

using Newtonsoft.Json;

namespace VeterinariaAPP.Models.Auth;

    public class Logeo
    {
    [JsonProperty("email")]

    public string Email { get; set; }
    [JsonProperty("password")]

    public string Password { get; set; }    
    }


