using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class Notificaciones : ContentPage
{
	public Notificaciones(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as MainViewModel;
        if (viewModel != null)
        {
            await viewModel.GetNotificacionesCommand.ExecuteAsync(null);
        }
    }
}