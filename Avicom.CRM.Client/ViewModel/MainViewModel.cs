using Avicom.CRM.Client.Extentions;
using Avicom.CRM.Client.Model;
using Avicom.CRM.Client.Services;
using Avicom.CRM.Data;
using Avicom.CRM.Data.Enums;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Avicom.CRM.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BaseService<User> users;
        private readonly BaseService<Company> companies;
        public UserProperty SelectedUser { get; set; }
        public CompanyProperty SelectedCompany { get; set; }
        public ObservableCollection<CompanyProperty> Companies { get; set; }
        public ObservableCollection<UserProperty> Users { get; private set; }
        public ICommand CompanyEditEnd { get; private set; }
        public ICommand CompanySelectionChanged { get; private set; }
        public ICommand UserEditEnd { get; private set; }
        public MainViewModel()
        {
            // should be injected?
            users = new UserService();
            companies = new CompanyService();

            Companies = new ObservableCollection<CompanyProperty>(companies.AsCompanyProperty());
            Companies.CollectionChanged += CollectionChanged;

            CompanySelectionChanged = new DelegateCommand(() =>
            {
                if (SelectedCompany != null)
                {
                    if (Users != null)
                        Users.CollectionChanged -= CollectionChanged;

                    var userProperties = users.AsUserProperty(e => e.CompanyId == SelectedCompany.Id);
                    Users = new ObservableCollection<UserProperty>(userProperties);

                    Users.CollectionChanged += CollectionChanged;
                }
            });

            CompanyEditEnd = new DelegateCommand(async () =>
            {
                if (SelectedCompany.Id == 0)
                {
                    var company = await companies.AddAsync(SelectedCompany);
                    SelectedCompany.Id = company.Id;
                }
                else
                {
                    await companies.UpdateAsync(SelectedCompany);
                }
            });

            UserEditEnd = new DelegateCommand(async () =>
            {
                if (SelectedUser.Id == 0)
                {
                    User user = SelectedUser;
                    user.CompanyId = SelectedCompany.Id;
                    await users.AddAsync(user);
                    SelectedUser.Id = user.Id;
                }
                else
                {
                    await users.UpdateAsync(SelectedUser);
                }
            });
        }
        private async void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is CompanyProperty company)
                    {
                        await companies.RemoveAsync(c => c.Id == company.Id);
                    }
                    else if (item is UserProperty user)
                    {
                        await users.RemoveAsync(c => c.Id == user.Id);
                    }
                }
            }
        }
        ~MainViewModel()
        {
            if (Users != null)
                Users.CollectionChanged -= CollectionChanged;
        }
    }
}