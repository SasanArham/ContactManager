namespace Application.Base
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
