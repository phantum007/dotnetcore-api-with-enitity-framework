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
	public class BreweryControllerTest
    {
        private  readonly BreweryController breweryController;

        private readonly Mock<IBreweryService> _breweryservice = new Mock<IBreweryService>();
		private readonly Mock<ILogger<BreweryController>> _logger = new Mock<ILogger<BreweryController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public BreweryControllerTest()
        {
            breweryController = new BreweryController(_logger.Object, _breweryservice.Object, _mapper.Object);
        }

        //Start GetBrewery

        Brewery testBreweryObj = new Brewery()
        {
            Id = 10,
            Name = "mock_brewery_name"
            
        };

        BreweryModel testBreweryModeelObj = new  BreweryModel()
        {
            Id = 2,
            Name = "mock_brewery_name"
            
        };

        List< Brewery> testBrewerylistObj = new List< Brewery>() {
                new Brewery() {
                 Id = 100,
                 Name = "mock_brewery_name"
                
            },
                new Brewery() {
                 Id = 100,
                 Name = "mock_brewery_name"
                
            }
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

        BreweryBeerMappingModel breweryBeersMappingModel = new BreweryBeerMappingModel()
        {
            BreweryId = 1,
            BeerId = 2
        };

        #region GetBreweryWithId

        [Fact]
        public void GetBrewery_with_id_good_flow()
        {
            //arrange
            var BreweryId = 100;

            _breweryservice.Setup(x => x.GetBrewery(BreweryId)).Returns(testBreweryObj);

            _mapper.Setup(x => x.Map< Brewery,  BreweryModel>(testBreweryObj))
                .Returns(
                    new  BreweryModel()
                    {
                        Id = testBreweryObj.Id,
                        Name = testBreweryObj.Name
                    }
                );

            var actual_Brewery_responce = breweryController.GetBrewery(BreweryId);

            var expected_responce = new BreweryResponseModel()
            {
                breweryModel = new  BreweryModel()
                {
                    Id = testBreweryObj.Id,
                    Name = testBreweryObj.Name
                },
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBrewery_with_id_for_wrong_id()
        {
            var _testBreweryObj = testBreweryObj;
            _testBreweryObj.Id = 0;

            _breweryservice.Setup(x => x.GetBrewery(_testBreweryObj.Id)).Returns(_testBreweryObj);

            _mapper.Setup(x => x.Map< Brewery,  BreweryModel>(_testBreweryObj))
                .Returns(
                    new  BreweryModel()
                    {
                        Id = testBreweryObj.Id,
                        Name = testBreweryObj.Name
                    }
                );

            var actual_Brewery_responce = breweryController.GetBrewery(_testBreweryObj.Id);
            var expected_responce = new BreweryResponseModel()
            {
                breweryModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for Id")
            };

            //Assert.Throws<ArgumentException>(() => breweryController.GetBrewery(_testBreweryObj.Id));
            Assert.Equivalent(actual_Brewery_responce, expected_responce);
        }
        #endregion


        #region GetAllBrewery

        [Fact]
        public void GetBrewery_good_flow()
        {
            //arrange
            double gtAlcoholByVolume = 10;
            double ltAlcoholByVolume = 50;

            _breweryservice.Setup(x => x.GetBreweries()).Returns(testBrewerylistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Brewery>, IEnumerable<BreweryModel>>(testBrewerylistObj))
                .Returns(
                    new List<BreweryModel>()
                    {
                        new  BreweryModel()
                        {
                        Id = testBrewerylistObj[0].Id,
                        Name = testBrewerylistObj[0].Name
                        },
                         new  BreweryModel()
                        {
                        Id = testBrewerylistObj[1].Id,
                        Name = testBrewerylistObj[1].Name
                        }
                    }
                );

            var actual_Brewery_responce = breweryController.GetBrewery();

            var expected_responce = new BreweryListResponseModel()
            {
                breweryModel = new List<BreweryModel>()
                    {
                        new  BreweryModel()
                        {
                        Id = testBrewerylistObj[0].Id,
                        Name = testBrewerylistObj[0].Name
                        },
                         new  BreweryModel()
                        {
                        Id = testBrewerylistObj[1].Id,
                        Name = testBrewerylistObj[1].Name
                        }
                    },
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        //End GetBrewery

        #region UpdateBrewery

        [Fact]
        public void UpdateBrewery_good_flow()
        {
            //arrange

            var _tmpBreweryobj = new Brewery()
            {
                Id = testBreweryModeelObj.Id,
                Name = testBreweryModeelObj.Name
            };

            _breweryservice.Setup(x => x.PutBrewery(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.UpdateBrewery(testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBrewery_duplicate_name()
        {
            //arrange

            var _tmpBreweryobj = new Brewery()
            {
                Id = testBreweryModeelObj.Id,
                Name = testBreweryModeelObj.Name,
               
            };

            _breweryservice.Setup(x => x.PutBrewery(_tmpBreweryobj)).Returns(true);
            _breweryservice.Setup(x => x.IsExist(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.UpdateBrewery(testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("one brewery already exist with this name")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBrewery_wrong_id()
        {
            //arrange

            var _testBreweryModeelObj = testBreweryModeelObj;
            _testBreweryModeelObj.Id = 0;
            var _tmpBreweryobj = new Brewery()
            {
                Id = _testBreweryModeelObj.Id,
                Name = _testBreweryModeelObj.Name
            };

            _breweryservice.Setup(x => x.PutBrewery(_tmpBreweryobj)).Returns(true);
            _breweryservice.Setup(x => x.IsExist(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(_testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.UpdateBrewery(_testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for brewery")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBrewery_without_name()
        {
            //arrange

            var _testBreweryModeelObj = testBreweryModeelObj;
            _testBreweryModeelObj.Name = null;
            var _tmpBreweryobj = new Brewery()
            {
                Id = _testBreweryModeelObj.Id,
                Name = _testBreweryModeelObj.Name
            };

            _breweryservice.Setup(x => x.PutBrewery(_tmpBreweryobj)).Returns(true);
            _breweryservice.Setup(x => x.IsExist(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(_testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.UpdateBrewery(_testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for brewery")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }


        #endregion

        #region CreateBrewery

        [Fact]
        public void CreateBrewery_good_flow()
        {
            //arrange

            var _tmpBreweryobj = new Brewery()
            {
                Name = testBreweryModeelObj.Name,

            };

            _breweryservice.Setup(x => x.PostBrewery(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.CreateBrewery(testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void CreateBrewery_duplicate_name()
        {
            //arrange

            var _tmpBreweryobj = new Brewery()
            {
                Name = testBreweryModeelObj.Name,

            };

            _breweryservice.Setup(x => x.PostBrewery(_tmpBreweryobj)).Returns(true);
            _breweryservice.Setup(x => x.IsExist(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.CreateBrewery(testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("one brewery already exist with this name")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }


        [Fact]
        public void CreateBrewery_without_name()
        {
            //arrange

            var _testBreweryModeelObj = testBreweryModeelObj;
            _testBreweryModeelObj.Name = null;
            var _tmpBreweryobj = new Brewery()
            {
                Id = _testBreweryModeelObj.Id,
                Name = _testBreweryModeelObj.Name
            };

            _breweryservice.Setup(x => x.PostBrewery(_tmpBreweryobj)).Returns(true);
            _breweryservice.Setup(x => x.IsExist(_tmpBreweryobj)).Returns(true);

            _mapper.Setup(x => x.Map<BreweryModel, Brewery>(_testBreweryModeelObj))
                .Returns(_tmpBreweryobj
                );

            var actual_Brewery_responce = breweryController.CreateBrewery(_testBreweryModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for brewery")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        #region GetBrewerynBeerbyId

        [Fact]
        public void GetBreweryWithBeer_with_id_good_flow()
        {
            //arrange
            var BreweryId = testBreweryObj.Id;
            var tmpBreweryObjModel = new List<BeerModel>()
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
                };
            var tmpBrewery = new BreweryWithBeerModel()
            {
                Id = testBreweryObj.Id,
                Name = testBreweryObj.Name
            };
            _breweryservice.Setup(x => x.GetBrewery(BreweryId)).Returns(testBreweryObj);

            _mapper.Setup(x => x.Map<Brewery, BreweryWithBeerModel>(testBreweryObj)).Returns(tmpBrewery);

            _breweryservice.Setup(x => x.GetAllBeerWithBarid(BreweryId)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj)).Returns(tmpBreweryObjModel);

            var actual_Brewery_responce = breweryController.GetBreweryWithBeer(BreweryId);

            var expected_responce = new BreweryWithBeerResponseModel()
            {
                breweryWithBeerModel = new BreweryWithBeerModel()
                {
                    Id = testBreweryObj.Id,
                    Name = testBreweryObj.Name,
                    Beers = tmpBreweryObjModel
                },
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBreweryWithBeer_with_id_wrong_id()
        {
            var BreweryId = 0;
            //arrange

            var actual_Brewery_responce = breweryController.GetBreweryWithBeer(BreweryId);

            var expected_responce = new BreweryWithBeerResponseModel()
            {
                breweryWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for Id")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBreweryWithBeer_with_no_brewery_found()
        {
            //arrange
            var BreweryId = testBreweryObj.Id;
            var tmpnull = testBreweryObj;
            tmpnull = null;
            _breweryservice.Setup(x => x.GetBrewery(BreweryId)).Returns(tmpnull);

            var actual_Brewery_responce = breweryController.GetBreweryWithBeer(BreweryId);

            var expected_responce = new BreweryWithBeerResponseModel()
            {
                breweryWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("No brewery found")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        #region GetAllBrewerynBeerbyId

        [Fact]
        public void GetAllBrewerysWithBeer_with_id_good_flow()
        {
            //arrange
            var tmptestBrewerylistObj = testBrewerylistObj;

            var tmpBeerListObjModel = new List<BeerModel>()
                {
                    new BeerModel()
                    {
                        Id = tmptestBrewerylistObj[0].Id,
                        Name = testBeerlistObj[0].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[0].PercentageAlcoholByVolume
                    },
                    new BeerModel()
                    {
                        Id = testBeerlistObj[1].Id,
                        Name = testBeerlistObj[1].Name,
                        PercentageAlcoholByVolume = testBeerlistObj[1].PercentageAlcoholByVolume
                    }
                };
            var tmpBeerBrewery = new BreweryWithBeerModel()
            {
                Id = testBreweryObj.Id,
                Name = testBreweryObj.Name
            };
            var tmpBreweryObjModel = new List<BeerModel>()
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
                };
            var tmpBreweryWithBeerModel = new List<BreweryWithBeerModel>()
                        {
                            new  BreweryWithBeerModel()
                            {
                            Id = tmptestBrewerylistObj[0].Id,
                            Name = tmptestBrewerylistObj[0].Name
                            },
                             new  BreweryWithBeerModel()
                            {
                            Id = testBrewerylistObj[1].Id,
                            Name = testBrewerylistObj[1].Name
                            }
                        };

            _breweryservice.Setup(x => x.GetBreweries()).Returns(tmptestBrewerylistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Brewery>, IEnumerable<BreweryWithBeerModel>>(tmptestBrewerylistObj)).Returns(tmpBreweryWithBeerModel);

            foreach (var item in tmpBreweryWithBeerModel)
            {
                _breweryservice.Setup(x => x.GetAllBeerWithBarid(item.Id)).Returns(testBeerlistObj);
                _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj)).Returns(tmpBreweryObjModel);
                item.Beers = tmpBreweryObjModel;
            }


            var actual_Brewery_responce = breweryController.GetAllBreweriesWithBeer();

            var expected_responce = new BreweryWithBeerListResponseModel()
            {
                breweryWithBeerModel = tmpBreweryWithBeerModel,
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetAllBrewerysWithBeer_with_no_brewery_found()
        {
            //arrange
            var tmpnull = testBrewerylistObj;
            tmpnull = null;
            _breweryservice.Setup(x => x.GetBreweries()).Returns(tmpnull);

            var actual_Brewery_responce = breweryController.GetAllBreweriesWithBeer();

            var expected_responce = new BreweryWithBeerResponseModel()
            {
                breweryWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("No brewery found")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        #region UpdateBrewerybeerModel

        [Fact]
        public void UpdateBrewerybeerModel_good_flow()
        {
            //arrange
            var _tmpBreweryobj = new BreweryBeersMapping()
            {
                BreweryId = breweryBeersMappingModel.BreweryId,
                BeerId = breweryBeersMappingModel.BeerId,
                Id=0
            };

            _mapper.Setup(x => x.Map<BreweryBeerMappingModel, BreweryBeersMapping>(breweryBeersMappingModel)).Returns(_tmpBreweryobj);

            _breweryservice.Setup(x => x.UpdateBrewerybeerModel(_tmpBreweryobj)).Returns(true);

            var actual_Brewery_responce = breweryController.UpdateBreweriesbeerModel(breweryBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
            //	act
            //	asser
        }



        [Fact]
        public void UpdateBrewerybeerModel_with_wrong_beer_id()
        {
            //arrange

            var _breweryBeersMappingModel = breweryBeersMappingModel;
            _breweryBeersMappingModel.BeerId = 0;

            var actual_Brewery_responce = breweryController.UpdateBreweriesbeerModel(_breweryBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
        }

        [Fact]
        public void UpdateBrewerybeerModel_with_wrong_brewery_id()
        {
            //arrange

            var _breweryBeersMappingModel = breweryBeersMappingModel;
            _breweryBeersMappingModel.BreweryId = 0;

            var actual_Brewery_responce = breweryController.UpdateBreweriesbeerModel(_breweryBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
        }

        [Fact]
        public void UpdateBrewerybeerModel_with_null_param()
        {
            //arrange

            var _breweryBeersMappingModel = breweryBeersMappingModel;
            _breweryBeersMappingModel = null;

            var actual_Brewery_responce = breweryController.UpdateBreweriesbeerModel(_breweryBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Brewery_responce, expected_responce);
        }

        #endregion
    }
}

