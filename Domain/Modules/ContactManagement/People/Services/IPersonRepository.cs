namespace Domain.Modules.ContactManagement.People.Services
{
    public interface IPersonRepository
    {
        void Add(Person person);
        Task<Person> GetByIDAsync(int ID);
        Task<Person> GetByGuidAsync(string guid);
        Task<List<Person>> GetListAsync(int skip = 0, int take = 50);
        Task<long> CountAsync(int skip = 0, int take = 50);
    }
}
