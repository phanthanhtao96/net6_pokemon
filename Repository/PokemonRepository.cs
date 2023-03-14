using Ecm.Data;
using Ecm.Interfaces;
using Ecm.Models;

namespace Ecm.Repository
{
    public class PokemonRepository: IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemonbyId(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == id);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r=>r.Rating) / review.Count());
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(ow => ow.Id == ownerId).FirstOrDefault();
            var pokemonCategoryEnity = _context.Categories.Where(ca => ca.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };
            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = pokemonCategoryEnity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
