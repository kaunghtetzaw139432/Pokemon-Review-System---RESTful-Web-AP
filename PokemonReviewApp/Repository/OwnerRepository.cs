using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners
                .Where(o => o.Id == ownerId)
                 .Include(c => c.Country)
                .FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners
                .Include(c=>c.Country)
                .ToList();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners
                .Where(p => p.PokemonId == pokeId)
                .Include(po => po.Owner)            
                    .ThenInclude(o => o.Country)    
                .Select(po => po.Owner)             
                .ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
          
            return _context.PokemonOwners
                .Where(o => o.OwnerId == ownerId)
                .Select(p => p.Pokemon)
                .ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}