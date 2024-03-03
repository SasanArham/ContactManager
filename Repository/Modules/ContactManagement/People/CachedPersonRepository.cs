using Application.Base;
using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Repository.Modules.ContactManagement.People
{
    public class CachedPersonRepository : IPersonRepository
    {
        private readonly IPersonRepository _decorated;
        private readonly IDistributedCacheProvider _distributedCachProvider;
        private readonly IDatabaseContext _dbContext;

        public CachedPersonRepository(IPersonRepository decorated
            , IDistributedCacheProvider distributedCachProvider
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
            string cachKey = PersonCacheKey.Person(ID);
            person = await _distributedCachProvider.GetAsync<Person>(cachKey);
            if (person is null)
            {
                person = await _decorated.GetByIDAsync(ID);
                await _distributedCachProvider.CacheAsync<Person>(cachKey, person);
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
