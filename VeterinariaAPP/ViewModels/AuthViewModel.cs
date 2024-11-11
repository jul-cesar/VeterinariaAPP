using Android.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VeterinariaAPP.Models.Auth;
using VeterinariaAPP.Services;

namespace VeterinariaAPP.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private readonly AuthService authService;

        public AuthViewModel(AuthService authService)
        {
            this.authService = authService;
        }

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string nombreEntry;

        [ObservableProperty]
        private string passwordEntry;

        [ObservableProperty]
        private string emailEntry;

        [RelayCommand]
        public async Task RegisterUser()
        {
            Console.WriteLine("COMMAND WORKING"); 
            IsLoading = true; // Set loading state
            try
            {
                // Create the Registro object
                var registroData = new Registro
                {
                    Nombre = NombreEntry,
                    Password = PasswordEntry,
                    Email = EmailEntry
                };

                // Call the RegisterUser method in AuthService
                var response = await authService.RegisterUserService(registroData);

                if (response != null)
                {
                    // Handle successful registration (e.g., navigate to login, show success message)
                    Console.WriteLine("Registration successful");
                   
                }
                else
                {
                    // Handle null response if registration failed
                    Console.WriteLine("Registration failed.");
                }
            }
            catch (Exception ex)
            {
                // Log or display error message
                Console.WriteLine($"Error during registration: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Reset loading state
            }
        }
    }
}
