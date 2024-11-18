
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json; 
using VeterinariaAPP.Models.Auth;

namespace VeterinariaAPP.Services
{
    public class AuthService
    {
        public HttpClient Client { get; set; }
        public readonly string UrlApi = "https://8e96-2800-e2-407f-fd96-f03f-205f-ceca-48f2.ngrok-free.app";

        public AuthService()
        {
            Client = new HttpClient();
        }

        public async Task<LoginResponse> LoginUserService(Logeo data)
        {
            var apiUrl = $"{UrlApi}/api/auth/login";
            var jsonContent = JsonConvert.SerializeObject(data);
            Console.WriteLine($"JSON Content: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseData;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Content: {errorContent}");

                var errorResponse = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorContent);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Message))
                {
                    throw new Exception(errorResponse.Message);
                }

                throw new Exception("An unknown error occurred during login.");
            }
        }


        public async Task<RegistroResponse> RegisterUserService(Registro data)
        {
            var apiUrl = $"{UrlApi}/api/auth/register";
           
                var jsonContent = JsonConvert.SerializeObject(data);
                Console.WriteLine($"JSON Content: {jsonContent}");

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await Client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<RegistroResponse>();
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

                    throw new Exception("Registration failed with status code: " + response.StatusCode);
                }
            
        }

    }
}
