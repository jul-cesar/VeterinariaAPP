using VeterinariaAPP.Models;
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

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if(e.CurrentSelection.FirstOrDefault() is Servicio servicio)
		{
			collectionViewServicios.SelectedItem = null;
            await Shell.Current.GoToAsync($"details?idServicio={servicio.IdServicio}");
		}
    }
}