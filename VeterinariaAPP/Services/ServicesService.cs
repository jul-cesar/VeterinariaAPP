

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using VeterinariaAPP.Models;
using VeterinariaAPP.Models.Auth;
using VeterinariaAPP.Models.Auth.Citas;

namespace VeterinariaAPP.Services;

public class ServicesService
{
    public HttpClient Client { get; set; }
    public readonly string UrlApi = "https://7747-2800-e2-407f-fd96-bdb6-4da5-36ff-f8f2.ngrok-free.app";

    public ServicesService()
    {
        Client = new HttpClient();
       
    }


    public async Task<List<Servicio>> GetServiciosService()
    {
        var apiUrl = $"{UrlApi}/api/servicios";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
            var responseData = JsonConvert.DeserializeObject<List<Servicio>>(jsonResponse);

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

    public async Task<Servicio> GetServicioService(string id)
    {
        var apiUrl = $"{UrlApi}/api/servicios/{id}";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
            var responseData = JsonConvert.DeserializeObject<Servicio>(jsonResponse);

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

    public async Task<List<Mascota>> GetMascotasUserService(string IdUser)
    {
        var apiUrl = $"{UrlApi}/api/mascotas/{IdUser}";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
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

    public async Task<List<Disponibilidad>> GetDisposService(string IdServicio)
    {
        var apiUrl = $"{UrlApi}/api/dispos/{IdServicio}";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
            var responseData = JsonConvert.DeserializeObject<List<Disponibilidad>>(jsonResponse);

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

    public async Task<CitaResponse> ApartarCitaService(CrearCita data)
    {
        var apiUrl = $"{UrlApi}/api/citas";

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
            var responseData = await response.Content.ReadFromJsonAsync<CitaResponse>();
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

            throw new Exception("Error al apartar la cita con código de estado: " + response.StatusCode);
        }
    }


    public async Task<List<Notificacion>> GetNotificacionesUser(string idUser)
    {
        var apiUrl = $"{UrlApi}/api/notificaciones/{idUser}";

        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
            var responseData = JsonConvert.DeserializeObject<List<Notificacion>>(jsonResponse);

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

