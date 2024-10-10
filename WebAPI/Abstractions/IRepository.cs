namespace WebAPI.Repositories
{
    public interface IRepository<T>
    {
        T? Get(int id);
        IEnumerable<T>? GetAll();
        T Create(T value);
        T? Update(T value);
        void Remove(int id);
    }
}
