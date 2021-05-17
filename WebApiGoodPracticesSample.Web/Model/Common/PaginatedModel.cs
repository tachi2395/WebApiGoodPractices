using System.Collections.Generic;

namespace WebApiGoodPracticesSample.Web.Model.Common
{
    public class PaginatedModel<TModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TModel> Results { get; set; }
    }
}
