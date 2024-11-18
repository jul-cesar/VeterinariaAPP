

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using VeterinariaAPP.Models;
using VeterinariaAPP.Models.Auth;
using VeterinariaAPP.Models.Auth.Citas;

namespace VeterinariaAPP.Services;

    public class UserService
    {
    public HttpClient Client { get; set; }
    public readonly string UrlApi = "https://8e96-2800-e2-407f-fd96-f03f-205f-ceca-48f2.ngrok-free.app";

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

    public async Task<MascotaResponse> ActualizarMascotaService(CrearMascota data, string id)
    {
        var apiUrl = $"{UrlApi}/api/mascotas/{id}";

        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jsonContent = JsonConvert.SerializeObject(data);
        Console.WriteLine($"JSON Content: {jsonContent}");

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await Client.PutAsync(apiUrl, content);

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
    public async Task<List<Mascota>> GetMascotasUserService(string IdUser)
    {
        var apiUrl = $"{UrlApi}/api/mascotas/{IdUser}";

        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<List<Mascota>>(jsonResponse);

            if (responseData != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(responseData, Formatting.Indented));
                return responseData;
            }
            else
            {
                throw new Exception("La respuesta fue exitosa, pero los datos están vacíos.");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener los servicios. Código de estado: {response.StatusCode}. Detalle: {errorContent}");
        }
    }

    public async Task<Mascota> GetMascotaService(string id)
    {
        var apiUrl = $"{UrlApi}/api/mascotas/info/{id}";

        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<Mascota>(jsonResponse);

            if (responseData != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(responseData, Formatting.Indented));
                return responseData;
            }
            else
            {
                throw new Exception("La respuesta fue exitosa, pero los datos están vacíos.");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener los servicios. Código de estado: {response.StatusCode}. Detalle: {errorContent}");
        }
    }

    public async Task<List<Cita>> GetCitasService(string id)
    {
        var apiUrl = $"{UrlApi}/api/citas/{id}";

        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<List<Cita>>(jsonResponse);

            if (responseData != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(responseData, Formatting.Indented));
                return responseData;
            }
            else
            {
                throw new Exception("La respuesta fue exitosa, pero los datos están vacíos.");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener los servicios. Código de estado: {response.StatusCode}. Detalle: {errorContent}");
        }
    }
}
