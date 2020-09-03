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
        private AvicomContext avicomContext;

        private Company selectedCompany;
        public Company SelectedCompany
        {
            get => selectedCompany; set
            {
                selectedCompany = value;

                if (Users != null)
                    Users.CollectionChanged -= Companies_CollectionChanged;
                Users = avicomContext.Users.Where(d => d.CompanyId == selectedCompany.Id).ToObservableCollection();
                Users.CollectionChanged += Users_CollectionChanged; ;
            }
        }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<User> Users { get; private set; }
        public MainViewModel()
        {
            //should be injected or disposed at least
            avicomContext = new AvicomContext();

            avicomContext.Companies.Load();
            Companies = avicomContext.Companies.Local;
            Companies.CollectionChanged += Companies_CollectionChanged;
            SaveChanges = new DelegateCommand(
                () =>
                avicomContext.SaveChanges()
                );
        }
        public ICommand SaveChanges { get; private set; }
        private void Companies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            avicomContext.SaveChanges();
        }
        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is User user)
                    {
                        user.CompanyId = selectedCompany.Id;
                        if (string.IsNullOrEmpty(user.Name))
                            user.Name = "Без имени";
                        if (string.IsNullOrEmpty(user.Password))
                            user.Login = "";
                        if (string.IsNullOrEmpty(user.Login))
                            user.Password = "";
                        avicomContext.Users.Add(user);
                        avicomContext.SaveChanges();
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is User user)
                    {
                        var dbUser = avicomContext.Users.Find(user.Id);
                        avicomContext.Users.Remove(dbUser);
                        avicomContext.SaveChanges();
                    }
                }
            }
        }
    }
}
