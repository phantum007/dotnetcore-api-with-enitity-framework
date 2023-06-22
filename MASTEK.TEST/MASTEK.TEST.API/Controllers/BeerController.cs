using System.Collections.Generic;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using AutoMapper;
using MASTEK.TEST.API.Models;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
//[Route("[controller]")]
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
    public BeerModel GetBeer(int Id)
    {
        var response1 = _beerservice.GetBeer(Id);
        var response = _mapper.Map<Beer, BeerModel>(response1);
          return response;
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

