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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ServicesViewModel viewModel)
            {
                if (!string.IsNullOrEmpty(viewModel.IdServicio))
                {
                    viewModel.GetServicioCommand.Execute(null);
                }
            }
        }


    }
}
