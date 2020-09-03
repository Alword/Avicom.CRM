using Avicom.CRM.Data;
using Avicom.CRM.Data.Enums;
using DevExpress.Mvvm;
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
        public Company SelectedCompany { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<User> Users { get; private set; }
        public MainViewModel()
        {
            //should be injected or disposed at least
            avicomContext = new AvicomContext();
            avicomContext.Companies.Load();
            Companies = avicomContext.Companies.Local;
            Companies.CollectionChanged += Companies_CollectionChanged;
            SaveChanges = new DelegateCommand(() => avicomContext.SaveChanges());
        }
        public ICommand SaveChanges { get; private set; }
        private void Companies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            avicomContext.SaveChanges();
        }
    }
}
