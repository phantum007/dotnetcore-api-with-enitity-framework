using AutoMapper;
using MASTEK.INTERVIEW.API.ExceptionClaases;
using MASTEK.INTERVIEW.API.Models;
using MASTEK.INTERVIEW.DAL;
using MASTEK.INTERVIEW.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.INTERVIEW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BarController : ControllerBase
{


private readonly IBarService<TestMastekDbContext> _barService;
private readonly ILogger<BarController> _logger;
private readonly IMapper _mapper;


public BarController(ILogger<BarController> logger, IBarService<TestMastekDbContext> barService, IMapper mapper)
{
    _logger = logger;
    _barService = barService;
    _mapper = mapper;
}


#region BAR

[HttpGet("{id:int}")]
public BarResponseModel GetBar(int id)
{
    if (id == 0)
    {
        return new BarResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for Id") };
    }
    var response = _mapper.Map<Bar, BarModel>(_barService.GetBar(id));
    return new BarResponseModel() { barModel = response };
}

[HttpGet]
public BarListResponseModel GetBar()
{
    var response = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarModel>>(_barService.GetBar());
    return new BarListResponseModel() { barsModel = response };
}

[HttpPut]
public CreateUpdateResponseModel UpdateBar(BarModel barModel)
{
    var bar = _mapper.Map<BarModel, Bar>(barModel);

    if (barModel.Id == 0 || barModel.Name == null || barModel.Address == null)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for bar") };
    }
    if (_barService.IsExist(bar))
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("one bar already exist with this name and address") };
    }

    return new CreateUpdateResponseModel() { status = _barService.PutBar(bar) };
}

[HttpPost]
public CreateUpdateResponseModel CreateBar(BarModel barModel)
{
    var bar = _mapper.Map<BarModel, Bar>(barModel);

    if (barModel.Name == null || barModel.Address == null)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for bar") };
    }
    if (_barService.IsExist(bar))
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("one bar already exist with this name and address") };
    }

    return new CreateUpdateResponseModel() { status = _barService.PostBar(bar) };
}
#endregion

#region BAR_BEER

[HttpGet("{id:int}/beer")]
public BarWithBeerResponseModel GetBarWithBeer(int id)
{
    if (id == 0)
    {
        return new BarWithBeerResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid Input Value for Id") };
    }
    var bar = _barService.GetBar(id);
    if (bar == null)
    {
        return new BarWithBeerResponseModel() { errorDetails = new InvalidInputOutputExceptions("No bar found") };
    }
    var response = _mapper.Map<Bar, BarWithBeerModel>(bar);
    var beers = _barService.GetAllBeerWithBarid(id);
    response.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);

    return new BarWithBeerResponseModel() { barWithBeerModel = response };
}

[HttpGet("beer")]
public BarWithBeerListResponseModel GetAllBarsWithBeer()
{
    var bars = _barService.GetBar();
    if (bars == null)
    {
        return new BarWithBeerListResponseModel() { errorDetails = new InvalidInputOutputExceptions("No bars found") };
    }
    var response = _mapper.Map<IEnumerable<Bar>, IEnumerable<BarWithBeerModel>>(bars);
    foreach (var item in response)
    {
        var beers = _barService.GetAllBeerWithBarid(item.Id);
        item.Beers = _mapper.Map<IEnumerable<Beer>, IEnumerable<BeerModel>>(beers);
    }
    return new BarWithBeerListResponseModel() { barWithBeerModel = response };
}

[HttpPost("beer")]
public CreateUpdateResponseModel UpdateBarbeerModel(BarBeersMappingModel barBeerMapping)
{
    if (barBeerMapping == null || barBeerMapping.BarId <=0 || barBeerMapping.BeerId <= 0)
    {
        return new CreateUpdateResponseModel() { errorDetails = new InvalidInputOutputExceptions("Invalid input param") };
    }
    var barBeer = _mapper.Map<BarBeersMappingModel, BarBeersMapping>(barBeerMapping);

        return new CreateUpdateResponseModel() { status = _barService.UpdateBarbeerModel(barBeer) };
}
#endregion
}

