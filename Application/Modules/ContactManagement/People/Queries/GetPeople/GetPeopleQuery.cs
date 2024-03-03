using AutoMapper;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Queries.GetPeople
{
    public class GetPeopleQuery : IRequest<List<GetPeopleResponse>>
    {

    }

    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, List<GetPeopleResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPeopleQueryHandler(IPersonRepository personRepository
            , IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPeopleResponse>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetListAsync();
            var response = _mapper.Map<List<GetPeopleResponse>>(people);
            return response;
        }
    }
}
