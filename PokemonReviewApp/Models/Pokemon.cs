using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int  Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        [Required]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
        public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
    }
}
