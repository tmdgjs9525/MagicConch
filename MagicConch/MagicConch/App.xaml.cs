using CommunityToolkit.Mvvm.DependencyInjection;
using MagicConch.Core;
using MagicConch.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Windows;

namespace MagicConch
{
    public sealed partial class App : Application
    {
        private Kernel kernel;

        public App()
        {
            this.InitializeComponent();

            //semanticKernelBuild();

            IServiceProvider provider = serviceInitialize();

            var mainView = provider.GetRequiredService<MainPage>();
            mainView.DataContext = provider.GetRequiredService<MainPageViewModel>();

            Window.Current.Content = mainView;
        }

        private IServiceProvider serviceInitialize()
        {
            ServiceCollection services = new ServiceCollection();

            //var ichat = kernel.Services.GetRequiredService<IChatCompletionService>();

            //services.AddSingleton<IChatCompletionService>(ichat);

            IServiceProvider provider = Configure.ConfigureService(services);

            Ioc.Default.ConfigureServices(provider);

            return provider;
        }

        private Kernel semanticKernelBuild()
        {
            string modelId = string.Empty;
            string apiKey = string.Empty;

            var builder = Kernel.CreateBuilder();

            builder.AddOpenAIChatCompletion("modelId", "apiKey");

            kernel = builder.Build();   
           

            return kernel;
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
