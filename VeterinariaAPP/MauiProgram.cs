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
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<AuthViewModel>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ServicesViewModel>();

            builder.Services.AddSingleton<ServicesService>();

            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<Perfil>();

            builder.Services.AddSingleton<ServiciosView>();




#if DEBUG
            builder.Logging.AddDebug();
#endif  

            return builder.Build();
        }
    }
}
