using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodeGenerator.Model
{
    public class AdminDetails
    {
        //public bool IsKey { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public bool IsImage { get; set; }
        public bool IsCheckBox { get; set; }
        public string TrueCheckBox { get; set; }
        public string FalseCheckBox { get; set; }
        public bool IsHtml { get; set; }
        public ComboBoxItem UseType { get; set; }
    }
}
