﻿namespace MASTEK.INTERVIEW.ENTITY;

public partial class Beer
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }

    //public DateTime? CreateTime { get; set; }

    public string? Name { get; set; }

    public double? PercentageAlcoholByVolume { get; set; }

    public virtual ICollection<BarBeersMapping> BarBeersMappings { get; set; } = new List<BarBeersMapping>();

    public virtual ICollection<BreweryBeersMapping> BreweryBeersMappings { get; set; } = new List<BreweryBeersMapping>();
}
