using System;
using System.Xml.Linq;
using MASTEK.TEST.API.Controllers;
using MASTEK.TEST.API;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Moq.EntityFrameworkCore;

namespace MASTEK.TEST.UNITTEST_X.SERVICES
{
    public class BeerserviceTest
    {
        private  IBeerservice<TestMastekDbContext> _beerservice;
        private readonly Mock<TestMastekDbContext> _context = new Mock<TestMastekDbContext>();
        private readonly DbContextOptions<TestMastekDbContext> options = new DbContextOptions<TestMastekDbContext>();

        public BeerserviceTest()
        {
            _beerservice = new Beerservice<TestMastekDbContext>(new TestMastekDbContext(options));
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
            Name = "new_beer_edited",
            PercentageAlcoholByVolume = 5
        };

        [Fact]
        public void GetBeer_with_id_good_flow()
        {
            var beerid = 1;
           
            _context.Setup(x => x.Beers.Find(beerid)).Returns(expectedBeer);
            var res = _beerservice.GetBeer(beerid);
            Assert.Equivalent(expectedBeer, res);
        }

        [Fact]
        public void GetBeer_with_id_no_data()
        {
            var beerid = 0;
            var res = expectedBeer;
            res = null;
            _context.Setup(x => x.Beers.Find(beerid)).Returns(res);
            Assert.Equivalent(res, _beerservice.GetBeer(beerid));
        }

        [Fact]
        public void GetBeer_with_volume_good_flow()
        {
            var gt = 0;
            var lt = 100;
            //_context.Setup(x => x.Beers.Where(x=>x.PercentageAlcoholByVolume>gt && x.PercentageAlcoholByVolume<lt).ToList()).Returns(expectedBeers);
            var res = _beerservice.GetBeer(gt,lt);
            Assert.True(res.Count()>1);
        }

        [Fact]
        public void GetBeer_with_volume_wrong_gt()
        {
            var gt = 100;
            var lt = 110;
            //_context.Setup(x => x.Beers.Where(x=>x.PercentageAlcoholByVolume>gt && x.PercentageAlcoholByVolume<lt).ToList()).Returns(expectedBeers);
            var res = _beerservice.GetBeer(gt, lt);
            Assert.True(res.Count() == 0);
        }
        [Fact]
        public void GetBeer_with_volume_wrong_lt()
        {
            var gt = -1;
            var lt = 0;
            //_context.Setup(x => x.Beers.Where(x=>x.PercentageAlcoholByVolume>gt && x.PercentageAlcoholByVolume<lt).ToList()).Returns(expectedBeers);
            var res = _beerservice.GetBeer(gt, lt);
            Assert.True(res.Count() == 0);
        }
        [Fact]
        public void GetBeer_with_volume_wrong_lt_gt()
        {
            var gt = 10;
            var lt = 10;
            //_context.Setup(x => x.Beers.Where(x=>x.PercentageAlcoholByVolume>gt && x.PercentageAlcoholByVolume<lt).ToList()).Returns(expectedBeers);
            var res = _beerservice.GetBeer(gt, lt);
            Assert.True(res.Count() == 0);
        }

        [Fact]
        public void PutBeer_good_flow()
        {
            var res = _beerservice.PutBeer(expectedBeer);
            Assert.True(res);
        }
        [Fact]
        public void PostBeer_good_flow()
        {
            var res = _beerservice.PutBeer(expectedBeer);
            Assert.True(res);
        }

    }
}

