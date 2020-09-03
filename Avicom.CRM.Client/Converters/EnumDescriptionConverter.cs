using Avicom.CRM.Data.Enums;
using Avicom.CRM.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Avicom.CRM.Client.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        private static Map<string, ContractStatus> convertMap;

        static EnumDescriptionConverter()
        {
            convertMap = new Map<string, ContractStatus>();
            convertMap.Add("Ещё не заключен", ContractStatus.NotYetConcluded);
            convertMap.Add("Заключен", ContractStatus.Concluded);
            convertMap.Add("Расторгнут", ContractStatus.Cancelled);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return GetDescription((ContractStatus)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public static string GetDescription(ContractStatus status)
        {
            return convertMap[status];
        }
    }
}
