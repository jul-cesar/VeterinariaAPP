using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class MascotasView : ContentPage
{
	public MascotasView(UserViewModel viewModel)
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
            await viewModel.GetMascotasUserCommand.ExecuteAsync(null);
        }
    }
}