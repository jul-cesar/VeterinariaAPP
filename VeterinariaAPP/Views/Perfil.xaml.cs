using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class Perfil : ContentPage
{
	public Perfil(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}