using Avicom.CRM.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
