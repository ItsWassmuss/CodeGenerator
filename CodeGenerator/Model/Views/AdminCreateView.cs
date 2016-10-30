using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Model.Views
{
    public class AdminCreateView
    {
        //public string SectionTitle { get; set; }
        //public string SectionPageHeader { get; set; }
        //public string SectionRenderStyles { get; set; }
        //public string SectionRenderScripts { get; set; }

        //public bool isAsc { get; set; }
        public bool isHaveHeadButton { get; set; }
        //public bool isHaveLanguage { get; set; }

        public string headButtonCssClass { get; set; }
        //public string deleteButtonId { get; set; }
        //public string deleteUrl { get; set; }
        //public string editUrl { get; set; }
        //public string detailsUrl { get; set; }

        public ObservableCollection<AdminCreateViewItem> _items = new ObservableCollection<AdminCreateViewItem>();

    }

    public class AdminCreateViewItem
    {
        public string Key { get; set; }
        public bool IsTextArea { get; set; }
        public bool IsCkEditor { get; set; }
        public bool IsTextBox { get; set; }
        public bool IsImage { get; set; }
        public bool IsLandscape { get; set; }
        public bool IsDropDownList { get; set; }
        public bool IsNestedDropDownList { get; set; }
        public bool IsKey { get; set; }
        public bool IsLabel { get; set; }
        public bool IsCheckBox { get; set; }


    }
}
