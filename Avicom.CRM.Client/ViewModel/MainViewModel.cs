using Avicom.CRM.Client.Services;
using Avicom.CRM.Data;
using Avicom.CRM.Data.Enums;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avicom.CRM.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BaseService<User> users;
        private readonly BaseService<Company> companies;

        public User SelectedUser { get; set; }
        public Company SelectedCompany { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<User> Users { get; private set; }
        public MainViewModel()
        {
            // should be injected?
            users = new UserService();
            companies = new CompanyService();
            Companies = new ObservableCollection<Company>(companies.All());

            CompanySelectionChanged = new DelegateCommand(() =>
            {
                Users = new ObservableCollection<User>(users.All(e => e.CompanyId == SelectedCompany.Id));
            });
            CompanyEditEnd = new DelegateCommand(async () =>
            {
                await companies.UpdateAsync(SelectedCompany);
            });
            UserEditEnd = new DelegateCommand(async () =>
            {
                await users.UpdateAsync(SelectedUser);
            });
        }
        public ICommand CompanyEditEnd { get; private set; }
        public ICommand UserEditEnd { get; private set; }
        public ICommand CompanySelectionChanged { get; private set; }
    }
}
