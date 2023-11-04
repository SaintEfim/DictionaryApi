using AutoMapper;
using Dictionary.Domain.Entity;
using Dictionary.Api.Models;

namespace StoresAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GermanRussianDictionary, GermanRussianDictionaryDto>();
            CreateMap<GermanRussianDictionaryDto, GermanRussianDictionary>();

            CreateMap<GermanRussianDictionary, CreateGermanRussianDictionary>();
            CreateMap<CreateGermanRussianDictionary, GermanRussianDictionary>();

            CreateMap<GermanRussianDictionary, ResultGermanRussianDictionary>();
            CreateMap<ResultGermanRussianDictionary, GermanRussianDictionary>();
        }
    }
}
