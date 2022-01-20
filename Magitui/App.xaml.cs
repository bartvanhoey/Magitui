using Magitui.Configuration;
using Magitui.Pages;
using System.Reflection;
using System.Text.Json;

namespace Magitui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //if (Device.Idiom == TargetIdiom.Phone)
        //    Shell.Current.CurrentItem = PhoneTabs;


        if (Config.Desktop)
            MainPage = new DesktopShell();
        else
            MainPage = new MobileShell();
    
        Routing.RegisterRoute(nameof(AddSavingsPage), typeof(AddSavingsPage));
        Routing.RegisterRoute(nameof(EditSavingsPage), typeof(EditSavingsPage));
        Routing.RegisterRoute(nameof(SavingsPage), typeof(SavingsPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

        //MainPage = new MainPage();
    }

    private static AppSettings _appSettings;
    public static AppSettings AppSettings
    {
        get
        {
            if (_appSettings == null) LoadAppSettings();
            return _appSettings;
        }
    }

    private static void LoadAppSettings()
    {
#if RELEASE
            var appSettingsResourceStream = Assembly.GetAssembly(typeof(AppSettings)).GetManifestResourceStream("Magitui.Configuration.appsettings.release.json");
#else
        var appSettingsResourceStream = Assembly.GetAssembly(typeof(AppSettings))?.GetManifestResourceStream("Magitui.Configuration.appsettings.debug.json");
#endif
        if (appSettingsResourceStream == null)
            return;

        using var streamReader = new StreamReader(appSettingsResourceStream);
        var jsonString = streamReader.ReadToEnd();
        _appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);
    }
}
