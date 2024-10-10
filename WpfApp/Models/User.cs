using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class User : INotifyPropertyChanged
    {
        private int _Id { get; set; }
        private string _Name { get; set; } = null!;
        private int? _Age { get; set; }
        private Company? _Company { get; set; }
        private int? _CompanyId { get; set; }

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int? Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                OnPropertyChanged("Age");
            }
        }

        public Company? Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                OnPropertyChanged("Company");
            }
        }

        public int? CompanyId
        {
            get { return _CompanyId; }
            set
            {
                _CompanyId = value;
                OnPropertyChanged("CompanyId");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
