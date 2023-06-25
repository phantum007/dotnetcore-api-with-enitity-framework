﻿using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.DAL
{
    public class BreweryService<T> : IBreweryService<T>
    {

        private readonly TestMastekDbContext context;


        public BreweryService(TestMastekDbContext testMastekDbContext)
        {
            context = testMastekDbContext;
        }

        public Brewery GetBrewery(int Id)
        {
            return context.Breweries.Find(Id);
        }

        public IEnumerable<Brewery> GetBreweries()
        {
            return context.Breweries;
        }

        public bool PostBrewery(Brewery Brewery)
        {
            context.Add(Brewery);
            context.SaveChanges();
            return true;
        }

        public bool PutBrewery(Brewery Brewery)
        {
            var entity = GetBrewery(Brewery.Id);
            context.Entry(entity).CurrentValues.SetValues(Brewery);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Beer> GetAllBeerWithBarid(int Id)
        {
            var beerIds = context.BreweryBeersMappings.Where(x => x.BreweryId == Id).Select(x => x.BeerId);
            var beers = context.Beers.Where(x => beerIds.Contains(x.Id)).ToList();
            return beers;
        }

        public bool UpdateBrewerybeerModel(BreweryBeersMapping bbm)
        {
            context.Add(bbm);
            context.SaveChanges();
            return true;
        }

        public bool IsExist(Brewery brewery)
        {
            return context.Breweries.Any(b => b.Name == brewery.Name && b.Id != brewery.Id);
        }
    }
}

