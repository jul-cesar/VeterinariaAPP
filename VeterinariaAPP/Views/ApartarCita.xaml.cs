using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class ApartarCita : ContentPage
{
	public ApartarCita(ServicesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}