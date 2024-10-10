using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public User User { get; private set; } = null!;
        public ObservableCollection<Company?>? Companies { get; private set; }
        public UserWindow(User user, ObservableCollection<Company?>? companies)
        {
            InitializeComponent();
            Companies = companies;
            // Companies?.Insert(0, new Company() { NameCompany = "Empty"});
            // Возможно нужно CompositeCollection
            companiesComboBox.ItemsSource = Companies;
            companiesComboBox.SelectedItem = Companies?.FirstOrDefault(x => x?.Id == user?.CompanyId);
            User = user;
            DataContext = User;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
