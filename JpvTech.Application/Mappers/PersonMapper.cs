using AutoMapper;
using JpvTech.Application.DTOs.PersonDTOs;
using JPVTech.Domain.Entities;

namespace JpvTech.Application.Mappers
{
    public class PersonMapper : Profile
    {
        public PersonMapper()
        {
            CreateMap<PersonEntity, PersonRequestDTO>().ReverseMap();
            CreateMap<PersonEntity, PersonResponseDTO>().ReverseMap();
        }
    }
}
