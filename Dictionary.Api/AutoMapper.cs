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

            CreateMap<GermanRussianDictionary, CreateGermanRussianDictionaryDto>();
            CreateMap<CreateGermanRussianDictionaryDto, GermanRussianDictionary>();

            CreateMap<GermanRussianDictionary, ResultGermanRussianDictionaryDto>();
            CreateMap<ResultGermanRussianDictionaryDto, GermanRussianDictionary>();
        }
    }
}
