namespace CodeGenerator.Model
{
    public interface IAdminIndexBaseViewModel
    {
        int PageSize { get; set; }
        int PageNum { get; set; }
        long RowCount { get; set; }
        int CurrentRowCount { get; set; }
        string SortFieldName { get; set; }
        bool IsAsc { get; set; }
        string[] FilterFieldsName { get; set; }

        //IEnumerable<IAdminIndexListBaseViewModel> RowList { get; set; }
    }
}