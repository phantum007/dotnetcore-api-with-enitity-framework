using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MASTEK.TEST.API.Models;

public class BeerModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name cannot be empty")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "PercentageAlcoholByVolume cannot be empty")]
    public double? PercentageAlcoholByVolume { get; set; }

}


public class BeerResponceModel
{
    public BeerModel beerModel { get; set; }
    public Exception errorDetails { get; set; }
}

public class BeerListResponceModel
{
    public IEnumerable<BeerModel> beersModel { get; set; }
    public Exception errorDetails { get; set; }

}

public class BeerUpdateResponceModel
{
    public bool updateStatus { get; set; }
    public Exception errorDetails { get; set; }
}