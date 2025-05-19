namespace _24HCodeChallenge.API.Services.Interfaces
{
    public interface IPersistence<T, TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
