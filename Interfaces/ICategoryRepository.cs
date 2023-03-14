using Ecm.Models;

namespace Ecm.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
        bool CategoryExists(int categoryId);
    }
}
