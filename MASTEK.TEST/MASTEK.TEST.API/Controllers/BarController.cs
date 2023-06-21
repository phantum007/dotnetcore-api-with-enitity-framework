using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MASTEK.TEST.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BarController : ControllerBase
{


    private readonly ILogger<BarController> _logger;
    private readonly IBeerservice _beerservice;

    public BarController(ILogger<BarController> logger, IBeerservice beerservice)
    {
        _logger = logger;
        _beerservice = beerservice;
    }


    [HttpGet(Name = "Bar")]
    public IEnumerable<Bar> GetBar(int Id, double? gtAlcoholByVolume, double? ltAlcoholByVolume)
    {
        if (Id != 0)
        {
            return 
                Enumerable.Range(1, 5).Select(index => new Bar
            {
                Id = index,
                Name = "Bar",
            })
        .ToArray();
        }
        else
        {
            return Enumerable.Range(1, 5).Select(index => new Bar
            {
                Id = 3,
                Name = "bar-3"
            })
       .ToArray();
        }
    }

    [HttpPut(Name = "Bar")]
    public IEnumerable<Bar> Get(int Id)
    {
        return Enumerable.Range(1, 5).Select(index => new Bar
        {
            Id = 2,
            Name = "sasa",
        })
        .ToArray();
    }

    [HttpPost(Name = "Bar")]
    public IEnumerable<Bar> Get(Bar Bar)
    {
        return Enumerable.Range(1, 5).Select(index => Bar)
        .ToArray();
    }
}

