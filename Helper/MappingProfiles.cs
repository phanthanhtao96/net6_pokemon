using AutoMapper;
using Ecm.Dto;
using Ecm.Models;

namespace Ecm.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewsDto>();
            CreateMap<Reviewer, ReviewerDto>();  
        }
    }
}
