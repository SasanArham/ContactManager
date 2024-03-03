using Application.Base;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Repository.Modules.ContactManagement.People
{
    public class CachedPersonRepository : IPersonRepository
    {
        private readonly IPersonRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabaseContext _dbContext;

        public CachedPersonRepository(IPersonRepository decorated
            , IDistributedCache distributedCache
            , IDatabaseContext dbContext)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
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
            string? cachedPerson = await _distributedCache.GetStringAsync(cachKey);
            if (string.IsNullOrEmpty(cachedPerson))
            {
                person = await _decorated.GetByIDAsync(ID);
                await _distributedCache.SetStringAsync(cachKey, JsonConvert.SerializeObject(person));
            }
            else
            {
                person = JsonConvert.DeserializeObject<Person>(cachedPerson);
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
