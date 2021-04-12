using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DCrm.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DCrm.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }

        public MainPageViewModel()
        {
            Accounts = new ObservableCollection<Account> { new Account { Address = "address1", Name = "Name" } };

            Task.Run(async () =>
            {
                await Update();
            });

            AccountSelected = new Command<Account>(async (a) =>
            {
                await Application.Current.MainPage.DisplayAlert("Title", a.Name, "OK");
            }, (a) => true);
        }

        public async Task Update()
        {
            Crm c = new Crm();

            await c.Setup();

            var accounts = await c.GetAccounts();

            accounts.ForEach(a => Accounts.Add(a));
        }

        public ICommand AccountSelected { get; set; }

    }
}
