using Microsoft.Maui.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using VeterinariaAPP.ViewModels;

namespace VeterinariaAPP.Views;

public partial class MainView : ContentPage
{

    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as MainViewModel;
        if (viewModel != null)
        {
            await viewModel.LoadUserDataCommand.ExecuteAsync(null);
        }
    }


    }
