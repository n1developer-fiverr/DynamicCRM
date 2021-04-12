using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DCrm.Helpers;
using Newtonsoft.Json;

namespace DCrm.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Accounts> Accounts;
        
        public MainPageViewModel()
        {
            Accounts = new ObservableCollection<Accounts>();
            Accounts.Add(new Accounts
            {
                Address = "address1",
                Name="Name"
            });

            Task.Run(async () =>
            {
                await Update();
            });
        }

        public async Task Update()
        {
            Crm c = new Crm();

            await c.Setup();

            var accounts = await c.GetAccounts();

            accounts.ForEach(a => Accounts.Add(a));
        }
    }
}
