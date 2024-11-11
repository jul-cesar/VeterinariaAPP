using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class Login : ContentPage
{
	public Login(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;	
	}
}