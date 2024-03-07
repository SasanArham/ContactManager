using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using AutoMapper;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Queries.GetPeople
{
    public class GetPeopleQuery : IRequest<List<GetPeopleResponse>>
    {
        public int Skip { get; init; } = 0;
        public int Take { get; set; } = PersonCacheHelper.DefaultList.MaxLen;
    }

    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, List<GetPeopleResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCacheProvider _distributedCacheProvider;

        public GetPeopleQueryHandler(IPersonRepository personRepository
            , IMapper mapper
            , IDistributedCacheProvider distributedCacheProvider)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _distributedCacheProvider = distributedCacheProvider;
        }

        public async Task<List<GetPeopleResponse>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            if (request.Skip + request.Take <= PersonCacheHelper.DefaultList.MaxLen)
            {
                var cacheKey = PersonCacheHelper.DefaultList.Key;
                var cachedPeople = await _distributedCacheProvider.GetItemsFromSetAsync<GetPeopleResponse>(cacheKey, request.Skip, request.Take);
                if (cachedPeople.Count < request.Take)
                {
                    var actualCount = await _personRepository.CountAsync(request.Skip, request.Take);
                    if (actualCount > cachedPeople.Count)
                    {
                        await _distributedCacheProvider.InvalidateAllInSetAsync(PersonCacheHelper.DefaultList.Key);
                        var people = await _personRepository.GetListAsync(request.Skip, request.Take);
                        var response = _mapper.Map<List<GetPeopleResponse>>(people);
                        response.ForEach(async item => await _distributedCacheProvider.CacheInSetAsync(PersonCacheHelper.DefaultList.Key, item, item.ID));
                        return response;
                    }
                }
                return cachedPeople;
            }
            var peopleFromDb = await _personRepository.GetListAsync(request.Skip, request.Take);
            var result = _mapper.Map<List<GetPeopleResponse>>(peopleFromDb);
            return result;
        }
    }
}
