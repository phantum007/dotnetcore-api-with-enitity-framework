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
public class BarController : ControllerBase
{


private readonly IBarService<TestMastekDbContext> _barservice;
private readonly ILogger<BarController> _logger;
private readonly IMapper _mapper;


public BarController(ILogger<BarController> logger, IBarService<TestMastekDbContext> Barservice, IMapper mapper)
{
    _logger = logger;
    _barservice = Barservice;
    _mapper = mapper;
}


#region BAR

[HttpGet("{id:int}")]
public BarResponseModel GetBar(int Id)
{
    if (Id == 0)
    {
        return new BarResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for Id") };
    }
    var response = _mapper.Map<Bar, BarModel>(_barservice.GetBar(Id));
    return new BarResponseModel() { barModel = response };
}

[HttpGet]
public BarListResponseModel GetBar()
{
    var response = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarModel>>(_barservice.GetBar());
    return new BarListResponseModel() { barsModel = response };
}

[HttpPut]
public CreateUpdateResponseModel UpdateBar(BarModel barmodel)
{
    var bar = _mapper.Map<BarModel, Bar>(barmodel);

    if (barmodel.Id == 0 || barmodel.Name == null || barmodel.Address == null)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for bar") };
    }
    if (_barservice.IsExist(bar))
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one bar already exist with this name and address") };
    }

    return new CreateUpdateResponseModel() { status = _barservice.PutBar(bar) };
}

[HttpPost]
public CreateUpdateResponseModel CreateBar(BarModel barmodel)
{
    var bar = _mapper.Map<BarModel, Bar>(barmodel);

    if ( barmodel.Name == null || barmodel.Address == null)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for bar") };
    }
    if (_barservice.IsExist(bar))
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("one bar already exist with this name and address") };
    }

    return new CreateUpdateResponseModel() { status = _barservice.PostBar(bar) };
}
#endregion

#region BAR_BEER

[HttpGet("{id:int}/beer")]
public BarWithBeerResponseModel GetBarWithBeer(int Id)
{
    if (Id == 0)
    {
        return new BarWithBeerResponseModel() { errorDetails = new InvalidInputExceptions("Invalid Input Value for Id") };
    }
    var bar = _barservice.GetBar(Id);
    if (bar == null)
    {
        return new BarWithBeerResponseModel() { errorDetails = new InvalidInputExceptions("No bar found") };
    }
    var barBeerResponse = _mapper.Map<Bar, BarWithBeerModel>(bar);
    var beers = _barservice.GetAllBeerWithBarid(Id);
    barBeerResponse.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);

    return new BarWithBeerResponseModel() { barWithBeerModel = barBeerResponse };
}

[HttpGet("beer")]
public BarWithBeerListResponseModel GetAllBarsWithBeer()
{
    var bars = _barservice.GetBar();
    if (bars == null)
    {
        return new BarWithBeerListResponseModel() { errorDetails = new InvalidInputExceptions("No bars found") };
    }
    var barBeerResponse = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarWithBeerModel>>(bars);
    foreach (var item in barBeerResponse)
    {
        var beers = _barservice.GetAllBeerWithBarid(item.Id);
        item.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);
    }
    return new BarWithBeerListResponseModel() { barWithBeerModel = barBeerResponse };
}

[HttpPost("beer")]
public CreateUpdateResponseModel UpdateBarbeerModel(BarBeersMappingModel barBeerMapping)
{
    if (barBeerMapping == null || barBeerMapping.BarId <=0 || barBeerMapping.BeerId <= 0)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputExceptions("Invalid input param") };
    }
    var bbm = _mapper.Map<BarBeersMappingModel, BarBeersMapping>(barBeerMapping);

        return new CreateUpdateResponseModel() { status = _barservice.UpdateBarbeerModel(bbm) };
}
#endregion
}

