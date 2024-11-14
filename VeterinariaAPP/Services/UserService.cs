

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using VeterinariaAPP.Models;
using VeterinariaAPP.Models.Auth;

namespace VeterinariaAPP.Services;

    public class UserService
    {
    public HttpClient Client { get; set; }
    public readonly string UrlApi = "https://f350-2800-e2-407f-fd96-95c9-85c8-428a-d87c.ngrok-free.app";

    public UserService()
    {
        Client = new HttpClient();

    }

    public async Task<MascotaResponse> AgregarMascotaService(CrearMascota data)
    {
        var apiUrl = $"{UrlApi}/api/mascotas";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jsonContent = JsonConvert.SerializeObject(data);
        Console.WriteLine($"JSON Content: {jsonContent}");

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await Client.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<MascotaResponse>();
            return responseData;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error Content: {errorContent}");

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
            if (errorResponse != null && errorResponse.Messages != null)
            {
                var combinedErrors = string.Join(", ", errorResponse.Messages.Select(e => $"{e.Message}"));
                Console.WriteLine($"Combined Errors: {combinedErrors}");

                throw new Exception(combinedErrors);
            }

            var singleErrorResponse = JsonConvert.DeserializeObject<SingleErrorResponse>(errorContent);
            if (singleErrorResponse != null)
            {
                Console.WriteLine($"Message: {singleErrorResponse.Message}");
                throw new Exception(singleErrorResponse.Message);
            }

            throw new Exception("Registro fallido con código de estado: " + response.StatusCode);
        }
    }



}
