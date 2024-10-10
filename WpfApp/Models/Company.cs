using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Company : INotifyPropertyChanged
    {
        private int id { get; set; }
        private string name_company { get; set; } = null!;
        private ICollection<User>? users { get; set; }

        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
        public string NameCompany { get => name_company; set { name_company = value; OnPropertyChanged("NameCompany"); } }
        public ICollection<User>? Users { get => users; set { users = value; OnPropertyChanged("Users"); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
