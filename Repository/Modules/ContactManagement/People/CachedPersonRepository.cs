using Application.Base;
using Application.Common;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Services;

namespace Repository.Modules.ContactManagement.People
{
    public class CachedPersonRepository : IPersonRepository
    {
        private readonly IPersonRepository _decorated;
        private readonly IDistributedCachProvider _distributedCachProvider;
        private readonly IDatabaseContext _dbContext;

        public CachedPersonRepository(IPersonRepository decorated
            , IDistributedCachProvider distributedCachProvider
            , IDatabaseContext dbContext)
        {
            _decorated = decorated;
            _distributedCachProvider = distributedCachProvider;
            _dbContext = dbContext;
        }

        public void Add(Person person)
        {
            _decorated.Add(person);
        }

        public async Task<Person> GetByIDAsync(int ID)
        {
            Person? person;
            string cachKey = $"person-{ID}";
            person = await _distributedCachProvider.GetAsync<Person>(cachKey);
            if (person is null)
            {
                person = await _decorated.GetByIDAsync(ID);
                await _distributedCachProvider.CachAsync<Person>(cachKey, person);
            }
            else
            {
                _dbContext.People.Attach(person!);
            }
            return person!;
        }

        public async Task<List<Person>> GetListAsync()
        {
            return await _decorated.GetListAsync();
        }
    }
}
