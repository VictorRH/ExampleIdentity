using AutoMapper;
using ExampleIdentity.Core.Entities;

namespace ExampleIdentity.Core.Dto
{
    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            CreateMap<StudentModel, StudenModelDto>();
        }
    }
}
