using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VeterinariaAPP.Models;
using VeterinariaAPP.Services;

namespace VeterinariaAPP.ViewModels;

public partial class ServicesViewModel : ObservableObject
{
    public ObservableCollection<Servicio> Servicios { get; set; } = new();
    private readonly ServicesService serviceServices;

    public ServicesViewModel(ServicesService service)
    {
        serviceServices = service;
    }

    [ObservableProperty]
    private bool isLoading;

    [RelayCommand]
    public async Task GetServiciosAsync()
    {
        try
        {
            IsLoading = true;

            var serviciosResponse = await serviceServices.GetServiciosService();
            if (Servicios.Count > 0)
            {
                Servicios.Clear();
            }

            if (serviciosResponse != null)
            {
                foreach (var servicio in serviciosResponse)
                {
                    Servicios.Add(servicio);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Error trayendo los servicios {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
