using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Mopups.Services;
using VeterinariaAPP.Models;
using VeterinariaAPP.Services;
using VeterinariaAPP.Views;

namespace VeterinariaAPP.ViewModels;

[QueryProperty(nameof(IdServicio), "idServicio")]
public partial class ServicesViewModel : ObservableObject
{
    private readonly ServicesService serviceServices;

    public ServicesViewModel(ServicesService service)
    {
        serviceServices = service;
    }

    [ObservableProperty]
    private ObservableCollection<Servicio> servicios = new();

    [ObservableProperty]
    private ObservableCollection<Mascota> mascotas = new();

    [ObservableProperty]
    private Mascota mascotaSeleccionada;

    [ObservableProperty]
    private Servicio servicioActual = new();

    [ObservableProperty]
    private string idServicio;

    [ObservableProperty]
    private bool isLoading;

    

    // Method called whenever IdServicio changes


    [RelayCommand]
    public async Task GetServiciosAsync()
    {
        try
        {
            IsLoading = true;

            var serviciosResponse = await serviceServices.GetServiciosService();

            // Update the collection directly with new items
            Servicios = new ObservableCollection<Servicio>(serviciosResponse ?? new List<Servicio>());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Error trayendo los servicios: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task GetServicioAsync()
    {
        try
        {
            IsLoading = true;

            var servicioResponse = await serviceServices.GetServicioService(IdServicio);

            if (servicioResponse != null)
            {
                ServicioActual = servicioResponse; // Automatically updates bound properties
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
    public async Task GetMascotasUserAsync()
    {
        try
        {
            IsLoading = true;
            var id = await SecureStorage.GetAsync("id");

            if (string.IsNullOrWhiteSpace(id))
            {
                await Shell.Current.DisplayAlert("Error", "No user ID found.", "OK");
                return;
            }

            var mascotasResponse = await serviceServices.GetMascotasUserService(id);
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
    public async Task OpenApartarCitaAsync()
    {
        await GetMascotasUserAsync();
        await Shell.Current.GoToAsync("apartar");
    }
}
