using CommunityToolkit.Mvvm.DependencyInjection;
using MagicConch.Core;
using MagicConch.ViewModels;
using MagicConch.Views;
using MagicConch.Views.Conch;
using MagicConch.Views.Title;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MagicConch
{
    public sealed partial class App : Application
    {
        private Kernel? kernel;

        public App()
        {
            this.InitializeComponent();

            Startup += OnStartup;

            //kernel = semanticKernelBuild();

          
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await LoadFonts();

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

        private IServiceProvider ConfigureService(IServiceCollection services)
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


        private async Task LoadFonts()
        {
            await FontFamily.LoadFontAsync("/MagicConch;component/Assets/Fonts/BASKVILL.ttf#Baskerville Old Face");
            await FontFamily.LoadFontAsync("/MagicConch;component/Assets/Fonts/GothamLight.ttf#Gotham");
            await FontFamily.LoadFontAsync("/MagicConch;component/Assets/Fonts/GothamBook.ttf#Gotham");


            var customFont = new FontFamily("/MagicConch;component/Assets/Fonts/BASKVILL.ttf#Baskerville Old Face");
            Application.Current.Resources["BASKVILL"] = customFont;

            var customFont2 = new FontFamily("/MagicConch;component/Assets/Fonts/GothamLight.ttf#Gotham");
            Application.Current.Resources["GothamLight"] = customFont;

            var customFont3 = new FontFamily("/MagicConch;component/Assets/Fonts/GothamBook.ttf#Gotham");
            Application.Current.Resources["GothamBook"] = customFont;
        }
    }

    internal static class Configure
    {
        
    }
}
