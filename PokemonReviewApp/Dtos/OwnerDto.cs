using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dtos
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Gym { get; set; } = string.Empty;
        //public CountryDto Country { get; set; } 
    }
}
