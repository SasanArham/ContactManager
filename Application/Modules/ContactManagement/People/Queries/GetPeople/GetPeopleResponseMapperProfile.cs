using AutoMapper;
using Domain.Modules.ContactManagement.People;

namespace Application.Modules.ContactManagement.People.Queries.GetPeople
{
    public class GetPeopleResponseMapperProfile : Profile
    {
        public GetPeopleResponseMapperProfile()
        {
            CreateMap<Person, GetPeopleResponse>();
        }
    }
}
