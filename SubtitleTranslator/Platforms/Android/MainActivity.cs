using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace SubtitleTranslator
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
		const int RequestCode = 123; // Unique request code for permission request
		public override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
		{
			Platform.Init(this, savedInstanceState);
			base.OnCreate(savedInstanceState, persistentState);
			
			// Check if the permission is already granted
			if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ManageExternalStorage)
				!= Permission.Granted)
			{
				// Request the permission
				ActivityCompat.RequestPermissions(this,
					new[] { Android.Manifest.Permission.ManageExternalStorage }, RequestCode);
			}
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			if (requestCode == RequestCode)
			{
				if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
				{
					// Permission granted, handle file operations here
					Console.WriteLine("Permission granted!");
				}
				else
				{
					// Permission denied, handle accordingly
					Console.WriteLine("Permission denied!");
				}
			}
		}
	}
}
