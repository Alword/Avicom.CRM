using Avicom.CRM.Data;
using Avicom.CRM.Data.Enums;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace Avicom.CRM.Client.Model
{
    public class CompanyProperty : BindableBase
    {
        public CompanyProperty() { }
        public CompanyProperty(Company company)
        {
            this.Id = company.Id;
            this.Name = company.Name;
            this.ContractStatus = company.ContractStatus;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ContractStatus ContractStatus { get; set; }

        public static implicit operator Company(CompanyProperty d) => new Company()
        {
            Id = d.Id,
            ContractStatus = d.ContractStatus,
            Name = d.Name
        };
    }
}
