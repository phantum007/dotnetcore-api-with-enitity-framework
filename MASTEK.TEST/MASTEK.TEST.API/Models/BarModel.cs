using System;
using System.Collections.Generic;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.API.Models;

public partial class BarModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
}

public partial class BarWithBeerModel: BarModel
{
    public virtual IEnumerable<BeerModel> Beers { get; set; } = new List<BeerModel>();
}

public partial class BarBeersMappingModel
{
    public int BarId { get; set; }

    public int BeerId { get; set; }

    public bool? Isdeleted { get; set; }
}
