using System;
using Google;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using MASTEK.TEST.ENTITY;
using System.Reflection.Metadata;

namespace MASTEK.TEST.DAL
{
    public class BreweryService : IBreweryService
    {

        TestMastekDbContext context = new TestMastekDbContext();


        public BreweryService()
		{
            var context = new TestMastekDbContext();
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

        //public Brewery GetBreweryByIdWithBeer(int Id)
        //{
        //    var brewery = context.Breweries.Find(Id);
        //    brewery.Beers = (new Beerservice()).GetBeerByBreweryId(brewery.Id).ToList();
        //    return brewery;
        //}

        //public IEnumerable<Brewery> GetAllBreweriesWithBeer()
        //{
        //    var breweries = context.Breweries;
        //    foreach (var item in breweries)
        //    {
        //        //item.Beers = context.Beers.Where(x=>x.BreweryId==item.Id).ToList;
        //        item.Beers = (new Beerservice()).GetBeerByBreweryId(item.Id).ToList();

        //    }
        //    return breweries;
        //}

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

        
    }
}

