using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public class BeerService<T>: IBeerservice<T>
    {

        private readonly TestMastekDbContext context ;


        public BeerService(TestMastekDbContext testMastekDbContext)
        {
             context = testMastekDbContext;
        }

        public Beer GetBeer(int Id)
        {
            return context.Beers.Find(Id);
        }

      
        public IEnumerable<Beer> GetBeer(double? gtAlcoholByVolume, double? ltAlcoholByVolume)
        {
           return context.Beers.Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume &&  b.PercentageAlcoholByVolume < ltAlcoholByVolume);
        }

        public bool PostBeer(Beer beer)
        {
            context.Add(beer);
            context.SaveChanges();
            return true;
        }

        public bool PutBeer(Beer beer)
        {
            var entity = GetBeer(beer.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Object not exist");
            }
            context.Entry(entity).CurrentValues.SetValues(beer);
            context.SaveChanges();
            return true;
        }

        public bool IsExist(Beer beer)
        {
           return context.Beers.Any(b => b.Name == beer.Name && b.PercentageAlcoholByVolume==beer.PercentageAlcoholByVolume && b.Id != beer.Id);
        }
    }
}

