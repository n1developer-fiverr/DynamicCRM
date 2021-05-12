using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Android.Content;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace DCrm.Droid
{
    [Activity(Label = "DCrm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
            //Uri uri = new Uri(data.GetStringExtra("ReturnedUrl"));

            //Console.WriteLine("Here");
            //Console.WriteLine(uri.PathAndQuery);
            //var auth = DependencyService.Get<IAuthenticator>();
            //var code1 = uri.PathAndQuery.ParseQueryString()["code"];
            //var code = WebUtility.UrlDecode(uri.PathAndQuery).ParseQueryString()["code"];

            //Console.WriteLine(code1);
            //Console.WriteLine(code);
            //Console.WriteLine(code1);
            //Console.WriteLine(code);
            //auth.RiseAuthenication(code);
        }

    }
    public static class UriExtensions
    {
        private static readonly Regex _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");

        public static IReadOnlyDictionary<string, string> ParseQueryString(this string uri)
        {
            var match = _regex.Match(uri);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                Console.WriteLine(match.Groups[1].Value);
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }
}