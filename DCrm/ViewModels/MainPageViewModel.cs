using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using DCrm.Helpers;
using Xamarin.Forms;

namespace DCrm.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }

        public MainPageViewModel()
        {
            Accounts = new ObservableCollection<Account> { new Account { AllowUpdate=false,Address = "Wait", Name = "Loading" } };

            Task.Run(async () =>
            {
                await Update();
            });

            AccountSelected = new Command<Account>(async (a) =>
            {
                if (!a.AllowUpdate)
                    return;

                var result = await UserDialogs.Instance.PromptAsync(new PromptConfig
                {
                    InputType = InputType.Name,
                    OkText = "Change",
                    Title = "Enter New Column Break",
                    Text = a.ColumnBreak
                });

                if(result.Ok && result.Text != null && !result.Text.Trim().Equals(""))
                {
                    a.ColumnBreak = result.Text;

                    isUpdating = true;

                    var ok = await crm.Update(a);

                    var message = ok ? "Account Updated!" : "Unable to update!";

                    await UserDialogs.Instance.AlertAsync(new AlertConfig
                    {
                        Title="Message",
                        Message= message,
                        OkText="Ok"
                    });
                    isUpdating= false;
                }
            }, _ => !isUpdating);
        }

        private bool isUpdating = false;

        private Crm crm;

        public async Task Update()
        {
            crm = new Crm();

            await crm.Setup();

            var accounts = await crm.GetAccounts();
            Accounts.RemoveAt(0);
            accounts.ForEach(a => Accounts.Add(a));
        }

        public ICommand AccountSelected { get; set; }

    }
}
