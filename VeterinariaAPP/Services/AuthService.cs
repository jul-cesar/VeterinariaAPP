using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using VeterinariaAPP.Models.Auth;

namespace VeterinariaAPP.Services
{
    public class AuthService
    {
        public HttpClient Client { get; set; }

        public AuthService()
        {
            Client = new HttpClient();
        }

        public async Task<RegistroResponse> RegisterUserService(Registro data)
        {
            var apiUrl = "https://4785-2800-e2-407f-fd96-1c71-d6c3-b573-baef.ngrok-free.app/api/auth/register";
            try
            {
                // Serialize the data to JSON
                var jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await Client.PostAsync(apiUrl, content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to RegistroResponse
                    var responseData = await response.Content.ReadFromJsonAsync<RegistroResponse>();
                    return responseData;
                }
                else
                {
                    // Handle error response or throw an exception as needed
                    throw new Exception("Registration failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error during registration: {ex.Message}");
                return null;
            }
        }
    }
}
