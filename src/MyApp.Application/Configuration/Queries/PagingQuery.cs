
namespace MyApp.Application.Configuration.Queries
{
    public abstract class PagingQuery<TResponse> : IPagingQuery<TResponse>
    {
        const int maxPageSize = 100;
        const int minCurrentPage = 1;

        private int _currentPage = 1;
        public int CurrentPage { 
            get { return _currentPage; } 
            set { _currentPage = (value < minCurrentPage) ? minCurrentPage : value; }
        }

        private int _pageSize = 10;
        public int PageSize 
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
