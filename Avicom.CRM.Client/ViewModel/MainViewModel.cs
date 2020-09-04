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

        public object CompanyEvent { get; set; }
        public User SelectedUser { get; set; }
        public CompanyProperty SelectedCompany { get; set; }

        public ObservableCollection<CompanyProperty> Companies { get; set; }
        public ObservableCollection<User> Users { get; private set; }
        public MainViewModel()
        {
            // should be injected?
            users = new UserService();
            companies = new CompanyService();
            Companies = new ObservableCollection<CompanyProperty>(companies.All().Select(e => new CompanyProperty(e)));
            Companies.CollectionChanged += Companies_CollectionChanged;

            CompanySelectionChanged = new DelegateCommand(() =>
            {
                if (SelectedCompany != null)
                {
                    Users = new ObservableCollection<User>(users.All(e => e.CompanyId == SelectedCompany.Id));
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
                    var company = await companies.AddAsync(SelectedCompany);
                    SelectedCompany.Id = company.Id;
                }
                else
                {
                    await users.UpdateAsync(SelectedUser);
                }
            });
        }
        public ICommand CompanyEditEnd { get; private set; }
        public ICommand CompanySelectionChanged { get; private set; }
        public ICommand UserEditEnd { get; private set; }
        private async void Companies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is Company company)
                    {
                        await companies.RemoveAsync(c => c.Id == company.Id);
                    }
                }
            }
        }
    }
}
