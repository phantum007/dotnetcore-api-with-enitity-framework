using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public class BarService<T> : IBarService<T>
    {


        private readonly TestMastekDbContext context;

        public BarService(TestMastekDbContext testMastekDbContext)
        {
            context = testMastekDbContext;
        }


        public Bar GetBar(int Id)
        {
            return context.Bars.Find(Id);
        }


        public IEnumerable<Bar> GetBar()
        {
            return context.Bars;
        }



        public bool PostBar(Bar Bar)
        {
            context.Add(Bar);
            context.SaveChanges();
            return true;
        }

        public bool PutBar(Bar Bar)
        {
            var entity = GetBar(Bar.Id);
            context.Entry(entity).CurrentValues.SetValues(Bar);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Beer> GetAllBeerWithBarid(int barId)
        {
            var beerIds = context.BarBeersMappings.Where(x => x.BarId == barId).Select(x => x.BeerId);
            var beers = context.Beers.Where(x => beerIds.Contains(x.Id)).ToList();
            return beers;
        }

        public bool UpdateBarbeerModel(BarBeersMapping bbm)
        {
            context.Add(bbm);
            context.SaveChanges();
            return true;
        }

        public bool IsExist(Bar bar)
        {
            return context.Bars.Any(b => b.Name == bar.Name && b.Address == bar.Address && b.Id != bar.Id);
        }
    }
}

