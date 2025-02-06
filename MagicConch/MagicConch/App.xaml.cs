using CommunityToolkit.Mvvm.DependencyInjection;
using MagicConch.Core;
using MagicConch.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MagicConch
{
    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            IServiceProvider provider = serviceInitialize();

            var mainView = provider.GetRequiredService<MainPage>();
            mainView.DataContext = provider.GetRequiredService<MainPageViewModel>();

            Window.Current.Content = mainView;
        }

        private IServiceProvider serviceInitialize()
        {
            ServiceCollection services = new ServiceCollection();

            IServiceProvider provider = Configure.ConfigureService(services);

            Ioc.Default.ConfigureServices(provider);

            return provider;
        }
    }

    internal static class Configure
    {
        public static IServiceProvider ConfigureService(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<MainPageViewModel>();

            Container container = new Container(services);

            container.AddSingletonNavigation<MainView, MainViewModel>();

            

            return services.BuildServiceProvider();
        }
    }
}
