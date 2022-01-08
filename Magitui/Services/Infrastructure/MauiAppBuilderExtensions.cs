using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services;

public static class MauiAppBuilderExtensions
{
    public interface IMyMauiAppBuilder
    {
        IMyMauiAppBuilder UseMapServiceToken(string token);

        IMyMauiAppBuilder AddAppAction(AppAction appAction);

        IMyMauiAppBuilder OnAppAction(Action<AppAction> action);

        IMyMauiAppBuilder UseVersionTracking();

        IMyMauiAppBuilder UseLegacySecureStorage();
    }

    public static MauiAppBuilder ConfigureAppBuilder(this MauiAppBuilder builder, Action<IMyMauiAppBuilder> configureDelegate = null)
    {
        if (configureDelegate != null)
        {
            builder.Services.AddSingleton<MyMauiAppRegistration>(new MyMauiAppRegistration(configureDelegate));
        }
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IMauiInitializeService, MyMauiAppInitializer>());

        builder.ConfigureLifecycleEvents(life =>
        {
#if __ANDROID__
                Platform.Init(MauiApplication.Current);

            life.AddAndroid(android => android
                .OnRequestPermissionsResult((activity, requestCode, permissions, grantResults) =>
                {
                    Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                })
                .OnNewIntent((activity, intent) =>
                {
                    Platform.OnNewIntent(intent);
                })
                .OnResume((activity) =>
                {
                    Platform.OnResume();
                }));
#elif __IOS__
                life.AddiOS(ios => ios
                    .ContinueUserActivity((application, userActivity, completionHandler) =>
                    {
                        return Platform.ContinueUserActivity(application, userActivity, completionHandler);
                    })
                    .OpenUrl((application, url, options) =>
                    {
                        return Platform.OpenUrl(application, url, options);
                    })
                    .PerformActionForShortcutItem((application, shortcutItem, completionHandler) =>
                    {
                        Platform.PerformActionForShortcutItem(application, shortcutItem, completionHandler);
                    }));
#elif WINDOWS
				life.AddWindows(windows => windows
					.OnActivated((window, args) =>
					{
						Platform.OnActivated(window, args);
					})
					.OnLaunched((application, args) =>
					{
						Platform.OnLaunched(args);
					}));
#endif
            });

        return builder;
    }

    public static IMyMauiAppBuilder AddAppAction(this IMyMauiAppBuilder MyMauiApp, string id, string title, string subtitle = null, string icon = null) =>
         MyMauiApp.AddAppAction(new AppAction(id, title, subtitle, icon));

    internal class MyMauiAppRegistration
    {
        private readonly Action<IMyMauiAppBuilder> _registerMyMauiApp;

        public MyMauiAppRegistration(Action<IMyMauiAppBuilder> registerMyMauiApp)
        {
            _registerMyMauiApp = registerMyMauiApp;
        }

        internal void RegisterMyMauiAppOptions(IMyMauiAppBuilder MyMauiApp)
        {
            _registerMyMauiApp(MyMauiApp);
        }
    }


    class MyMauiAppInitializer : IMauiInitializeService
    {
        private readonly IEnumerable<MyMauiAppRegistration> _MyMauiAppRegistrations;
        private MyMauiAppBuilder _MyMauiAppBuilder;

        public MyMauiAppInitializer(IEnumerable<MyMauiAppRegistration> MyMauiAppRegistrations)
        {
            _MyMauiAppRegistrations = MyMauiAppRegistrations;
        }

        public async void Initialize(IServiceProvider services)
        {
            _MyMauiAppBuilder = new MyMauiAppBuilder();
            if (_MyMauiAppRegistrations != null)
            {
                foreach (var MyMauiAppRegistration in _MyMauiAppRegistrations)
                {
                    MyMauiAppRegistration.RegisterMyMauiAppOptions(_MyMauiAppBuilder);
                }
            }

#if WINDOWS
				Platform.MapServiceToken = _MyMauiAppBuilder.MapServiceToken;
#elif __ANDROID__
            //SecureStorage.LegacyKeyHashFallback = _MyMauiAppBuilder.UseLegaceSecureStorage;
#endif

            AppActions.OnAppAction += HandleOnAppAction;

            try
            {
                await AppActions.SetAsync(_MyMauiAppBuilder.AppActions);
            }
            catch (FeatureNotSupportedException ex)
            {
                services.GetService<ILoggerFactory>()?
                    .CreateLogger<IMyMauiAppBuilder>()?
                    .LogError(ex, "App Actions are not supported on this platform.");
            }

            if (_MyMauiAppBuilder.TrackVersions)
                VersionTracking.Track();
        }

        void HandleOnAppAction(object sender, AppActionEventArgs e)
        {
            _MyMauiAppBuilder.AppActionHandlers?.Invoke(e.AppAction);
        }
    }

    class MyMauiAppBuilder : IMyMauiAppBuilder
    {
        internal readonly List<AppAction> AppActions = new List<AppAction>();
        internal Action<AppAction> AppActionHandlers;
        internal bool TrackVersions;

#pragma warning disable CS0414 // Remove unread private members
        internal bool UseLegaceSecureStorage;
        internal string MapServiceToken;
#pragma warning restore CS0414 // Remove unread private members

        public IMyMauiAppBuilder UseMapServiceToken(string token)
        {
            MapServiceToken = token;
            return this;
        }

        public IMyMauiAppBuilder AddAppAction(AppAction appAction)
        {
            AppActions.Add(appAction);
            return this;
        }

        public IMyMauiAppBuilder OnAppAction(Action<AppAction> action)
        {
            AppActionHandlers += action;
            return this;
        }

        public IMyMauiAppBuilder UseVersionTracking()
        {
            TrackVersions = true;
            return this;
        }

        public IMyMauiAppBuilder UseLegacySecureStorage()
        {
            UseLegaceSecureStorage = true;
            return this;
        }
    }


}




