using System;
namespace MASTEK.TEST.API.ExceptionClaases
{
	public class InvalidInputExceptions : Exception
    {
        public InvalidInputExceptions(string message)
        : base(message)
        {
        }
    }
}

