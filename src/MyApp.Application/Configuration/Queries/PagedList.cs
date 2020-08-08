using System.Collections.Generic;

namespace MyApp.Application.Configuration.Queries
{
    public class PagedList<T>
    {
        public PageInfo Paging { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
