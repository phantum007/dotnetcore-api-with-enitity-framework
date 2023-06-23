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

        BeerModel testBeerModeelObj = new BeerModel()
        {
            Id = 100,
            Name = "mock_beer_name",
            PercentageAlcoholByVolume = 4
        };

        List<Beer> testBeerlistObj = new List<Beer>() {
                new Beer() {
                 Id = 100,
                 Name = "mock_beer_name",
                 PercentageAlcoholByVolume = 4
            },
                new Beer() {
                 Id = 100,
                 Name = "mock_beer_name",
                 PercentageAlcoholByVolume = 4
            }
        };

        #region GetBeerWithId

        [Fact]
        public void GetBeer_with_id_good_flow()
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
        public void GetBeer_with_id_for_wrong_id()
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
        #endregion


        #region GetBeerByVolume

        [Fact]
        public void GetBeer_good_flow()
        {
            //arrange
            double gtAlcoholByVolume = 10;
            double ltAlcoholByVolume = 50;

            _beerservice.Setup(x => x.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj))
                .Returns(
                    new List<BeerModel>()
                    {
                        new BeerModel()
                        {
                        Id = testBeerlistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                        },
                         new BeerModel()
                        {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                        }
                    }
                );

            var actual_beer_responce = beerController.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume);

            var expected_responce = new BeerListResponceModel()
            {
                beersModel = new List<BeerModel>()
                    {
                        new BeerModel()
                        {
                        Id = testBeerlistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                        },
                         new BeerModel()
                        {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                        }
                    },
                errorDetails = null
            };

            Assert.Equivalent(actual_beer_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBeer_for_wrong_gtAlcoholByVolume()
        {
            double gtAlcoholByVolume = 100;
            double ltAlcoholByVolume = 50;


            _beerservice.Setup(x => x.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj))
                    .Returns(
                        new List<BeerModel>()
                        {
                        new BeerModel()
                        {
                        Id = testBeerlistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                        },
                         new BeerModel()
                        {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                        }
                        }
                    );

            var actual_beer_responce = beerController.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume);
            var expected_responce = new BeerListResponceModel()
            {
                beersModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for gtAlcoholByVolume")
            };

            //Assert.Throws<ArgumentException>(() => beerController.GetBeer(_testBeerObj.Id));
            Assert.Equivalent(actual_beer_responce, expected_responce);
        }

        [Fact]
        public void GetBeer_for_wrong_ltAlcoholByVolume()
        {
            double gtAlcoholByVolume = 10;
            double ltAlcoholByVolume = 0;


            _beerservice.Setup(x => x.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj))
                    .Returns(
                        new List<BeerModel>()
                        {
                        new BeerModel()
                        {
                        Id = testBeerlistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                        },
                         new BeerModel()
                        {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                        }
                        }
                    );

            var actual_beer_responce = beerController.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume);
            var expected_responce = new BeerListResponceModel()
            {
                beersModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for ltAlcoholByVolume")
            };

            Assert.Equivalent(actual_beer_responce, expected_responce);
        }


        [Fact]
        public void GetBeer_for_wrong_cobination_of_params()

        {
            double gtAlcoholByVolume = 10;
            double ltAlcoholByVolume = 10;


            _beerservice.Setup(x => x.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj))
                    .Returns(
                        new List<BeerModel>()
                        {
                        new BeerModel()
                        {
                        Id = testBeerlistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                        },
                         new BeerModel()
                        {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                        }
                        }
                    );

            var actual_beer_responce = beerController.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume);
            var expected_responce = new BeerListResponceModel()
            {
                beersModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value : ltAlcoholByVolume can not be same as gtAlcoholByVolume")
            };

            Assert.Equivalent(actual_beer_responce, expected_responce);
        }
        #endregion

        //End GetBeer



        [Fact]
        public void UpdateBeer_good_flow()
        {
            //arrange
            var _testBeerObj = testBeerObj;

            _beerservice.Setup(x => x.PutBeer(_testBeerObj)).Returns(true);

            _mapper.Setup(x => x.Map<BeerModel, Beer>(testBeerModeelObj))
                .Returns(
                    new Beer()
                    {
                        Id = testBeerObj.Id,
                        Name = testBeerObj.Name,
                        PercentageAlcoholByVolume = testBeerObj.PercentageAlcoholByVolume
                    }
                );

            var actual_beer_responce = beerController.UpdateBeer(testBeerModeelObj);

            var expected_responce = new BeerUpdateResponceModel()
            {
                updateStatus=true,
                errorDetails = null
            };

            Assert.Equivalent(actual_beer_responce, expected_responce);
            //	act
            //	asser
        }


    }
}

