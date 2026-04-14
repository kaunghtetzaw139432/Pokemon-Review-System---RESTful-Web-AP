using AutoMapper;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Reviewer,ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
    
}
