using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class ServiciosView : ContentPage
{
	public ServiciosView(ServicesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
		viewModel.GetServiciosCommand.Execute(null);
	}
}