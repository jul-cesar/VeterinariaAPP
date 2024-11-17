using VeterinariaAPP.Models;
using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class ServiciosView : ContentPage
{
	public ServiciosView(ServicesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as ServicesViewModel;
        if (viewModel != null)
        {
            await viewModel.GetServiciosCommand.ExecuteAsync(null);
        }

    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if(e.CurrentSelection.FirstOrDefault() is Servicio servicio)
		{
			collectionViewServicios.SelectedItem = null;
            await Shell.Current.GoToAsync($"details?idServicio={servicio.IdServicio}");
		}
    }
}