using System;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.DAL
{
	public interface IBreweryService<T>
    {
        Brewery GetBrewery(int Id);
        IEnumerable<Brewery> GetBreweries();
        bool PutBrewery(Brewery Id);
        bool PostBrewery(Brewery Brewery);
        bool IsExist(Brewery brewery);

        IEnumerable<Beer> GetAllBeerWithBarid(int Id);
        Boolean UpdateBrewerybeerModel(BreweryBeersMapping bbm);

    }
}
