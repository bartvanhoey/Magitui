﻿using System.IO;
using System.Reflection;
using System.Text.Json;
using Magitui.Configuration;
using Magitui.Views;
using Magitui.Views.Debts;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace Magitui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("addSavingsEntry", typeof(AddSavingsEntryPage));
            Routing.RegisterRoute("addDebtEntry", typeof(AddDeptEntryPage));

            MainPage = new AppShell();
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
}