using Magitui.Services.Calculator;
using Magitui.Services.File;
using Magitui.Services.RepoContent;
using Magitui.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services.Infrastructure;

public static class AddServices
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {

        builder.Services.AddSingleton<ISavingsFileService, SavingsFileService>();
        builder.Services.AddSingleton<ICalculatorService, CalculatorService>();
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<IGitHubInfoService, GitHubInfoService>();


        //        builder.Services.AddBlazorWebView();
        //        builder.Services.AddSingleton<SubscriptionsService>();
        //        builder.Services.AddHttpClient<ShowsService>(client =>
        //        {
        //            client.BaseAddress = new Uri(Config.APIUrl);
        //        });
        //        builder.Services.AddSingleton<ListenLaterService>();
        //#if WINDOWS
        //            builder.Services.TryAddSingleton<IAudioService, Platforms.Windows.AudioService>();
        //            builder.Services.TryAddTransient<IShareService, Platforms.Windows.ShareService>();
        //#elif ANDROID
        //        builder.Services.TryAddSingleton<IAudioService, Platforms.Android.AudioService>();
        //#elif MACCATALYST
        //            builder.Services.TryAddSingleton<IAudioService, Platforms.MacCatalyst.AudioService>();
        //            builder.Services.TryAddSingleton< Platforms.MacCatalyst.ConnectivityService>();
        //#elif IOS
        //        builder.Services.TryAddSingleton<IAudioService, Platforms.iOS.AudioService>();
        //#endif

        //        builder.Services.TryAddTransient<WifiOptionsService>();
        //        builder.Services.TryAddSingleton<PlayerService>();

        //        builder.Services.AddScoped<ThemeInterop>();
        //        builder.Services.AddScoped<ClipboardInterop>();
        //        builder.Services.AddScoped<ListenTogetherHubClient>(_ =>
        //            new ListenTogetherHubClient(Config.ListenTogetherUrl));


        return builder;
    }
}
