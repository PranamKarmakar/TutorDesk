using System;
using System.Collections.Generic;
using System.Text;

namespace TutorDesk.Common.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(string message, int statusCode = 400): base(message)
        {
            StatusCode = statusCode;
        }

    }
}

