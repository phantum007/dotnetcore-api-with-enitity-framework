namespace MASTEK.INTERVIEW.ENTITY;

public partial class Bar
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }

    //public DateTime? CreateTime { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<BarBeersMapping> BarBeersMappings { get; set; } = new List<BarBeersMapping>();
}
