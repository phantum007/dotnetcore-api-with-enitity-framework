using System;
using System.Xml.Linq;
using AutoMapper;
using MASTEK.INTERVIEW.API.Controllers;
using MASTEK.INTERVIEW.API.ExceptionClaases;
using MASTEK.INTERVIEW.API.Models;
using MASTEK.INTERVIEW.DAL;
using MASTEK.INTERVIEW.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MASTEK.INTERVIEW.UNITTEST_X.Controllers
{
	public class BarControllerTest
    {
        private  readonly BarController barController;

        private readonly Mock<IBarService<TestMastekDbContext>> _barservice = new Mock<IBarService<TestMastekDbContext>>();
		private readonly Mock<ILogger<BarController>> _logger = new Mock<ILogger<BarController>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public BarControllerTest()
        {
            barController = new BarController(_logger.Object, _barservice.Object, _mapper.Object);
        }

        //Start GetBar

        Bar testBarObj = new Bar()
        {
            Id = 10,
            Name = "mock_bar_name",
            Address = "leeds"
        };

        BarModel testBarModeelObj = new  BarModel()
        {
            Id = 2,
            Name = "mock_bar_name",
            Address = "leeds"
        };

        List< Bar> testBarlistObj = new List< Bar>() {
                new Bar() {
                 Id = 100,
                 Name = "mock_bar_name",
                Address = "leeds"
            },
                new Bar() {
                 Id = 100,
                 Name = "mock_bar_name",
                Address = "leeds"
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

        BarBeersMappingModel barBeersMappingModel = new BarBeersMappingModel()
        {
            BarId = 1,
            BeerId = 2
        };

        #region GetBarWithId

        [Fact]
        public void GetBar_with_id_good_flow()
        {
            //arrange
            var BarId = 100;

            _barservice.Setup(x => x.GetBar(BarId)).Returns(testBarObj);

            _mapper.Setup(x => x.Map< Bar,  BarModel>(testBarObj))
                .Returns(
                    new  BarModel()
                    {
                        Id = testBarObj.Id,
                        Name = testBarObj.Name,
                        Address = testBarObj.Address
                    }
                );

            var actual_Bar_responce = barController.GetBar(BarId);

            var expected_responce = new BarResponseModel()
            {
                barModel = new  BarModel()
                {
                    Id = testBarObj.Id,
                    Name = testBarObj.Name,
                    Address = testBarObj.Address
                },
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBar_with_id_for_wrong_id()
        {
            var _testBarObj = testBarObj;
            _testBarObj.Id = 0;

            _barservice.Setup(x => x.GetBar(_testBarObj.Id)).Returns(_testBarObj);

            _mapper.Setup(x => x.Map< Bar,  BarModel>(_testBarObj))
                .Returns(
                    new  BarModel()
                    {
                        Id = testBarObj.Id,
                        Name = testBarObj.Name,
                        Address = testBarObj.Address
                    }
                );

            var actual_Bar_responce = barController.GetBar(_testBarObj.Id);
            var expected_responce = new BarResponseModel()
            {
                barModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for Id")
            };

            //Assert.Throws<ArgumentException>(() => barController.GetBar(_testBarObj.Id));
            Assert.Equivalent(actual_Bar_responce, expected_responce);
        }
        #endregion


        #region GetAllBar

        [Fact]
        public void GetBar_good_flow()
        {
            //arrange
            double gtAlcoholByVolume = 10;
            double ltAlcoholByVolume = 50;

            _barservice.Setup(x => x.GetBar()).Returns(testBarlistObj);

            _mapper.Setup(x => x.Map<IEnumerable< Bar>, IEnumerable< BarModel>>(testBarlistObj))
                .Returns(
                    new List< BarModel>()
                    {
                        new  BarModel()
                        {
                        Id = testBarlistObj[0].Id,
                        Name = testBarlistObj[0].Name,
                        Address = testBarlistObj[0].Address
                        },
                         new  BarModel()
                        {
                        Id = testBarlistObj[1].Id,
                        Name = testBarlistObj[1].Name,
                        Address = testBarlistObj[1].Address
                        }
                    }
                );

            var actual_Bar_responce = barController.GetBar();

            var expected_responce = new BarListResponseModel()
            {
                barsModel = new List< BarModel>()
                    {
                        new  BarModel()
                        {
                        Id = testBarlistObj[0].Id,
                        Name = testBarlistObj[0].Name,
                        Address = testBarlistObj[0].Address
                        },
                         new  BarModel()
                        {
                        Id = testBarlistObj[1].Id,
                        Name = testBarlistObj[1].Name,
                        Address = testBarlistObj[1].Address
                        }
                    },
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        //End GetBar

        #region UpdateBar

        [Fact]
        public void UpdateBar_good_flow()
        {
            //arrange

            var _tmpBarobj = new Bar()
            {
                Id = testBarModeelObj.Id,
                Name = testBarModeelObj.Name,
                Address = testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PutBar(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.UpdateBar(testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBar_duplicate_name()
        {
            //arrange

            var _tmpBarobj = new Bar()
            {
                Id = testBarModeelObj.Id,
                Name = testBarModeelObj.Name,
                Address = testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PutBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.UpdateBar(testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("one bar already exist with this name and address")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBar_wrong_id()
        {
            //arrange

            var _testBarModeelObj = testBarModeelObj;
            _testBarModeelObj.Id = 0;
            var _tmpBarobj = new Bar()
            {
                Id = _testBarModeelObj.Id,
                Name = _testBarModeelObj.Name,
                Address = _testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PutBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(_testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.UpdateBar(_testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for bar")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBar_without_name()
        {
            //arrange

            var _testBarModeelObj = testBarModeelObj;
            _testBarModeelObj.Name = null;
            var _tmpBarobj = new Bar()
            {
                Id = _testBarModeelObj.Id,
                Name = _testBarModeelObj.Name,
                Address = _testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PutBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(_testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.UpdateBar(_testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for bar")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void UpdateBar_wrong_Address()
        {
            //arrange

            var _testBarModeelObj = testBarModeelObj;
            _testBarModeelObj.Address = null;
            var _tmpBarobj = new Bar()
            {
                Id = _testBarModeelObj.Id,
                Name = _testBarModeelObj.Name,
                Address = _testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PutBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(_testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.UpdateBar(_testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for bar")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }
        #endregion

        #region CreateBar

        [Fact]
        public void CreateBar_good_flow()
        {
            //arrange

            var _tmpBarobj = new Bar()
            {
                Name = testBarModeelObj.Name,
                Address = testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PostBar(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.CreateBar(testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void CreateBar_duplicate_name()
        {
            //arrange

            var _tmpBarobj = new Bar()
            {
                Name = testBarModeelObj.Name,
                Address = testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PostBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.CreateBar(testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("one bar already exist with this name and address")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }


        [Fact]
        public void CreateBar_without_name()
        {
            //arrange

            var _testBarModeelObj = testBarModeelObj;
            _testBarModeelObj.Name = null;
            var _tmpBarobj = new Bar()
            {
                Id = _testBarModeelObj.Id,
                Name = _testBarModeelObj.Name,
                Address = _testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PostBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(_testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.CreateBar(_testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for bar")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void CreateBar_wrong_Address()
        {
            //arrange

            var _testBarModeelObj = testBarModeelObj;
            _testBarModeelObj.Address = null;
            var _tmpBarobj = new Bar()
            {
                Name = _testBarModeelObj.Name,
                Address = _testBarModeelObj.Address
            };

            _barservice.Setup(x => x.PostBar(_tmpBarobj)).Returns(true);
            _barservice.Setup(x => x.IsExist(_tmpBarobj)).Returns(true);

            _mapper.Setup(x => x.Map< BarModel, Bar>(_testBarModeelObj))
                .Returns(_tmpBarobj
                );

            var actual_Bar_responce = barController.CreateBar(_testBarModeelObj);



            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for bar")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }
        #endregion

        #region GetBarnBeerbyId

        [Fact]
        public void GetBarWithBeer_with_id_good_flow()
        {
            //arrange
            var BarId = testBarObj.Id;
            var tmpBarObjModel = new List<BeerModel>()
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
            var tmpBar = new BarWithBeerModel()
            {
                Id = testBarObj.Id,
                Name = testBarObj.Name,
                Address = testBarObj.Address
            };
            _barservice.Setup(x => x.GetBar(BarId)).Returns(testBarObj);

            _mapper.Setup(x => x.Map<Bar, BarWithBeerModel>(testBarObj)).Returns( tmpBar );

            _barservice.Setup(x => x.GetAllBeerWithBarid(BarId)).Returns(testBeerlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj)).Returns(tmpBarObjModel);

            var actual_Bar_responce = barController.GetBarWithBeer(BarId);

            var expected_responce = new BarWithBeerResponseModel()
            {
                barWithBeerModel = new BarWithBeerModel()
                {
                    Id = testBarObj.Id,
                    Name = testBarObj.Name,
                    Address = testBarObj.Address,
                    Beers= tmpBarObjModel
                },
                errorDetails = null
            };

        Assert.Equivalent(actual_Bar_responce, expected_responce);
        //	act
        //	asser
    }

        [Fact]
        public void GetBarWithBeer_with_id_wrong_id()
        {
            var BarId = 0;
            //arrange

            var actual_Bar_responce = barController.GetBarWithBeer(BarId);

            var expected_responce = new BarWithBeerResponseModel()
            {
                barWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("Invalid Input Value for Id")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetBarWithBeer_with_no_bar_found()
        {
            //arrange
            var BarId = testBarObj.Id;
            var tmpnull = testBarObj;
            tmpnull = null;
            _barservice.Setup(x => x.GetBar(BarId)).Returns(tmpnull);

            var actual_Bar_responce = barController.GetBarWithBeer(BarId);

            var expected_responce = new BarWithBeerResponseModel()
            {
                barWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("No bar found")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        #region GetAllBarnBeerbyId

        [Fact]
        public void GetAllBarsWithBeer_with_id_good_flow()
        {
            //arrange
            var tmptestBarlistObj = testBarlistObj;

            var tmpBeerListObjModel = new List<BeerModel>()
            {
                new BeerModel()
                {
                    Id = tmptestBarlistObj[0].Id,
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
            var tmpBeerBar = new BarWithBeerModel()
            {
                Id = testBarObj.Id,
                Name = testBarObj.Name,
                Address = testBarObj.Address
            };
            var tmpBarObjModel = new List<BeerModel>()
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
            var tmpBarWithBeerModel = new List<BarWithBeerModel>()
                    {
                        new  BarWithBeerModel()
                        {
                        Id = tmptestBarlistObj[0].Id,
                        Name = tmptestBarlistObj[0].Name,
                        Address = tmptestBarlistObj[0].Address
                        },
                         new  BarWithBeerModel()
                        {
                        Id = testBarlistObj[1].Id,
                        Name = testBarlistObj[1].Name,
                        Address = testBarlistObj[1].Address
                        }
                    };

            _barservice.Setup(x => x.GetBar()).Returns(tmptestBarlistObj);

            _mapper.Setup(x => x.Map<IEnumerable<Bar>, IEnumerable<BarWithBeerModel>>(tmptestBarlistObj)).Returns(tmpBarWithBeerModel);

            foreach (var item in tmpBarWithBeerModel)
            {
              _barservice.Setup(x => x.GetAllBeerWithBarid(item.Id)).Returns(testBeerlistObj);
              _mapper.Setup(x => x.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(testBeerlistObj)).Returns(tmpBarObjModel);
                item.Beers = tmpBarObjModel;
            }


            var actual_Bar_responce = barController.GetAllBarsWithBeer();

            var expected_responce = new BarWithBeerListResponseModel()
            {
                barWithBeerModel = tmpBarWithBeerModel,
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        [Fact]
        public void GetAllBarsWithBeer_with_no_bar_found()
        {
            //arrange
            var tmpnull = testBarlistObj;
            tmpnull = null;
            _barservice.Setup(x => x.GetBar()).Returns(tmpnull);

            var actual_Bar_responce = barController.GetAllBarsWithBeer();

            var expected_responce = new BarWithBeerResponseModel()
            {
                barWithBeerModel = null,
                errorDetails = new InvalidInputExceptions("No bars found")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

        #endregion

        #region UpdateBarbeerModel

        [Fact]
        public void UpdateBarbeerModel_good_flow()
        {
            //arrange
            var _tmpBarobj = new BarBeersMapping()
            {
                BarId = barBeersMappingModel.BarId,
                BeerId = barBeersMappingModel.BeerId,
                Id=0
            };

            _mapper.Setup(x => x.Map<BarBeersMappingModel, BarBeersMapping>(barBeersMappingModel)).Returns(_tmpBarobj);

            _barservice.Setup(x => x.UpdateBarbeerModel(_tmpBarobj)).Returns(true);

            var actual_Bar_responce = barController.UpdateBarbeerModel(barBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = true,
                errorDetails = null
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
            //	act
            //	asser
        }

       

        [Fact]
        public void UpdateBarbeerModel_with_wrong_beer_id()
        {
            //arrange

            var _barBeersMappingModel = barBeersMappingModel;
            _barBeersMappingModel.BeerId = 0;

            var actual_Bar_responce = barController.UpdateBarbeerModel(_barBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
        }

        [Fact]
        public void UpdateBarbeerModel_with_wrong_bar_id()
        {
            //arrange

            var _barBeersMappingModel = barBeersMappingModel;
            _barBeersMappingModel.BarId = 0;

            var actual_Bar_responce = barController.UpdateBarbeerModel(_barBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
        }

        [Fact]
        public void UpdateBarbeerModel_with_null_param()
        {
            //arrange

            var _barBeersMappingModel = barBeersMappingModel;
            _barBeersMappingModel = null;

            var actual_Bar_responce = barController.UpdateBarbeerModel(_barBeersMappingModel);

            var expected_responce = new CreateUpdateResponseModel()
            {
                status = false,
                errorDetails = new InvalidInputExceptions("Invalid input param")
            };

            Assert.Equivalent(actual_Bar_responce, expected_responce);
        }

        #endregion
    }
}

