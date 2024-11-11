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
        private readonly IServiceProvider serviceProvider;
        public AuthViewModel(AuthService auth, IServiceProvider provider)
        {
            authService = auth;

            serviceProvider = provider;
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
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(PasswordEntry) || string.IsNullOrWhiteSpace(EmailEntry))
            {
                Console.WriteLine("Please fill in all fields.");
                ErrorMessage = "Por favor, llena todos los campos";
                IsLoading = false;
                return;
            }

            IsLoading = true; // Corregido: Añadido punto y coma

            try
            {
                var loginData = new Logeo
                {
                    Email = EmailEntry,
                    Password = PasswordEntry,
                };

                // Intento de inicio de sesión llamando al servicio de autenticación
                var response = await authService.LoginUserService(loginData);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    Console.WriteLine("Login successful");
                    ErrorMessage = string.Empty; // Limpiar mensaje de error en caso de éxito

                    // Aquí puedes navegar a otra página o limpiar los campos
                    // Ejemplo: NavigationService.NavigateTo("HomePage");
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
                ErrorMessage = ex.Message; // Mostrar mensaje de error en la UI
            }
            finally
            {
                IsLoading = false; // Detener el indicador de carga
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
                    passwordEntry = String.Empty;
                    EmailEntry = String.Empty;
                    await Application.Current.MainPage.Navigation.PushAsync(serviceProvider.GetRequiredService<Login>());

                }
                else
                {
                    Console.WriteLine("Registration failed.");
                  

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                ErrorMessage = ex.Message; // Mostrar mensaje de error en la UI

            }
            finally
            {
                IsLoading = false; 
            }
        }


    }
}
