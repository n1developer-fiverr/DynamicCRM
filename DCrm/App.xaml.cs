using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCrm
{
    public partial class App : Application
    {
        public static string ClientId = "b8d6f914-882a-40a9-a035-4e9ef4e3caa2";
        public static string TenantId = "5ff5e38b-6561-4f85-adf9-1c7d92ba2848";
        public static string Resource = "https://krplastics.api.crm.dynamics.com/";

        public static string Authority = $"https://login.microsoftonline.com/{TenantId}";
        public static string ReturnUri = "http://localhost";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
