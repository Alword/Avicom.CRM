using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Data
{
    public class AvicomContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public AvicomContext() : base("AvicomContext")
        {
                
        }
    }
}
