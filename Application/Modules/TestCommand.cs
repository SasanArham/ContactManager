using Application.Common;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules
{
    public class TestCommand : IRequest<List<Person>>
    {
    }

    public class TestCommandHandler : IRequestHandler<TestCommand, List<Person>>
    {
        private readonly IDistributedCacheProvider _distributedCacheProvider;
        private readonly IPersonRepository _personRepository;

        public TestCommandHandler(IDistributedCacheProvider distributedCacheProvider
            , IPersonRepository personRepository)
        {
            _distributedCacheProvider = distributedCacheProvider;
            _personRepository = personRepository;
        }

        public async Task<List<Person>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            var people = await _distributedCacheProvider.GetItemsFromSetAsync<Person>("people-default-list");
            if (!people.Any())
            {
                people = await _personRepository.GetListAsync();
            }
            return people;
        }
    }
}
