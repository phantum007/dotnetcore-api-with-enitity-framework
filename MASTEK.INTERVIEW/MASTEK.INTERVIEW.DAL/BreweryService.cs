using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public class BreweryService<T> : IBreweryService<T>
    {

        private readonly TestMastekDbContext context;


        public BreweryService(TestMastekDbContext testMastekDbContext)
        {
            context = testMastekDbContext;
        }

        public Brewery GetBrewery(int id)
        {
            return context.Breweries.Find(id);
        }

        public IEnumerable<Brewery> GetBreweries()
        {
            return context.Breweries;
        }

        public bool PostBrewery(Brewery brewery)
        {
            context.Add(brewery);
            context.SaveChanges();
            return true;
        }

        public bool PutBrewery(Brewery brewery)
        {
            var entity = GetBrewery(brewery.Id);
            context.Entry(entity).CurrentValues.SetValues(brewery);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Beer> GetAllBeerWithBarid(int id)
        {
            var beerIds = context.BreweryBeersMappings.Where(x => x.BreweryId == id).Select(x => x.BeerId);
            var beers = context.Beers.Where(x => beerIds.Contains(x.Id)).ToList();
            return beers;
        }

        public bool UpdateBrewerybeerModel(BreweryBeersMapping breweryBeersMapping)
        {
            context.Add(breweryBeersMapping);
            context.SaveChanges();
            return true;
        }

        public bool IsExist(Brewery brewery)
        {
            return context.Breweries.Any(b => b.Name == brewery.Name && b.Id != brewery.Id);
        }
    }
}

