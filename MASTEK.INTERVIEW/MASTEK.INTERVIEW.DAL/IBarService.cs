using System;
using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
	public interface IBarService<T>
    {
        Bar GetBar(int Id);
        IEnumerable<Bar> GetBar();
        bool PutBar(Bar Id);
        bool PostBar(Bar Bar);
        bool IsExist(Bar beer);

        public IEnumerable<Beer> GetAllBeerWithBarid(int Id);
        public Boolean UpdateBarbeerModel(BarBeersMapping bbm);

    }
}
