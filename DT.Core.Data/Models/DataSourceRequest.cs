namespace DT.Core.Data.Models
{
    public class DataSourceRequest
    {
        public int FiltersCount { get; set; }
        public int GroupsCount { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int RecordStartIndex { get; set; }
        public int RecordEndIndex { get; set; }
    }
}
