using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using VeterinariaAPP.Models;
using VeterinariaAPP.Models.Auth.Citas;
using VeterinariaAPP.Services;
using VeterinariaAPP.Views;

namespace VeterinariaAPP.ViewModels;

[QueryProperty(nameof(IdServicio), "idServicio")]
public partial class ServicesViewModel : ObservableObject
{
    private readonly ServicesService serviceServices;
    private readonly IServiceProvider provider;

    public ServicesViewModel(ServicesService service, IServiceProvider provider)
    {
        serviceServices = service;
        this.provider = provider;   
    }

    [ObservableProperty]
    private ObservableCollection<Servicio> servicios = new();

    [ObservableProperty]
    private ObservableCollection<Mascota> mascotas = new();

    [ObservableProperty]
    private ObservableCollection<Disponibilidad> dispos = new();

    [ObservableProperty]
    private Mascota mascotaSeleccionada;

    [ObservableProperty]
    private Disponibilidad fechaDisponibleSeleccionada;
    [ObservableProperty]
    private string descripcion;
    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private Servicio servicioActual;

    [ObservableProperty]
    private string idServicio;

    [ObservableProperty]
    private bool isLoading;
    [ObservableProperty]
    private string metodoPagoSeleccionado;
    [ObservableProperty]
    private string[] metodosPago = new string[]
        {
        "TarjetaDeCredito",
        "TarjetaDeDebito",
        "TransferenciaBancaria",
        "Efectivo"
        };
    [ObservableProperty]
    private string monto;



    [RelayCommand]
    public async Task GetServiciosAsync()
    {
        try
        {
            IsLoading = true;

            var serviciosResponse = await serviceServices.GetServiciosService();

          
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
                ServicioActual = servicioResponse; 
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
    public async Task GetDispos()
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

            var disposResponse = await serviceServices.GetDisposService(ServicioActual.IdServicio);
            Dispos = new ObservableCollection<Disponibilidad>(disposResponse ?? new List<Disponibilidad>());
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
    public async Task ApartarCita()
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            Console.WriteLine("Please fill in all fields.");
            ErrorMessage = "Por favor, llena todos los campos";
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
                IsLoading = false;
                return;
            }

            if (FechaDisponibleSeleccionada == null || MascotaSeleccionada == null)
            {
                ErrorMessage = "Por favor, selecciona una mascota y una fecha.";
                IsLoading = false;
                return;
            }

            if (servicioActual == null)
            {
                ErrorMessage = "Servicio no encontrado.";
                IsLoading = false;
                return;
            }

            var citaData = new CrearCita
            {
                descripcion = Descripcion,
                id_disponibilidad = FechaDisponibleSeleccionada.id_disponibilidad,
                id_mascota = MascotaSeleccionada.IdMascota,
                id_servicio = servicioActual.IdServicio,
                id_usuario = idUser,
                metodo_pago = MetodoPagoSeleccionado,
                monto = Monto
                
            };

            var response = await serviceServices.ApartarCitaService(citaData);

            if (response != null)
            {
                Console.WriteLine("Cita apartada con éxito.");
                await Shell.Current.GoToAsync("///servicios");
                await MopupService.Instance.PopAsync();

                await Shell.Current.DisplayAlert("Exito", "Cita apartada correctamente", "Ok");
                Descripcion = string.Empty;
                Monto = string.Empty;
                MascotaSeleccionada = new();
                MetodoPagoSeleccionado = string.Empty;


            }
            else
            {
                Console.WriteLine("Fallo al apartar la cita.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante la reserva de cita: {ex.Message}");
            ErrorMessage = ex.Message;
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
        await GetDispos();
        await MopupService.Instance.PushAsync(provider.GetRequiredService<ApartarCita>());
    }
}
