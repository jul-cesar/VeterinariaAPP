

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using VeterinariaAPP.Models;

namespace VeterinariaAPP.Services;

public class ServicesService
{
    public HttpClient Client { get; set; }

    public ServicesService()
    {
        Client = new HttpClient();

    }


    public async Task<List<Servicio>> GetServiciosService()
    {
        var apiUrl = "https://734a-2800-e2-407f-fd96-35de-6b2e-8f1b-288b.ngrok-free.app/api/servicios";

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
        var apiUrl = $"https://734a-2800-e2-407f-fd96-35de-6b2e-8f1b-288b.ngrok-free.app/api/servicios/{id}";

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
        var apiUrl = $"https://734a-2800-e2-407f-fd96-35de-6b2e-8f1b-288b.ngrok-free.app/api/mascotas/{IdUser}";

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
        var apiUrl = $"https://734a-2800-e2-407f-fd96-35de-6b2e-8f1b-288b.ngrok-free.app/api/dispos/{IdServicio}";

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


}

