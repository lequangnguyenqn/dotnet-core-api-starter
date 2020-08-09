using System.Collections.Generic;

namespace MyApp.Application.Configuration.Queries
{
    public class PagedList<T>
    {
        public PagedList(IPagingQuery<PagedList<T>> pagingQuery)
        {
            CurrentPage = pagingQuery.CurrentPage;
            PageSize = pagingQuery.PageSize;
        }
        public PagedList(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
