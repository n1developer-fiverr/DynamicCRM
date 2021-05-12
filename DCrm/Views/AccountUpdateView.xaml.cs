using DCrm.Helpers;
using DCrm.Models;
using DCrm.ViewModels;
using Xamarin.Forms;

namespace DCrm.Views
{
    public partial class AccountUpdateView : ContentPage
    {
        public AccountUpdateView(Account account)
        {
            InitializeComponent();

            ((UpdateAccountViewModel)BindingContext).Navigation = Navigation;
            ((UpdateAccountViewModel)BindingContext).SetAccount(account);
        }
    }
}
