using System;
namespace MASTEK.INTERVIEW.API.ExceptionClaases
{
	public class InvalidInputExceptions : Exception
    {
        public InvalidInputExceptions(string message)
        : base(message)
        {
        }
    }
}

