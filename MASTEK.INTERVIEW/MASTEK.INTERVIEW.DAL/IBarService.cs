using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public interface IBarService<T>
    {
        Bar GetBar(int id);
        IEnumerable<Bar> GetBar();
        bool PutBar(Bar id);
        bool PostBar(Bar bar);
        bool IsExist(Bar beer);

        public IEnumerable<Beer> GetAllBeerWithBarid(int id);
        public Boolean UpdateBarbeerModel(BarBeersMapping barBeersMappingm);

    }
}
