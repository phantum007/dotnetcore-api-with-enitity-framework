using System;
using System.Xml.Linq;
using AutoMapper;
using MASTEK.TEST.API.Controllers;
using MASTEK.TEST.API.ExceptionClaases;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;
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

        //Start GetBeer

        Beer testBeerObj = new Beer()
        {
            Id = 100,
            Name = "mock_beer_name",
            PercentageAlcoholByVolume = 4
        };

       
        [Fact]
        public void GetBeer_good_flow()
        {
            //arrange
            var beerId = 100;
          
            _beerservice.Setup(x => x.GetBeer(beerId)).Returns(testBeerObj);

            _mapper.Setup(x => x.Map<Beer, BeerModel>(testBeerObj))
                .Returns(
                    new BeerModel()
                    {
                        Id = testBeerObj.Id,
                        Name = testBeerObj.Name,
                        PercentageAlcoholByVolume = testBeerObj.PercentageAlcoholByVolume
                    }
                );

            var actual_beer_responce = beerController.GetBeer(beerId);

            var expected_responce = new BeerResponceModel()
            {
                beerModel = new BeerModel()
                {
                    Id = testBeerObj.Id,
                    Name = testBeerObj.Name,
                    PercentageAlcoholByVolume = testBeerObj.PercentageAlcoholByVolume
                },
                errorDetails = null
        };

        Assert.Equivalent(actual_beer_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBeer_for_no_id()
        {
            var _testBeerObj = testBeerObj;
            _testBeerObj.Id = 0;

            _beerservice.Setup(x => x.GetBeer(_testBeerObj.Id)).Returns(_testBeerObj);

            _mapper.Setup(x => x.Map<Beer, BeerModel>(_testBeerObj))
                .Returns(
                    new BeerModel()
                    {
                        Id = testBeerObj.Id,
                        Name = testBeerObj.Name,
                        PercentageAlcoholByVolume = testBeerObj.PercentageAlcoholByVolume
                    }
                );

            var actual_beer_responce = beerController.GetBeer(_testBeerObj.Id);
            var expected_responce = new BeerResponceModel()
            {
                beerModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for Id")
            };

            //Assert.Throws<ArgumentException>(() => beerController.GetBeer(_testBeerObj.Id));
            Assert.Equivalent(actual_beer_responce, expected_responce);
        }

    }
}

