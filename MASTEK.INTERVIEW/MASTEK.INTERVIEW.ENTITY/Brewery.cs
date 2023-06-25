namespace MASTEK.INTERVIEW.ENTITY;

public partial class Brewery
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BreweryBeersMapping> BreweryBeersMappings { get; set; } = new List<BreweryBeersMapping>();
}
