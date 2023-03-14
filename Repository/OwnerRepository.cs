using Ecm.Data;
using Ecm.Interfaces;
using Ecm.Models;

namespace Ecm.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwnerById(int ownerId)
        {
            return _context.Owners.Where(ow => ow.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfPokemon(int pokemonId)
        {
            return _context.PokemonOwners.Where(po => po.PokemonId == pokemonId).Select(po => po.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(po => po.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(ow => ow.Id == ownerId);
        }
    }
}
