//using System;
//using System.Xml.Linq;
//using MASTEK.TEST.API.Controllers;
//using MASTEK.TEST.API;
//using MASTEK.TEST.DAL;
//using MASTEK.TEST.ENTITY;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Moq;
//using Moq.EntityFrameworkCore;

//namespace MASTEK.TEST.UNITTEST_X.SERVICES
//{
//    public class BeerserviceTest
//    {
//        private readonly IBeerservice<TestMastekDbContext> _beerservice;
//        private readonly Mock<TestMastekDbContext> _context;

//        public BeerserviceTest( Beerservice<TestMastekDbContext> beerservice)
//        {
//            _beerservice = beerservice;
//        }

//        List<Beer> expectedBeers = new List<Beer>()
//        {
//           new Beer() {
//                Id = 1,
//            Name = "corona",    
//            PercentageAlcoholByVolume = 4
//        },
//           new Beer() {
//                Id = 2,
//            Name = "corona extra",
//            PercentageAlcoholByVolume = 4
//        }
//        };
        

//        Beer expectedBeer = new Beer()
//        {
//            Id = 1,
//            Name = "corona",
//            PercentageAlcoholByVolume = 4
//        };


//        [Fact]
//        public void GetBeer_with_id_good_flow()
//        {
//            var beerid = 1;

//            //context.Setup<DbSet<Beer>>(x => x.Beers).ReturnsDbSet(expectedBeers);
//            _context.Setup(x => x.Beers.Find(beerid)).Returns(expectedBeer);
//            var res = _beerservice.GetBeer(beerid);
//            Assert.Equivalent(expectedBeer, res);
//        }

//        [Fact]
//        public void GetBeer_with_id_no_data()
//        {
//            var beerid = 1;
//            var res = expectedBeer;
//            res = null;
//            _context.Setup(x => x.Beers.Find(beerid)).Returns(res);
//            Assert.Equivalent(expectedBeer, _beerservice.GetBeer(beerid));
//        }
//    }
//}

