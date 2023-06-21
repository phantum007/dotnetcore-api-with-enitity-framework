using System;
using System.Collections.Generic;

namespace MASTEK.TEST.ENTITY;

public partial class Beer
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }

    public DateTime? CreateTime { get; set; }

    public string? Name { get; set; }

    public double? PercentageAlcoholByVolume { get; set; }

    public int? BreweryId { get; set; }

    public virtual Brewery? Brewery { get; set; }
}
