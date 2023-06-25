using System;
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
    public class BarserviceTest
    {
        private  IBarService<TestMastekDbContext> _barservice;
        private readonly Mock<TestMastekDbContext> _context = new Mock<TestMastekDbContext>();
        private readonly DbContextOptions<TestMastekDbContext> options = new DbContextOptions<TestMastekDbContext>();

        public BarserviceTest()
        {
            _barservice = new BarService<TestMastekDbContext>(new TestMastekDbContext(options));
        }

        List<Bar> expectedBars = new List<Bar>()
        {
           new Bar() {
                Id = 1,
            Name = "new_bar_edited"
        },
           new Bar() {
                Id = 2,
            Name = "Garden Gate"
        }
        };

        Bar expectedBar = new Bar()
        {
            Id = 1,
            Name = "new_bar_edited"
        };

        [Fact]
        public void GetBar_with_id_good_flow()
        {
            var barid = 1;
           
            _context.Setup(x => x.Bars.Find(barid)).Returns(expectedBar);
            var res = _barservice.GetBar(barid);
            Assert.Equivalent(expectedBar, res);
        }

        [Fact]
        public void GetBar_with_id_no_data()
        {
            var barid = 0;
            var res = expectedBar;
            res = null;
            _context.Setup(x => x.Bars.Find(barid)).Returns(res);
            Assert.Equivalent(res, _barservice.GetBar(barid));
        }

        [Fact]
        public void GetBar_with_volume_good_flow()
        {
            var gt = 0;
            var lt = 100;
            //_context.Setup(x => x.Bars.Where(x=>x.PercentageAlcoholByVolume>gt && x.PercentageAlcoholByVolume<lt).ToList()).Returns(expectedBars);
            var res = _barservice.GetBar();
            Assert.True(res.Count()>1);
        }

        [Fact]
        public void PutBar_good_flow()
        {
            var res = _barservice.PutBar(expectedBar);
            Assert.True(res);
        }
        [Fact]
        public void PostBar_good_flow()
        {
            var res = _barservice.PutBar(expectedBar);
            Assert.True(res);
        }

    }
}

