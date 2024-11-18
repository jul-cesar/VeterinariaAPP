using System;
using Microsoft.Extensions.DependencyInjection;
using VeterinariaAPP.ViewModels;
using VeterinariaAPP.Views;

namespace VeterinariaAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
