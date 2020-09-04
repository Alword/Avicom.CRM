using Avicom.CRM.Data;
using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Services
{
    public class UserService
    {
        public User[] Users(Expression<Func<User, bool>> expression)
        {
            using (AvicomContext context = new AvicomContext())
            {
                return context.Users.Where(expression).AsNoTracking().ToArray();
            }
        }
        public async Task<User> AddUserAsync(User user)
        {
            using (AvicomContext context = new AvicomContext())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return user;
            }
        }
        public async Task<int> RemoveUserAsync(Expression<Func<User, bool>> expression)
        {
            using (AvicomContext context = new AvicomContext())
            {
                var users = context.Users.Where(expression).AsNoTracking();
                context.Users.RemoveRange(users);
                return await context.SaveChangesAsync();
            }
        }
        public async Task UpdateUserAsync(User user)
        {
            using (AvicomContext context = new AvicomContext())
            {
                var editUser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
                if (editUser != null)
                {
                    editUser.Login = user.Login;
                    editUser.Name = user.Name;
                    editUser.Password = user.Password;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

