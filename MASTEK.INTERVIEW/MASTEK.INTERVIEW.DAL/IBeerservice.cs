using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public interface IBeerservice<T>
    {
        Beer GetBeer(int Id);
        IEnumerable<Beer> GetBeer(double? gtAlcoholByVolume, double? ltAlcoholByVolume);
        bool PutBeer(Beer Id);
        bool PostBeer(Beer beer);
        bool IsExist(Beer beer);
    }
}
