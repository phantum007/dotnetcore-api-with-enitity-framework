using System.Collections.Generic;
using AutoMapper;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BarController : ControllerBase
{


    private readonly IBarService _barservice;
    private readonly ILogger<BarController> _logger;
    private readonly IMapper _mapper;


    public BarController(ILogger<BarController> logger, IBarService Barservice, IMapper mapper)

    {
        _logger = logger;
        _barservice = Barservice;
        _mapper = mapper;
    }


    [HttpGet("{id:int}")]
    public BarModel GetBar(int Id)
    {
        var response = _mapper.Map<Bar, BarModel>(_barservice.GetBar(Id));
        return response;
    }

    [HttpGet]
    public IEnumerable<BarModel> GetBar()
    {
        var response = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarModel>>(_barservice.GetBar());
        return response;
    }

    [HttpPut]
    public bool UpdateBar(BarModel Barmodel)
    {
        var Bar = _mapper.Map<BarModel, Bar>(Barmodel);
        return _barservice.PutBar(Bar);
    }

    [HttpPost]
    public bool CreateBar(BarModel barmodel)
    {
        var Bar = _mapper.Map<BarModel, Bar>(barmodel);
        return _barservice.PostBar(Bar);
    }

    [HttpGet("{id:int}/beer")]
    public BarWithBeerModel GetBarWithBeer(int Id)
    {
        var bar = _barservice.GetBar(Id);
        var barBeerResponse = _mapper.Map<Bar, BarWithBeerModel>(bar);
        var beers = _barservice.GetAllBeerWithBarid(Id);
        barBeerResponse.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);
        return barBeerResponse;
    }

    [HttpGet("beer")]
    public IEnumerable<BarWithBeerModel> GetAllBreweriesWithBeer()
    {
        var bars = _barservice.GetBar();
        var barBeerResponse = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarWithBeerModel>>(bars);
        foreach (var item in barBeerResponse)
        {
            var beers = _barservice.GetAllBeerWithBarid(item.Id);
            item.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);
        }
        return barBeerResponse;
    }

    [HttpPost("beer")]
    public Boolean UpdateBarbeerModel(BarBeersMappingModel barBeerMapping)
    {
        var bbm = _mapper.Map<BarBeersMappingModel, BarBeersMapping>(barBeerMapping);

        return _barservice.UpdateBarbeerModel(bbm);
    }
}

