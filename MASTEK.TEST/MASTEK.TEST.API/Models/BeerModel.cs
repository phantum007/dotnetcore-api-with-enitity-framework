using System;
using System.Collections.Generic;

namespace MASTEK.TEST.API.Models;

public class BeerModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public double? PercentageAlcoholByVolume { get; set; }

    public int? BreweryId { get; set; }

}
