using System;
using System.ComponentModel;
using System.Windows.Input;
using DCrm.Helpers;
using DCrm.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DCrm.ViewModels
{
    public class UpdateAccountViewModel:INotifyPropertyChanged
    {
        private Account _account;
        public INavigation Navigation { get; set; }
        
        public UpdateAccountViewModel()
        {
            UpdateCommand = new Command(Update);
        }

        public Account Account
        {
            get => _account;
            set
            {
                if (value == null)
                    return;

                _account = value;

                OnPropertyChanged(nameof(Account));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetAccount(Account account)
        {
            Account = account;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand UpdateCommand { get; set; }

        private async void Update()
        {
            var crm = Crm.AuthenticatedCrmService;

            Console.WriteLine(JsonConvert.SerializeObject(Account));

            await crm.Update(Account);

            await Navigation.PopAsync();
        }
    }
}
