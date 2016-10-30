using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Model.Views
{
    public class AdminIndexView
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

        public ObservableCollection<AdminIndexViewItem> _items = new ObservableCollection<AdminIndexViewItem>();

    }

    public class AdminIndexViewItem
    {
        public string Key { get; set; }
        public string headTcss { get; set; }
        public string headIcss { get; set; }
        //public string filterFields { get; set; }
        public string headDivcss { get; set; }
        public bool canFilterFields { get; set; }
        public bool canSortFields { get; set; }
        public bool inclue { get; set; }
        public bool exclude { get; set; }

    }
}

