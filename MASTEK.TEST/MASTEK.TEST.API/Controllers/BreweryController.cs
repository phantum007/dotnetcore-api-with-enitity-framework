using System.Collections.Generic;
using AutoMapper;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.DAL;
using MASTEK.TEST.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BreweryController : ControllerBase
{


    private readonly IBreweryService _breweryService;
    private readonly ILogger<BreweryController> _logger;
    private readonly IMapper _mapper;

    public BreweryController(ILogger<BreweryController> logger, IBreweryService breweryService, IMapper mapper)

    {
        _logger = logger;
        _breweryService = breweryService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public BreweryModel GetBrewery(int Id)
    {
        var response = _mapper.Map<Brewery, BreweryModel>(_breweryService.GetBrewery(Id));
        return response;
    }

 

    [HttpGet]
    public IEnumerable<BreweryModel> GetBrewery()
    {
        var response = _mapper.Map<IEnumerable<Brewery>, IEnumerable<BreweryModel>>(_breweryService.GetBreweries());
        return response;
    }

    [HttpPut]
    public bool UpdateBrewery(BreweryModel Brewerymodel)
    {
        var Brewery = _mapper.Map<BreweryModel, Brewery>(Brewerymodel);
        return _breweryService.PutBrewery(Brewery);
    }

    [HttpPost]
    public bool CreateBrewery(BreweryModel Brewerymodel)
    {
        var Brewery = _mapper.Map<BreweryModel, Brewery>(Brewerymodel);
        return _breweryService.PostBrewery(Brewery);
    }



    [HttpGet("{id:int}/beer")]
    public BreweryWithBeerModel GetBreweryWithBeer(int Id)
    {
        var brewery = _breweryService.GetBrewery(Id);
        var breweryBeerResponse = _mapper.Map<Brewery, BreweryWithBeerModel>(brewery);
        var beers = _breweryService.GetAllBeerWithBarid(Id);
        breweryBeerResponse.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);
        return breweryBeerResponse;
    }
    

   [HttpGet("beer")]
    public IEnumerable<BreweryWithBeerModel> GetAllBreweriesWithBeer()
    {       
        var brewery = _breweryService.GetBreweries();
        var breweryBeerResponse = _mapper.Map<IEnumerable<Brewery>, IEnumerable<BreweryWithBeerModel>>(brewery);
        foreach (var item in breweryBeerResponse)
        {
            var breweries = _breweryService.GetAllBeerWithBarid(item.Id);
            item.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(breweries);
        }
        return breweryBeerResponse;
    }

    [HttpPost("beer")]
    public bool UpdateBreweriesbeerModel(BreweryBeerMappingModel breweryWithBeerModel)
    {
        var bbm = _mapper.Map<BreweryBeerMappingModel, BreweryBeersMapping>(breweryWithBeerModel);

        return _breweryService.UpdateBrewerybeerModel(bbm);
    }
}

