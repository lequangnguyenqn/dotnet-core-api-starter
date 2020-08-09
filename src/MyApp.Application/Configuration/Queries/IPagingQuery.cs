using MediatR;

namespace MyApp.Application.Configuration.Queries
{
    public interface IPagingQuery<out TResponse> : IRequest<TResponse>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
