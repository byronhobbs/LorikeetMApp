using Android.App;
using Android.Content.PM;
using Android.OS;
using FormsPinView.Droid;

namespace LorikeetMApp.Droid
{
	[Activity(Label = "LorikeetMApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static object Instance { get; internal set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            PinItemViewRenderer.Init();
            LoadApplication(new App());
        }
    }
}

