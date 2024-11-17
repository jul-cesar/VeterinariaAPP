using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using UraniumUI;
using VeterinariaAPP.Services;
using VeterinariaAPP.ViewModels;
using VeterinariaAPP.Views;

namespace VeterinariaAPP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Services (singleton for shared resources or state)
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ServicesService>();
            builder.Services.AddSingleton<UserService>();


            // View Models
            builder.Services.AddSingleton<AuthViewModel>();      
            builder.Services.AddSingleton<MainViewModel>();       
            builder.Services.AddSingleton<ServicesViewModel>();   
            builder.Services.AddSingleton<ApartarCita>();  
            builder.Services.AddSingleton<Notificaciones>();  
            builder.Services.AddSingleton<AgregarMascota>();  

            // Views
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<Perfil>();
            builder.Services.AddSingleton<MascotasView>();
            builder.Services.AddSingleton<MascotaDetails>();
            builder.Services.AddSingleton<HistorialCItasView>();


            builder.Services.AddSingleton<ServiciosView>();
            builder.Services.AddSingleton<UserViewModel>();

            builder.Services.AddSingleton<DetailsServicioView>();  





#if DEBUG
            builder.Logging.AddDebug();
#endif  

            return builder.Build();
        }
    }
}
