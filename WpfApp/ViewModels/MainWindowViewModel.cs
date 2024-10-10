using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp.Infrastructure.Commands;
using WpfApp.Services;
using WpfApp.ViewModels.Base;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        readonly UserManager userManager;
        readonly CompanyManager companyManager;

        #region Список людей 

        private ObservableCollection<User?>? users = new ObservableCollection<User?>();
        public ObservableCollection<User?>? Users { get => users; set => Set(ref users, value); }

        public async Task GetAllUsers()
        {
            try
            {
                var convertedObjects = await userManager.GetAllUsers();
                if (convertedObjects != null)
                {
                    foreach (var user in convertedObjects)
                    {
                        Users?.Add(user);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        #endregion

        #region Список компаний

        private ObservableCollection<Company?>? companies = new ObservableCollection<Company?>();
        public ObservableCollection<Company?>? Companies { get => companies; set => Set(ref companies, value); }

        public async Task GetAllCompanies()
        {
            try
            {
                var convertedObjects = await companyManager.GetAllCompanies();
                if (convertedObjects != null)
                {
                    foreach (var company in convertedObjects)
                    {
                        Companies?.Add(company);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        #endregion

        #region Команды - пользователи

        #region AddNewUserCommand - добавление нового пользователя

        public ICommand AddNewUserCommand { get; }
        private bool CanAddNewUserCommandExecute(object? p) => true;
        private async void OnAddNewUserCommandExecuted(object? p)
        {
            UserWindow userWindow = new UserWindow(new User(), Companies);
            if (userWindow.ShowDialog() == true)
            {
                User? user = userWindow.User;
                Company? company = userWindow.companiesComboBox.SelectedValue as Company;
                user.CompanyId = company?.Id;
                try
                {
                    user = await userManager.CreateUser(user);
                    if (user?.CompanyId != null)
                        user.Company = Companies?.First(x => x?.Id == user.CompanyId);
                    Users?.Add(user);
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        #endregion

        #region EditUserCommand - редактирование пользователя

        public ICommand EditUserCommand { get; }
        private bool CanEditUserCommandExecute(object? p) => true;
        private async void OnEditUserCommandExecuted(object? p)
        {
            User? user = p as User;
            Company? oldCompany = Companies?.Where(x => x?.Id == user?.CompanyId).FirstOrDefault();
            if (user == null) return;
            UserWindow userWindow = new UserWindow(user, Companies);
            if (userWindow.ShowDialog() == true)
            {
                Company? company = userWindow.companiesComboBox.SelectedItem as Company;
                var user_t = oldCompany?.Users?.Where(x => x?.Id == user.Id).FirstOrDefault();
                if (user_t != null)
                    oldCompany?.Users?.Remove(user_t);
                userWindow.User.CompanyId = company?.Id;
                userWindow.User.Company = company;
                try
                {
                    await userManager.UpdateUser(user);
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region RemoveUser - удаление пользователя

        public ICommand RemoveUserCommand { get; }
        private bool CanRemoveUserCommandExecute(object? p) => true;
        private async void OnRemoveUserCommandExecuted(object? p)
        {
            User? user = p as User;
            if (user == null) return;
            try
            {
                var response = await userManager.RemoveUser(user.Id);
                if (response)
                {
                    Company? company = Companies?.Where(x => x?.Users?.Where(x => x.Id == user.Id).Count() > 0).FirstOrDefault();
                    if (company != null)
                    {
                        var company_t = Companies?.Where(x => x?.Id == company.Id).First();
                        var user_t = company_t?.Users?.Where(x => x.Id == user.Id).FirstOrDefault();
                        if (user_t != null)
                            company_t?.Users?.Remove(user_t);
                    }
                    Users?.Remove(user);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion

        #region Команды - компании

        #region AddNewCompany - команда добавления новой компании

        public ICommand AddNewCompanyCommand { get; }
        private bool CanAddNewCompanyCommandExecute(object? p) => true;
        private async void OnAddNewCompanyCommandExecuted(object? p)
        {
            CompanyWindow companyWindow = new CompanyWindow(new Company());
            if (companyWindow.ShowDialog() == true)
            {
                Company? company = companyWindow.Company;
                try
                {
                    company = await companyManager.CreateCompany(company);
                    Companies?.Add(company);
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region EditCompany - команда для редактирования компании

        public ICommand EditCompanyCommand { get; }
        private bool CanEditCompanyCommandExecute(object? p) => true;
        private async void OnEditCompanyCommandExecuted(object? p)
        {
            Company? company = p as Company;
            if (company == null) return;
            CompanyWindow companyWindow = new CompanyWindow(company);
            if (companyWindow.ShowDialog() == true)
            {
                try
                {
                    await companyManager.UpdateCompany(company);
                    var usersList = Users?.Where(x => x.Company?.Id == company.Id).ToList();
                    if (usersList != null)
                    {
                        foreach (var user in usersList)
                        {
                            user.Company = company;
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region RemoveCompany = команда удаления компании

        public ICommand RemoveCompanyCommand { get; }
        private bool CanRemoveCompanyCommandExecute(object? p)
        {
            return true;
        }

        private async void OnRemoveCompanyCommandExecuted(object? p)
        {
            Company? company = p as Company;
            if (company == null) return;
            if (company.Users != null && company.Users?.Count() != 0)
            {
                MessageBox.Show("В компании работают люди, нельзя удалить компанию!");
                return;
            }
            try
            {
                var response = await companyManager.RemoveCompany(company.Id);
                if (response)
                {
                    Companies?.Remove(company);
                }
                else
                {

                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Подключение к базе данных отсутсвует, попробуйте перезапустить программу.");
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            userManager = new UserManager();
            companyManager = new CompanyManager();
            InitCollections();
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            EditUserCommand = new RelayCommand(OnEditUserCommandExecuted, CanEditUserCommandExecute);
            RemoveUserCommand = new RelayCommand(OnRemoveUserCommandExecuted, CanRemoveUserCommandExecute);

            AddNewCompanyCommand = new RelayCommand(OnAddNewCompanyCommandExecuted, CanAddNewCompanyCommandExecute);
            EditCompanyCommand = new RelayCommand(OnEditCompanyCommandExecuted, CanEditCompanyCommandExecute);
            RemoveCompanyCommand = new RelayCommand(OnRemoveCompanyCommandExecuted, CanRemoveCompanyCommandExecute);
        }

        private async void InitCollections()
        {
            await GetAllUsers();
            await GetAllCompanies();
        }
    }
}
