﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReceptFromHolodilnik.Services;
using ReceptFromHolodilnik.ViewModels;
using ReceptFromHolodilnik;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ReceptFromHolodilnik.Services.Interfaces;

namespace ReceptFromHolodilnik
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost _host;
        public static IHost Host => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;

            await host.StopAsync().ConfigureAwait(false);
            _host = null;
        }
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services) => services
            .RegisterServices()
            .RegisterViewModels();

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
