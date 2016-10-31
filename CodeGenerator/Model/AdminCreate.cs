using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodeGenerator.Model
{
    class AdminCreate
    {
        public string Field { get; set; }

        public string Display { get; set; }

        public bool IsRequired { get; set; }
        public string RequiredMessage { get; set; }

        //public bool IsMaxLength { get; set; }
        //public string MaxLengthMessage { get; set; }
        //public int MaxLength { get; set; }

        //public bool IsMinLength { get; set; }
        //public string MinLengthMessage { get; set; }
        //public int MinLength { get; set; }

        public bool IsStringLength { get; set; }
        public string StringLengthMessage { get; set; }
        public int MaxStringLength { get; set; }
        public int MinStringLength { get; set; }

        public bool IsAllowHtml { get; set; }

        public bool IsScaffoldColumnFalse { get; set; }

        public bool IsPhone { get; set; }
        public string PhoneMessage { get; set; }

        public bool IsRange { get; set; }
        public string RangeMessage { get; set; }
        public int MaxRange { get; set; }
        public int MinRange { get; set; }

        public bool IsRegularExpression { get; set; }
        public string RegularExpressionMessage { get; set; }
        public string Pattern { get; set; }

        public bool IsImage { get; set; }
        public bool IsLanguage { get; set; }
        public bool IsUseInOl { get; set; }

        //public ComboBoxItem UseType { get; set; }

    }
}
