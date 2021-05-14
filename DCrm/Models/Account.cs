using System;
using System.ComponentModel;

namespace DCrm.Models
{
    public class Account:INotifyPropertyChanged
    {
        private string _columnBreak;

        public string Name { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string ColumnBreak { get=>_columnBreak; set {
                if (value == null)
                    return;

                _columnBreak = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColumnBreak)));
            }
        }
        public bool AllowUpdate { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
