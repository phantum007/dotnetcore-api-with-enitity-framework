using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.API.Models;

public partial class BreweryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Address cannot be empty")]
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


#region ResponseModel

public class BreweryResponseModel : ErrorResponse
{
    public BreweryModel breweryModel { get; set; }
}

public class BreweryListResponseModel : ErrorResponse
{
    public IEnumerable<BreweryModel> breweryModel { get; set; }
}

public class BreweryWithBeerResponseModel : ErrorResponse
{
    public BreweryWithBeerModel breweryWithBeerModel { get; set; }
}

public class BreweryWithBeerListResponseModel : ErrorResponse
{
    public IEnumerable<BreweryWithBeerModel> breweryWithBeerModel { get; set; }
}
#endregion