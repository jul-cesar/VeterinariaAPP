using Mopups.Pages;
using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class ApartarCita : PopupPage
{
	public ApartarCita(ServicesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}