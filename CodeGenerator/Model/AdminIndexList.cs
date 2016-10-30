using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Model
{
    public class AdminIndexList
    {
        public string Field { get; set; }

        public bool IsKey { get; set; }

        public bool IsFilterText { get; set; }
        public int FilterText { get; set; }

        public bool IsCleanHtml { get; set; }

        public bool IsBoolean { get; set; }
        public string YesMessage { get; set; }
        public string NoMessage { get; set; }


    }
}
