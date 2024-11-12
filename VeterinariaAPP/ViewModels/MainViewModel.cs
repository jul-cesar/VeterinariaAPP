
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VeterinariaAPP.ViewModels;

    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
    {
        LoadUserDataCommand.Execute(null);

    }

   
    [ObservableProperty]
    private string nombreUser;
    [ObservableProperty]
    private string emailUser;
    [ObservableProperty]
    private string idUser;

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





}

