using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using VeterinariaAPP.Models;
using VeterinariaAPP.Services;
using VeterinariaAPP.Views;

namespace VeterinariaAPP.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ServicesService _servicesService;


    public MainViewModel(ServicesService servicesService)
    {
        _servicesService = servicesService;
        LoadUserDataCommand.Execute(null);
    }

    [ObservableProperty]
    private string nombreUser;
    [ObservableProperty]
    private string emailUser;
    [ObservableProperty]
    private string idUser;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private ObservableCollection<Notificacion> notis = new();

    [RelayCommand]
    public async Task LoadUserData()
    {
        var nombre = await SecureStorage.GetAsync("nombre");
        var email = await SecureStorage.GetAsync("email");
        var id = await SecureStorage.GetAsync("id");

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id))
        {
            await Logout();
        }
        else
        {
            NombreUser = nombre;
            EmailUser = email;
            IdUser = id;
           
        }
    }

    [RelayCommand]
    public async Task Logout()
    {
        SecureStorage.Remove("auth_token");
        SecureStorage.Remove("nombre");
        SecureStorage.Remove("email");

        await Shell.Current.GoToAsync("//login");
    }
  

    [RelayCommand]
    public async Task GetNotificaciones()
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

            var notisResponse = await _servicesService.GetNotificacionesUser(id);
            // Limpia la colección actual y añade las nuevas notificaciones.
            Notis.Clear();
            if (notisResponse != null)
            {
                foreach (var notificacion in notisResponse)
                {
                    Notis.Add(notificacion);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Error retrieving notifications: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]

    public async Task VerHistorial()
    {
        await Shell.Current.GoToAsync("citas");
    }
}
