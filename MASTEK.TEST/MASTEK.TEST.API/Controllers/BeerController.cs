using System.Collections.Generic;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using AutoMapper;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.API.ExceptionClaases;
using System.Net;
//using System.Net.Http;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
public class BeerController : ControllerBase
{


    private readonly IBeerservice _beerservice;
    private readonly ILogger<BeerController> _logger;
    private readonly IMapper _mapper;


    public BeerController(ILogger<BeerController> logger, IBeerservice beerservice, IMapper mapper)

    {
        _logger = logger;
        _beerservice = beerservice;
        _mapper = mapper;
    }
   

    [HttpGet("beer/{id:int}")]
    public BeerResponceModel GetBeer(int Id)
    {
        try
        {
            if (Id == 0)
            {
                return new BeerResponceModel() { errorDetails  = new InvalidInputExceptions( "Invalid Input Value for Id") };
            }
            var response = _mapper.Map<Beer, BeerModel>(_beerservice.GetBeer(Id));
            return new BeerResponceModel() { beerModel=response};
        }
        catch (Exception ex)
        {
            return new BeerResponceModel() { errorDetails = ex };
        }
       
    }

    [HttpGet("beer")]
    public IEnumerable<BeerModel> GetBeer( double? gtAlcoholByVolume=0, double? ltAlcoholByVolume=100)
    {
        var response = _mapper.Map<IEnumerable<Beer>, IEnumerable< BeerModel> >(_beerservice.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume));
        return response;
    }

    [HttpPut("beer")]
    public bool UpdateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);
        return _beerservice.PutBeer(beer);
    }

    [HttpPost("beer")]
    public bool CreateBeer(BeerModel beermodel)
    {
        var beer = _mapper.Map<BeerModel, Beer>(beermodel);
        return _beerservice.PostBeer(beer);
    }
}

