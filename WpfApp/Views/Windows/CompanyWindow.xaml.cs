using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        public Company Company { get; private set; } = null!;
        public CompanyWindow(Company company)
        {
            InitializeComponent();
            Company = company;
            DataContext = Company;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
