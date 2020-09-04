using Avicom.CRM.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Services
{
    public class CompanyService : BaseService<Company>
    {
        public override Company[] All(Expression<Func<Company, bool>> expression)
        {
            using (AvicomContext context = new AvicomContext())
            {
                return context.Companies.Where(expression).AsNoTracking().ToArray();
            }
        }
        public override async Task<Company> AddAsync(Company user)
        {
            using (AvicomContext context = new AvicomContext())
            {
                context.Companies.Add(user);
                await context.SaveChangesAsync();
                return user;
            }
        }
        public override async Task<int> RemoveAsync(Expression<Func<Company, bool>> expression)
        {
            using (AvicomContext context = new AvicomContext())
            {
                var users = context.Companies.Where(expression).AsNoTracking();
                context.Companies.RemoveRange(users);
                return await context.SaveChangesAsync();
            }
        }
        public override async Task UpdateAsync(Company entity)
        {
            using (AvicomContext context = new AvicomContext())
            {
                var editEntity = await context.Companies.SingleOrDefaultAsync(u => u.Id == entity.Id);
                if (editEntity != null)
                {
                    editEntity.Name = entity.Name;
                    editEntity.ContractStatus = entity.ContractStatus;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
