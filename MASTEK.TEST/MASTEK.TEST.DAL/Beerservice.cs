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
    public class Beerservice: IBeerservice
    {

        TestMastekDbContext context = new TestMastekDbContext();


        public Beerservice()
		{
            var context = new TestMastekDbContext();
        }

        //public TestMastekDbContext GetContext()
        //{
        //    return context;
        //}

        public Beer GetBeer(int Id)
        {
            return context.Beers.Find(Id);
        }

        public IEnumerable<Beer> GetBeer(double? gtAlcoholByVolume, double? ltAlcoholByVolume)
        {
           return context.Beers.Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume &&  b.PercentageAlcoholByVolume < ltAlcoholByVolume);
        }


        //internal IEnumerable<Beer> GetBeerByBreweryId(int breweryId)
        //{
        //    return context.Beers.Where(b => b.BreweryId == breweryId);
        //}

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

