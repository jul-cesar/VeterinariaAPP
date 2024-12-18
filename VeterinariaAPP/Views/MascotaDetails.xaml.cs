using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class MascotaDetails : ContentPage
{
	public MascotaDetails(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as UserViewModel;
        if (viewModel != null)
        {
            await viewModel.GetMascotaCommand.ExecuteAsync(null);
        }
    }
}