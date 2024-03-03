using Application.Base;
using Application.Base.Exceptions;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Services;
using Microsoft.EntityFrameworkCore;


namespace Repository.Modules.ContactManagement.People
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDatabaseContext _dbContext;

        public PersonRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Person person)
        {
            _dbContext.People.Add(person);
        }

        public async Task<Person> GetByIDAsync(int ID)
        {
            try
            {
                return await _dbContext.People.FirstAsync(c => c.ID == ID && !c.Deleted);
            }
            catch (Exception ex)
            {
                throw new CrudException("Peron not found", ex);
            }
        }

        public async Task<List<Person>> GetListAsync()
        {
            try
            {
                return await _dbContext.People.Where(c => !c.Deleted).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CrudException("Failed to loag people", ex);
            }
        }
    }
}
