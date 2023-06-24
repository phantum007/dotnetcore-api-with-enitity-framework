using System;
using MASTEK.TEST.API.Controllers;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Moq;

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

        Beer expectedBeer = new Beer()
        {
            Id = 1,
            Name = "corona",
            PercentageAlcoholByVolume = 4
        };

        [Fact]
        public void GetBeer_with_id_good_flow()
        {
            var beerid = 1;
            context.Setup(x => x.Beers.Find(beerid)).Returns(expectedBeer);
            var res = beerservice.GetBeer(beerid);
            Assert.Equivalent(expectedBeer );
        }

        [Fact]
        public void GetBeer_with_id_no_data()
        {
            var beerid = 1;
            var res = expectedBeer;
            res = null;
            context.Setup(x => x.Beers.Find(beerid)).Returns(res);
            Assert.Equivalent(expectedBeer, beerservice.GetBeer(beerid));
        }
    }
}

