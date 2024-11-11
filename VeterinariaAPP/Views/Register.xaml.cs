using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class Register : ContentPage
{
	public Register(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}

    
}