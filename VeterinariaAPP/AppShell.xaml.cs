using VeterinariaAPP.Views;

namespace VeterinariaAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("details", typeof(DetailsServicioView));
            Routing.RegisterRoute("servicios", typeof(ServiciosView));
            Routing.RegisterRoute("login", typeof(Login));
            Routing.RegisterRoute("register", typeof(Register));
            Routing.RegisterRoute("main", typeof(MainView));
            Routing.RegisterRoute("apartar", typeof(ApartarCita));



        }
    }
}
