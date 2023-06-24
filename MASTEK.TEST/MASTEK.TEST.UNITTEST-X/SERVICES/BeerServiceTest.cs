using System;
using System.Xml.Linq;
using MASTEK.TEST.API.Controllers;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace MASTEK.TEST.UNITTEST_X.SERVICES
{
    public class BeerserviceTest
    {
        private readonly Beerservice beerservice;
        private readonly Mock<TestMastekDbContext> context;

        public BeerserviceTest()
        {
            beerservice = new Beerservice();
            context = new Mock<TestMastekDbContext>();
        }

        List<Beer> expectedBeers = new List<Beer>()
        {
           new Beer() {
                Id = 1,
            Name = "corona",
            PercentageAlcoholByVolume = 4
        },
           new Beer() {
                Id = 2,
            Name = "corona extra",
            PercentageAlcoholByVolume = 4
        }
        };
        

        Beer expectedBeer = new Beer()
        {
            Id = 1,
            Name = "corona",
            PercentageAlcoholByVolume = 4
        };

        //[Fact]
        //public void GetBeer_with_id_good_flow()
        //{
        //    var beerid = 1;

        //    context.Setup<DbSet<Beer>>(x => x.Beers).ReturnsDbSet(expectedBeers);

        //    //context.Setup(x => x.Beers.Find(beerid)).Returns(expectedBeer);
        //    var res = beerservice.GetBeer(beerid);
        //    Assert.Equivalent(expectedBeer, res);
        //}

        //[Fact]
        //public void GetBeer_with_id_no_data()
        //{
        //    var beerid = 1;
        //    var res = expectedBeer;
        //    res = null;
        //    context.Setup(x => x.Beers.Find(beerid)).Returns(res);
        //    Assert.Equivalent(expectedBeer, beerservice.GetBeer(beerid));
        //}
    }
}

