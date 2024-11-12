using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VeterinariaAPP.Models;
using VeterinariaAPP.Services;

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
}
