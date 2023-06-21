using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BreweryController : ControllerBase
{


    private readonly ILogger<BreweryController> _logger;

    public BreweryController(ILogger<BreweryController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Brewery")]
    public IEnumerable<Brewery> GetBrewery(int Id, double? gtAlcoholByVolume, double? ltAlcoholByVolume)
    {
        if (Id != 0)
        {
            return Enumerable.Range(1, 5).Select(index => new Brewery
            {
                Id = index,
                Name = "Brewery Id " + index,
            })
        .ToArray();
        }
        else
        {
            return Enumerable.Range(10, 15).Select(index => new Brewery
            {
                Id = index,
                Name = "Breweryfilter Id " + index,
            })
       .ToArray();
        }
    }

    [HttpPut(Name = "Brewery")]
    public IEnumerable<Brewery> Get(int Id)
    {
        return Enumerable.Range(1, 5).Select(index => new Brewery
        {
            Id = index,
            Name = "Brewery Id " + index,
        });
    }

    [HttpPost(Name = "Brewery")]
    public IEnumerable<Brewery> Get(Brewery brewery
        )
    {
        return Enumerable.Range(1, 5).Select(index => brewery)
        .ToArray();
    }
}
