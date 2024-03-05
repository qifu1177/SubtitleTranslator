using App.DataAccess.Services;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SubtitleTranslator.ContentPages;
using SubtitleTranslator.Services;
using SubtitleTranslator.ViewModels;
using SubtitleTranslator.ViewModels.PopupViewModels;
using SubtitleTranslator.Views;

namespace SubtitleTranslator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
           
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.RegisterServices().RegisterViewModels().RegisterViews();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<PathHelp>();
            builder.Services.AddSingleton<ILocalService, LocalService>();
            builder.Services.AddTransient<ISettingSerivice, SettingSerivice>();
            builder.Services.AddTransient<ISetup, AppSetup>();
            builder.Services.AddScoped<AppService>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<TextViewModel>();
            builder.Services.AddSingleton<UiSettingViewModel>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddScoped<IntroductionViewModel>();
            builder.Services.AddScoped<EditSubtitleViewModel>();
            builder.Services.AddSingleton<SizeViewModel>();
            builder.Services.AddScoped<WaitingViewModel>();
            builder.Services.AddScoped<ExportFileTypeViewModel>();
            return builder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {            
            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddScoped<SettingPage>();
            builder.Services.AddScoped<IntroductionPage>();
            return builder;
        }
       
        
    }
}
