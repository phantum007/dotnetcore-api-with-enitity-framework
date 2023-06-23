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
    public class BarService : IBarService
    {

        TestMastekDbContext context = new TestMastekDbContext();


        public BarService()
		{
            var context = new TestMastekDbContext();
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
           var beerIds= context.BarBeersMappings.Where(x => x.BarId == barId).Select(x=>x.BeerId);
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

