using System.ComponentModel.DataAnnotations;

namespace MASTEK.INTERVIEW.API.Models;

public partial class BarModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name cannot be empty")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Address cannot be empty")]
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

    //public bool? Isdeleted { get; set; }
}

#region ResponseModel

public class BarResponseModel : ErrorResponse
{
    public BarModel barModel { get; set; }
}

public class BarListResponseModel : ErrorResponse
{
    public IEnumerable<BarModel> barsModel { get; set; }
}

public class BarWithBeerResponseModel : ErrorResponse
{
    public BarWithBeerModel barWithBeerModel { get; set; }
}

public class BarWithBeerListResponseModel : ErrorResponse
{
    public IEnumerable<BarWithBeerModel> barWithBeerModel { get; set; }
}
#endregion