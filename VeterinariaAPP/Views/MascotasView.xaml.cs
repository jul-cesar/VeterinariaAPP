using VeterinariaAPP.Models;
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

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Mascota mascota)
        {
            collectionViewMascotas.SelectedItem = null;
            await Shell.Current.GoToAsync($"detailsmascota?idMascota={mascota.IdMascota}");
        }
    }
}