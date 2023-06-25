using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MASTEK.INTERVIEW.API.Models;

public class BeerModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name cannot be empty")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "PercentageAlcoholByVolume cannot be empty")]
    public double? PercentageAlcoholByVolume { get; set; }

}

#region BeerResponseModel

public class BeerResponseModel : ErrorResponse
{
    public BeerModel beerModel { get; set; }
}

public class BeerListResponseModel : ErrorResponse
{
    public IEnumerable<BeerModel> beersModel { get; set; }
}


#endregion