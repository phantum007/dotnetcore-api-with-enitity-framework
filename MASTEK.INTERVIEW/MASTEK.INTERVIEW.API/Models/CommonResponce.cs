using System;
namespace MASTEK.INTERVIEW.API.Models
{
    public class ErrorResponse
    {
        public Exception errorDetails { get; set; }
    }

    public class CreateUpdateResponseModel : ErrorResponse
    {
        public bool status { get; set; }
    }
}

