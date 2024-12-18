﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using VeterinariaAPP.Models;
using VeterinariaAPP.Models.Auth.Citas;
using VeterinariaAPP.Services;
using VeterinariaAPP.Views;

namespace VeterinariaAPP.ViewModels
{
    [QueryProperty(nameof(IdMascota), "idMascota")]

    public partial class UserViewModel : ObservableObject
    {
        private readonly UserService userService;
        private readonly IServiceProvider provider;

        public UserViewModel(UserService _userService, IServiceProvider provider)
        {
            this.userService = _userService;
            this.provider = provider;

        }
        [ObservableProperty]

        private Mascota mascotaActual;

        [ObservableProperty]
        private string idMascota;

        [ObservableProperty]
        private ObservableCollection<Mascota> mascotas = new();
        [ObservableProperty]
        private ObservableCollection<Cita> citas = new();

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

        [RelayCommand]
        public async Task ActualizarMascota()
        {
           
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
                    Edad = MascotaActual.Edad,
                    Especie = MascotaActual.Especie,
                    IdUsuario = idUser,
                    Nombre = MascotaActual.Nombre,
                    NotasMedicas = MascotaActual.NotasMedicas,
                    Raza = MascotaActual.Raza
                };

                var response = await userService.ActualizarMascotaService(mascotaData, IdMascota);

                if (response != null)
                {
                    Console.WriteLine("Mascota actualizada con éxito.");

                   

                    await Shell.Current.DisplayAlert("Éxito", "Mascota actualizada correctamente", "Ok");
                    await Shell.Current.GoToAsync("///mascotas");

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
        [RelayCommand]
        public async Task GetMascotasUserAsync()
        {
            try
            {
                isLoading = true;
                var id = await SecureStorage.GetAsync("id");

                if (string.IsNullOrWhiteSpace(id))
                {
                    await Shell.Current.DisplayAlert("Error", "No user ID found.", "OK");
                    return;
                }

                var mascotasResponse = await userService.GetMascotasUserService(id);
                Mascotas = new ObservableCollection<Mascota>(mascotasResponse ?? new List<Mascota>());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Error retrieving pets: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
        [RelayCommand]
        public async Task GetMascota()
        {
            try
            {
                IsLoading = true;

                var servicioResponse = await userService.GetMascotaService(IdMascota);

                if (servicioResponse != null)
                {
                    MascotaActual = servicioResponse;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Error trayendo el servicio: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task AgregarMascotaPopUp()
        {

            await MopupService.Instance.PushAsync(provider.GetRequiredService<AgregarMascota>());
        }


        [RelayCommand]
        public async Task GetCitas()
        {
            try
            {
                IsLoading = true;
                var id = await SecureStorage.GetAsync("id");

                var servicioResponse = await userService.GetCitasService(id);

                if (servicioResponse != null)
                {
                    Citas = new ObservableCollection<Cita>(servicioResponse);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Error trayendo el servicio: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

}
