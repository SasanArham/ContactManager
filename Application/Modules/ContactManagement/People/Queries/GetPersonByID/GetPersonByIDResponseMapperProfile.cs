using AutoMapper;
using Domain.Modules.ContactManagement.People;

namespace Application.Modules.ContactManagement.People.Queries.GetPersonByID
{
    public class GetPersonByIDResponseMapperProfile : Profile
    {
        public GetPersonByIDResponseMapperProfile()
        {
            CreateMap<Person, GetPersonByIDResponse>();
        }
    }
}
