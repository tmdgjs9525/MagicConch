﻿using CommunityToolkit.Mvvm.DependencyInjection;
using MagicConch.Core;
using MagicConch.Helper;
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
using ServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;

namespace MagicConch
{
    public sealed partial class App : Application
    {
        private Kernel? kernel;

        public App()
        {
            this.InitializeComponent();

            Startup += onStartup;

            //kernel = semanticKernelBuild();

          
        }

        private async void onStartup(object sender, StartupEventArgs e)
        {
            await loadFonts();

            IServiceProvider provider = serviceInitialize();

            var mainView = provider.GetRequiredService<MainPage>();
            mainView.DataContext = provider.GetRequiredService<MainPageViewModel>();

            Window.Current.Content = mainView;
        }

        private IServiceProvider serviceInitialize()
        {
            ServiceCollection services = new ServiceCollection();

            IServiceProvider provider = configureService(services);

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

        private ServiceProvider configureService(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<MainPageViewModel>();

            Container container = new Container(services);

            container.AddSingletonNavigation<MainView, MainViewModel>();
            container.AddSingletonNavigation<ConchView, ConchViewModel>();

            if (kernel is not null)
            {
                var ichat = kernel.Services.GetRequiredService<IChatCompletionService>();

                services.AddSingleton<IChatCompletionService>(ichat);
            }

            return services.BuildServiceProvider();
        }

        //App.xaml 리소스에 폰트를 등록하게 되면 페이지 로드후에 폰트가 적용되기 때문에 
        //미리 폰트를 로드합니다.
        private static async Task loadFonts()
        {
            await LoadFontHelper.LoadFont("/MagicConch;component/Assets/Fonts/BASKVILL.ttf#Baskerville Old Face", "BASKVILL");
            await LoadFontHelper.LoadFont("/MagicConch;component/Assets/Fonts/GothamLight.ttf#Gotham", "GothamLight");
            await LoadFontHelper.LoadFont("/MagicConch;component/Assets/Fonts/GothamBook.ttf#Gotham", "GothamBook");
        }
    }
}
