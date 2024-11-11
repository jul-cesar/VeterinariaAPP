using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class Register : ContentPage
{

	public Register(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }
}