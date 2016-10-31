using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodeGenerator.Model
{
    public class IAdminIndexList
    {
        public bool IsKey { get; set; }
        public int Order { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public ComboBoxItem UseType { get; set; }
    }

    public enum UseType
    {
        justMain = 1,
        justOtherLanguage= 2,
        Multiple = 3
    }
}
