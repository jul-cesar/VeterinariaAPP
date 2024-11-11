﻿using System;
using Microsoft.Extensions.DependencyInjection;
using VeterinariaAPP.ViewModels;
using VeterinariaAPP.Views;

namespace VeterinariaAPP
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = new NavigationPage(serviceProvider.GetRequiredService<Register>());
        }
    }
}