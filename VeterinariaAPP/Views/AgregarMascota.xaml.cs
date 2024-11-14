using Mopups.Pages;
using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class AgregarMascota : PopupPage
{
	public AgregarMascota(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}