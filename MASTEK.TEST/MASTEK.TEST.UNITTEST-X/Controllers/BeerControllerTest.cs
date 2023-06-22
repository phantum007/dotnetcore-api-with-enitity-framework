using System;
using System.Xml.Linq;
using AutoMapper;
using MASTEK.TEST.API.Controllers;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.Extensions.Logging;
using Moq;

namespace MASTEK.TEST.UNITTEST_X.Controllers
{
	public class BeerControllerTest
	{
        private  readonly BeerController beerController;

        private readonly Mock<IBeerservice> _beerservice = new Mock<IBeerservice>();
		private readonly Mock<ILogger<BeerController>> _logger = new Mock<ILogger<BeerController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public BeerControllerTest()
		{
			beerController = new BeerController(_logger.Object, _beerservice.Object, _mapper.Object);

        }

		[Fact]
		public void GetBeer_for_no_id()
		{
			//arrange
			//	act
			//	asser
		}

        [Fact]
        public void GetBeer_for_correct_id()
        {
            //arrange
            var beerId = 100;
            var beer = new Beer()
            {
                Id = beerId,
                Name = "mock_beer_name",
                PercentageAlcoholByVolume = 4,
                BreweryId = 1
            };

            _beerservice.Setup(x => x.GetBeer(beerId)).Returns(beer);

            _mapper.Setup(x => x.Map<Beer, BeerModel>(beer))
                .Returns(
                    new BeerModel()
                    {
                        Id = beer.Id,
                        Name = beer.Name,
                        PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume,
                        BreweryId = beer.BreweryId
                    }

                );

            var actual_beer = beerController.GetBeer(beerId);

            Assert.Equal(actual_beer.Name, "mock_beer_name");
            //	act
            //	asser
        }
    }
}

