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


        public Bar GetBar(int id)
        {
            return context.Bars.Find(id);
        }


        public IEnumerable<Bar> GetBar()
        {
            return context.Bars;
        }



        public bool PostBar(Bar bar)
        {
            context.Add(bar);
            context.SaveChanges();
            return true;
        }

        public bool PutBar(Bar bar)
        {
            var entity = GetBar(bar.Id);
            context.Entry(entity).CurrentValues.SetValues(bar);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Beer> GetAllBeerWithBarid(int barId)
        {
            var beerIds = context.BarBeersMappings.Where(x => x.BarId == barId).Select(x => x.BeerId);
            var beers = context.Beers.Where(x => beerIds.Contains(x.Id)).ToList();
            return beers;
        }

        public bool UpdateBarbeerModel(BarBeersMapping barBeersMapping)
        {
            context.Add(barBeersMapping);
            context.SaveChanges();
            return true;
        }

        public bool IsExist(Bar bar)
        {
            return context.Bars.Any(b => b.Name == bar.Name && b.Address == bar.Address && b.Id != bar.Id);
        }
    }
}

