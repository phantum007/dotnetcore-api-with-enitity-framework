using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public interface IBreweryService<T>
    {
        Brewery GetBrewery(int id);
        IEnumerable<Brewery> GetBreweries();
        bool PutBrewery(Brewery id);
        bool PostBrewery(Brewery brewery);
        bool IsExist(Brewery brewery);

        IEnumerable<Beer> GetAllBeerWithBarid(int id);
        Boolean UpdateBrewerybeerModel(BreweryBeersMapping breweryBeersMapping);

    }
}
