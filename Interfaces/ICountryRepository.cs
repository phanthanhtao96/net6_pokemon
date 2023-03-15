using Ecm.Models;

namespace Ecm.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountryById(int countryId);
        Country GetCountryByOwner(int ownerId);  
        ICollection<Owner> GetOwnersByCountry(int countryId);
        bool CountryExists(int countryId);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool Save();

    }
}
