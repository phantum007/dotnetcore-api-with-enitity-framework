using System.Collections.Generic;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.API.Models;

public partial class BreweryModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
}

public partial class BreweryWithBeerModel: BreweryModel
{
    public virtual IEnumerable<BeerModel> Beers { get; set; } = new List<BeerModel>();
}


public partial class BreweryBeerMappingModel
{
    public int BreweryId { get; set; }

    public int BeerId { get; set; }

    public bool? Isdeleted { get; set; }
}