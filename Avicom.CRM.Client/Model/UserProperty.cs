using Avicom.CRM.Data;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Model
{
    public class UserProperty : BindableBase
    {
        public UserProperty() { }
        public UserProperty(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.Login;
            Password = user.Password;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public static implicit operator User(UserProperty d) => new User()
        {
            Id = d.Id,
            Name = d.Name,
            Login = d.Login,
            Password = d.Password
        };
    }
}
