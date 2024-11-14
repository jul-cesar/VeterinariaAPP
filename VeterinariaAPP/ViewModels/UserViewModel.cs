using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using VeterinariaAPP.Models;
using VeterinariaAPP.Services;

namespace VeterinariaAPP.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly UserService userService;

        public UserViewModel(UserService _userService)
        {
            this.userService = _userService;
        }

        [ObservableProperty]
        private string razaMascota;

        [ObservableProperty]
        private string especieMascota;

        [ObservableProperty]
        private string nombreMascota;

        [ObservableProperty]
        private int edadMascota;

        [ObservableProperty]
        private string notasMedicas;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string errorMessage;

        private bool ValidarCamposRequeridos()
        {
            if (string.IsNullOrWhiteSpace(NombreMascota))
            {
                ErrorMessage = "Por favor, ingresa el nombre de la mascota.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(EspecieMascota))
            {
                ErrorMessage = "Por favor, ingresa la especie de la mascota.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(RazaMascota))
            {
                ErrorMessage = "Por favor, ingresa la raza de la mascota.";
                return false;
            }

            if (EdadMascota <= 0)
            {
                ErrorMessage = "Por favor, ingresa una edad válida para la mascota.";
                return false;
            }

            return true;
        }

        [RelayCommand]
        public async Task AgregarMascota()
        {
            if (!ValidarCamposRequeridos())
            {
                Console.WriteLine(ErrorMessage);
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
                IsLoading = false;
                return;
            }

            IsLoading = true;

            try
            {
                var idUser = await SecureStorage.GetAsync("id");

                if (string.IsNullOrWhiteSpace(idUser))
                {
                    ErrorMessage = "No se encontró el ID del usuario.";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
                    IsLoading = false;
                    return;
                }

                var mascotaData = new CrearMascota
                {
                    Edad = EdadMascota,
                    Especie = EspecieMascota,
                    IdUsuario = idUser,
                    Nombre = NombreMascota,
                    NotasMedicas = NotasMedicas,
                    Raza = RazaMascota
                };

                var response = await userService.AgregarMascotaService(mascotaData);

                if (response != null)
                {
                    Console.WriteLine("Mascota agregada con éxito.");
                    await Shell.Current.GoToAsync("///servicios");
                    await MopupService.Instance.PopAsync();

                    await Shell.Current.DisplayAlert("Éxito", "Mascota agregada correctamente", "Ok");

                    EdadMascota = 0;
                    EspecieMascota = string.Empty;
                    NombreMascota = string.Empty;
                    NotasMedicas = string.Empty;
                    RazaMascota = string.Empty;
                }
                else
                {
                    Console.WriteLine("Fallo al agregar la mascota.");
                    ErrorMessage = "Fallo al agregar la mascota.";
                    await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante agregando la mascota: {ex.Message}");
                ErrorMessage = $"Error: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
