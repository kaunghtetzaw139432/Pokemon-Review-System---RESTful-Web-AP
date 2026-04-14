using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons
                .Include(p => p.Reviews)
                .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(o => o.Country)
                .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons
                .Include(p => p.Reviews)
                .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(o => o.Country)
                .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
                .Where(p => p.Name == name)
                .FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (!reviews.Any())
                return 0;

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons
                .Include(p => p.Reviews)
                .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(o => o.Country)
                .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
         
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonOwner);

      
            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
            _context.Add(pokemonCategory);


            _context.Add(pokemon);

            return Save();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {  
            _context.Update(pokemon);
            return Save();
        }
        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}