using DCrm.Helpers;
using Xamarin.Forms;

namespace DCrm
{
    public partial class Login : ContentPage
    {
        public Login()
        {

            InitializeComponent();

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var auth = DependencyService.Get<IMsAuthenticator>();

            if (!(await auth.IsAuthenticated(App.Authority)))
            {
                var result = await auth.Authenticate(App.Authority, App.Resource, App.ClientId, App.ReturnUri);

                await DisplayAlert("Message", result.AccessToken, "Cancel");
            }
        }

    }
}
