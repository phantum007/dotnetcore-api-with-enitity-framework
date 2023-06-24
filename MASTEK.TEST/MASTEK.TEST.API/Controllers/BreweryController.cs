using System.Collections.Generic;
using AutoMapper;
using MASTEK.TEST.API.ExceptionClaases;
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


    #region Brewery

    [HttpGet("{id:int}")]
    public BreweryResponseModel GetBrewery(int Id)
    {
        if (Id == 0)
        {
            return new BreweryResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for Id") };
        }
        var response = _mapper.Map<Brewery, BreweryModel>(_breweryService.GetBrewery(Id));
        return new BreweryResponseModel() { breweryModel = response };
    }

    [HttpGet]
    public BreweryListResponseModel GetBrewery()
    {
        var response = _mapper.Map<IEnumerable<Brewery>, IEnumerable<BreweryModel>>(_breweryService.GetBreweries());
        return new BreweryListResponseModel() { breweryModel = response };
    }

    [HttpPut]
    public CreateUpdateResponseModel UpdateBrewery(BreweryModel brewerymodel)
    {

        var brewery = _mapper.Map<BreweryModel, Brewery>(brewerymodel);

        if (brewery.Id == 0 || brewery.Name == null )
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for brewery") };
        }
        if (_breweryService.IsExist(brewery))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one brewery already exist with this name") };
        }

        return new CreateUpdateResponseModel() { status = _breweryService.PutBrewery(brewery) };

    }

    [HttpPost]
    public CreateUpdateResponseModel CreateBrewery(BreweryModel brewerymodel)
    {
        var brewery = _mapper.Map<BreweryModel, Brewery>(brewerymodel);

        if (brewerymodel.Name == null )
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for brewery") };
        }
        if (_breweryService.IsExist(brewery))
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one brewery already exist with this name") };
        }

        return new CreateUpdateResponseModel() { status = _breweryService.PostBrewery(brewery) };

    }
    #endregion


    #region Brewerybeer

    [HttpGet("{id:int}/beer")]
    public BreweryWithBeerResponseModel GetBreweryWithBeer(int Id)
    {
        if (Id == 0)
        {
            return new BreweryWithBeerResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for Id") };
        }
        var brewery = _breweryService.GetBrewery(Id);
        if (brewery == null)
        {
            return new BreweryWithBeerResponseModel() { errorDetails = new InvalidInputExceptions("No brewery found") };
        }
        var breweryBeerResponse = _mapper.Map<Brewery, BreweryWithBeerModel>(brewery);
        var beers = _breweryService.GetAllBeerWithBarid(Id);
        breweryBeerResponse.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);

        return new BreweryWithBeerResponseModel() {  breweryWithBeerModel = breweryBeerResponse };
    }


    [HttpGet("beer")]
    public BreweryWithBeerListResponseModel GetAllBreweriesWithBeer()
    {
        var brewery = _breweryService.GetBreweries();
        if (brewery == null)
        {
            return new BreweryWithBeerListResponseModel() { errorDetails = new InvalidInputExceptions("No brewery found") };
        }
        var breweryBeerResponse = _mapper.Map<IEnumerable<Brewery>, IEnumerable<BreweryWithBeerModel>>(brewery);
        foreach (var item in breweryBeerResponse)
        {
            var breweries = _breweryService.GetAllBeerWithBarid(item.Id);
            item.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(breweries);
        }
        return new BreweryWithBeerListResponseModel() {   breweryWithBeerModel = breweryBeerResponse };
    }

    [HttpPost("beer")]
    public CreateUpdateResponseModel UpdateBreweriesbeerModel(BreweryBeerMappingModel breweryWithBeerModel)
    {
        if (breweryWithBeerModel == null || breweryWithBeerModel.BreweryId <= 0 || breweryWithBeerModel.BeerId <= 0)
        {
            return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid input param") };
        }
        var bbm = _mapper.Map<BreweryBeerMappingModel, BreweryBeersMapping>(breweryWithBeerModel);

        return new CreateUpdateResponseModel() { status = _breweryService.UpdateBrewerybeerModel(bbm) };
    }
    #endregion
}

