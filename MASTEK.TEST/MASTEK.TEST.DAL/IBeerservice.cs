using System;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.DAL
{
	public interface IBeerservice
    {
        Beer GetBeer(int Id);
        IEnumerable<Beer> GetBeer(double? gtAlcoholByVolume, double? ltAlcoholByVolume);
        bool PutBeer(Beer Id);
        bool PostBeer(Beer beer);
        bool IsExist(Beer beer);
    }
}
