using Avicom.CRM.Client.Model;
using Avicom.CRM.Client.Services;
using Avicom.CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Client.Extentions
{
    public static class CompanyExtentions
    {
        public static IEnumerable<CompanyProperty> AsCompanyProperty(this IEnumerable<Company> companies)
        {
            return companies.Select(e => new CompanyProperty(e));
        }
        public static IEnumerable<CompanyProperty> AsCompanyProperty(this BaseService<Company> companies)
        {
            return companies.All().AsCompanyProperty();
        }
    }
}
