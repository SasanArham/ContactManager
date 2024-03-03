namespace Domain.Modules.ContactManagement.People.Services
{
    public interface IPersonRepository
    {
        void Add(Person person);
        Task<Person> GetByIDAsync(int ID);
        Task<List<Person>> GetListAsync();
    }
}
