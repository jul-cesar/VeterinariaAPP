using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VeterinariaAPP.Models.Auth;
using VeterinariaAPP.Services;
using VeterinariaAPP.Views;

namespace VeterinariaAPP.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private readonly AuthService authService;
        public AuthViewModel(AuthService auth)
        {
            authService = auth;

        }

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string nombreEntry;

        [ObservableProperty]
        private string passwordEntry;

        [ObservableProperty]
        private string emailEntry;

        [ObservableProperty]

        private string errorMessage;

        [RelayCommand]
        public async Task LoginUser()
        {
            if (string.IsNullOrWhiteSpace(PasswordEntry) || string.IsNullOrWhiteSpace(EmailEntry))
            {
                Console.WriteLine("Please fill in all fields.");
                ErrorMessage = "Por favor, llena todos los campos";
                IsLoading = false;
                return;
            }

            IsLoading = true;

            try
            {
                var loginData = new Logeo
                {
                    Email = EmailEntry,
                    Password = PasswordEntry,
                };

                var response = await authService.LoginUserService(loginData);

                if (response != null && !string.IsNullOrEmpty(response.Token) && !string.IsNullOrEmpty(response.Data.Id_Usuario) && !string.IsNullOrEmpty(response.Data.Nombre) && !string.IsNullOrEmpty(response.Data.Email))
                {
                    Console.WriteLine("Login successful");
                    await SecureStorage.SetAsync("token", response.Token);
                    await SecureStorage.SetAsync("id", response.Data.Id_Usuario);
                    await SecureStorage.SetAsync("email", response.Data.Email);
                    await SecureStorage.SetAsync("nombre", response.Data.Nombre);


                    ErrorMessage = string.Empty;
                    EmailEntry = string.Empty;
                    PasswordEntry = string.Empty;

                    await Shell.Current.GoToAsync("//main", true);

                }
                else
                {
                    Console.WriteLine("Login failed.");
                    ErrorMessage = "No se pudo iniciar sesión. Intenta nuevamente.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                ErrorMessage = ex.Message; 
            }
            finally
            {
                IsLoading = false; 
            }
        }


        [RelayCommand]
        public async Task RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(NombreEntry) ||
                string.IsNullOrWhiteSpace(PasswordEntry) ||
                string.IsNullOrWhiteSpace(EmailEntry))
            {
                Console.WriteLine("Please fill in all fields.");
                ErrorMessage = "Por favor, llena todos los campos";
                IsLoading = false;
                return;
            }
            IsLoading = true; 
            try
            {
                var registroData = new Registro
                {
                    Nombre = NombreEntry,
                    Password = PasswordEntry,
                    Email = EmailEntry
                };

                var response = await authService.RegisterUserService(registroData);

                if (response != null)
                {
                    Console.WriteLine("Registration successful");
                    PasswordEntry = String.Empty;
                    EmailEntry = String.Empty;
                    await Shell.Current.GoToAsync($"//login", true);

                }
                else
                {
                    Console.WriteLine("Registration failed.");
                  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                ErrorMessage = ex.Message; 

            }
            finally
            {
                IsLoading = false; 
            }
        }

        [RelayCommand]

        public async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync("//register");
        }


    }
}
