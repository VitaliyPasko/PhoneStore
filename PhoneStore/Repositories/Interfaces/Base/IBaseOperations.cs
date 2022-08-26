namespace PhoneStore.Repositories.Interfaces.Base
{
    public interface IBaseOperations<T>
    {
        void Create(T entity);
        void Update(T entity);
        T GetById(int id);
    }
}