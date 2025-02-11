using CommunityToolkit.Mvvm.DependencyInjection;
using MagicConch.Core;
using MagicConch.ViewModels;
using MagicConch.Views;
using MagicConch.Views.Conch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Windows;

namespace MagicConch
{
    public sealed partial class App : Application
    {
        private Kernel? kernel;

        public App()
        {
            this.InitializeComponent();

            //kernel = semanticKernelBuild();

            IServiceProvider provider = serviceInitialize();

            var mainView = provider.GetRequiredService<MainPage>();
            mainView.DataContext = provider.GetRequiredService<MainPageViewModel>();

            Window.Current.Content = mainView;
        }

        private IServiceProvider serviceInitialize()
        {
            ServiceCollection services = new ServiceCollection();

            IServiceProvider provider = ConfigureService(services);

            Ioc.Default.ConfigureServices(provider);

            return provider;
        }

        private Kernel semanticKernelBuild()
        {
            string modelId = string.Empty;
            string apiKey = string.Empty;

            var builder = Kernel.CreateBuilder();

            builder.AddOpenAIChatCompletion("", "");
         
            kernel = builder.Build();   

            return kernel;
        }

        public IServiceProvider ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<MainPageViewModel>();

            Container container = new Container(services);

            container.AddTransientNavigation<MainView, MainViewModel>();
            container.AddSingletonNavigation<ConchView, ConchViewModel>();

            if (kernel is not null)
            {
                var ichat = kernel.Services.GetRequiredService<IChatCompletionService>();

                services.AddSingleton<IChatCompletionService>(ichat);
            }

            return services.BuildServiceProvider();
        }
    }

    internal static class Configure
    {
        
    }
}
