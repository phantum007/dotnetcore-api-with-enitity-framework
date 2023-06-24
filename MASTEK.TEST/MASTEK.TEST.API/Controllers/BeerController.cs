using System.Collections.Generic;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using AutoMapper;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.API.ExceptionClaases;
using System.Net;
using System.Data;
//using System.Net.Http;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
public class BeerController : ControllerBase
{


    private readonly IBeerservice<TestMastekDbContext> _beerservice;
    private readonly ILogger<BeerController> _logger;
    private readonly IMapper _mapper;


    public BeerController(ILogger<BeerController> logger, IBeerservice<TestMastekDbContext> beerservice, IMapper mapper)

    {
        _logger = logger;
        _beerservice = beerservice;
        _mapper = mapper;
    }
   

    [HttpGet("beer/{id:int}")]
    public BeerResponseModel GetBeer(int Id)
    {
            if (Id == 0)
            {
                return new BeerResponseModel() { errorDetails  = new InvalidInputExceptions( "Invalid Input Value for Id") };
            }
            var response = _mapper.Map<Beer, BeerModel>(_beerservice.GetBeer(Id));
            return new BeerResponseModel() { beerModel=response};
    }

    [HttpGet("beer")]
    public BeerListResponseModel GetBeer( double? gtAlcoholByVolume=0, double? ltAlcoholByVolume=100)
    {
        if (gtAlcoholByVolume >= 100)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for gtAlcoholByVolume") };
        }
        if (ltAlcoholByVolume <= 0)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for ltAlcoholByVolume") };
        }
        if (ltAlcoholByVolume == gtAlcoholByVolume)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value : ltAlcoholByVolume can not be same as gtAlcoholByVolume") };
        }
        var response = _mapper.Map<IEnumerable<Beer>, IEnumerable< BeerModel> >(_beerservice.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume));
         return new BeerListResponseModel() { beersModel=response};
    }

    [HttpPut("beer")]
    public CreateUpdateResponseModel UpdateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);

        if (beermodel.Id == 0 || beermodel.Name == null || beermodel.PercentageAlcoholByVolume < 0 || beermodel.PercentageAlcoholByVolume > 100)
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for beer") };
        }
        if (_beerservice.IsExist(beer))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one beer already exist with this name and volume") };
        }

        return new CreateUpdateResponseModel() { status = _beerservice.PutBeer(beer) };
    }

    [HttpPost("beer")]
    public CreateUpdateResponseModel CreateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);

        if (beermodel.Name == null || beermodel.PercentageAlcoholByVolume < 0 || beermodel.PercentageAlcoholByVolume > 100)
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for beer") };
        }
        if (_beerservice.IsExist(beer))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one beer already exist with this name and volume") };
        }
        return new CreateUpdateResponseModel() { status = _beerservice.PostBeer(beer) };
    }
}

