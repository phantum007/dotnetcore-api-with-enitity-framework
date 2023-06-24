using System.Collections.Generic;

namespace MASTEK.TEST.ENTITY;

public partial class Brewery
{
    public int Id { get; set; }

    //public DateTime? CreateTime { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BreweryBeersMapping> BreweryBeersMappings { get; set; } = new List<BreweryBeersMapping>();
    //public virtual ICollection<Beer> Beers { get; set; } = new List<Beer>();
}
