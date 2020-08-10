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

        /// <summary>
        /// The current page number
        /// </summary>
        /// <example>1</example>
        public int CurrentPage { get; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        /// <example>1</example>
        public int PageSize { get; }

        /// <summary>
        /// The total number of data.
        /// </summary>
        /// <example>50</example>
        public int TotalCount { get; set; }

        /// <summary>
        /// The list of items
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
