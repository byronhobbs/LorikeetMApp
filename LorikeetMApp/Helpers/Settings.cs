using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LorikeetMApp.Helpers
{
	public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static bool IsInitialized
        {
			get => AppSettings.GetValueOrDefault(nameof(IsInitialized), false);
			set => AppSettings.AddOrUpdateValue(nameof(IsInitialized), value);
        }

		public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

		public static string Pin
        {
            get => AppSettings.GetValueOrDefault(nameof(Pin), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Pin), value);
        }

		public static string typeOfLogin
        {
			get => AppSettings.GetValueOrDefault(nameof(typeOfLogin), string.Empty);
			set => AppSettings.AddOrUpdateValue(nameof(typeOfLogin), value);
        }
    }
}
