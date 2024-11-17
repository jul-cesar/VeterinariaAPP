using System.ComponentModel;
using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views
{
    public partial class DetailsServicioView : ContentPage
    {

        public DetailsServicioView(ServicesViewModel viewModel)
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
                await viewModel.GetServicioCommand.ExecuteAsync(null);
            }
        }


    }
}
