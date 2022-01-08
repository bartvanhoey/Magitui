using Magitui.Services.Calculator;
using Magitui.Services.File;
using Magitui.Services;
using Magitui.Services.Infrastructure;

namespace Magitui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureAppBuilder()
            .ConfigureServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("fa-solid-900.otf", "FAS");
                fonts.AddFont("fa-brands-regular-400.otf", "FAB");
                fonts.AddFont("fa-regular.otf", "FAR");

            });

  
        return builder.Build();
    }
}
