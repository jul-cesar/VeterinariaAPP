using Microsoft.Extensions.Logging;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Services (singleton for shared resources or state)
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ServicesService>();

            // View Models
            builder.Services.AddSingleton<AuthViewModel>();       // Singleton for auth-related state
            builder.Services.AddSingleton<MainViewModel>();       // Singleton for main app state
            builder.Services.AddTransient<ServicesViewModel>();   // Transient if `ServicesViewModel` is specific to a view

            // Views
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<Perfil>();
            builder.Services.AddSingleton<ServiciosView>();
            builder.Services.AddTransient<DetailsServicioView>();  // Transient if used with specific parameters each time





#if DEBUG
            builder.Logging.AddDebug();
#endif  

            return builder.Build();
        }
    }
}
