using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class MascotaDetails : ContentPage
{
	public MascotaDetails(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UserViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.IdMascota))
            {
                viewModel.GetMascotaCommand.Execute(null);
            }
        }
    }
}