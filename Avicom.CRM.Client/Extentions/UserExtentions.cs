using Avicom.CRM.Client.Model;
using Avicom.CRM.Client.Services;
using Avicom.CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Extentions
{
    public static class UserExtentions
    {
        public static IEnumerable<UserProperty> AsUserProperty(this IEnumerable<User> users)
        {
            return users.Select(e => new UserProperty(e));
        }
        public static IEnumerable<UserProperty> AsUserProperty(this BaseService<User> users)
        {
            return users.All().AsUserProperty();
        }
        public static IEnumerable<UserProperty> AsUserProperty(this BaseService<User> users, Expression<Func<User, bool>> expression)
        {
            return users.All(expression).AsUserProperty();
        }
    }
}
