namespace BookShelf.Domain.Rest
{
    public interface IApiRequestExecutor
    {
        Task<TResponse> GetAsync<TResponse>(string reguest);
    }
}
