﻿using AutoMapper;
using MASTEK.INTERVIEW.API.ExceptionClaases;
using MASTEK.INTERVIEW.API.Models;
using MASTEK.INTERVIEW.DAL;
using MASTEK.INTERVIEW.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.INTERVIEW.API.Controllers;

[ApiController]
public class BeerController : ControllerBase
{


    private readonly IBeerservice<TestMastekDbContext> _beerService;
    private readonly ILogger<BeerController> _logger;
    private readonly IMapper _mapper;


    public BeerController(ILogger<BeerController> logger, IBeerservice<TestMastekDbContext> beerService, IMapper mapper)

    {
        _logger = logger;
        _beerService = beerService;
        _mapper = mapper;
    }
   

    [HttpGet("beer/{id:int}")]
    public BeerResponseModel GetBeer(int Id)
    {
        if (Id == 0)
        {
            return new BeerResponseModel() { errorDetails  = new InvalidInputOutputExceptions( "Invalid Input Value for Id") };
        }
        var response = _mapper.Map<Beer, BeerModel>(_beerService.GetBeer(Id));
        return new BeerResponseModel() { beerModel=response};
    }

    [HttpGet("beer")]
    public BeerListResponseModel GetBeer( double? gtAlcoholByVolume=0, double? ltAlcoholByVolume=100)
    {
        if (gtAlcoholByVolume >= 100)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for gtAlcoholByVolume") };
        }
        if (ltAlcoholByVolume <= 0)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for ltAlcoholByVolume") };
        }
        if (ltAlcoholByVolume == gtAlcoholByVolume)
        {
            return new BeerListResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value : ltAlcoholByVolume can not be same as gtAlcoholByVolume") };
        }
        var response = _mapper.Map<IEnumerable<Beer>, IEnumerable< BeerModel> >(_beerService.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume));
         return new BeerListResponseModel() { beersModel=response};
    }

    [HttpPut("beer")]
    public CreateUpdateResponseModel UpdateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);

        if (beermodel.Id == 0 || beermodel.Name == null || beermodel.PercentageAlcoholByVolume < 0 || beermodel.PercentageAlcoholByVolume > 100)
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for beer") };
        }
        if (_beerService.IsExist(beer))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("one beer already exist with this name and volume") };
        }

        return new CreateUpdateResponseModel() { status = _beerService.PutBeer(beer) };
    }

    [HttpPost("beer")]
    public CreateUpdateResponseModel CreateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);

        if (beermodel.Name == null || beermodel.PercentageAlcoholByVolume < 0 || beermodel.PercentageAlcoholByVolume > 100)
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for beer") };
        }
        if (_beerService.IsExist(beer))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("one beer already exist with this name and volume") };
        }
        return new CreateUpdateResponseModel() { status = _beerService.PostBeer(beer) };
    }
}

