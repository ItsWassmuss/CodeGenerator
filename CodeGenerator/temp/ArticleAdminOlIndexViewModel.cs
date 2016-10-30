//Generate by CodeGenerator 2.0.0
using System.Collections.Generic;
using CodeGenerator.Model;

namespace Panberes.ViewModels.Article
{
    public class ArticleAdminOlIndexViewModel : IAdminIndexBaseViewModel
    {
        public int ArticleId { get; set; }

        public IList<ArticleAdminOlIndexListViewModel> ArticleList { get; set; }

        public bool CanAddNewLanguage { get; set; }

        public int PageSize { get; set; }

        public int PageNum { get; set; }

        public long RowCount { get; set; }

        public int CurrentRowCount { get; set; }

        public string SortFieldName { get; set; }

        public bool IsAsc { get; set; }

        public string[] FilterFieldsName { get; set; }

        public IEnumerable<IAdminIndexListBaseViewModel> RowList { get; set; }
    }
}

