using AutoMapper;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Queries.GetPersonByID
{
    public class GetPersonByIDQuery : IRequest<GetPersonByIDResponse>
    {
        public int ID { get; init; }
    }

    public class GetPersonByIDQueryHandler : IRequestHandler<GetPersonByIDQuery, GetPersonByIDResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPersonByIDQueryHandler(IPersonRepository personRepository
            , IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<GetPersonByIDResponse> Handle(GetPersonByIDQuery request, CancellationToken cancellationToken)
        {
            var persom = await _personRepository.GetByIDAsync(request.ID);
            var personDto = _mapper.Map<GetPersonByIDResponse>(persom);
            return personDto;
        }
    }

}
