using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Shared.CustomExceptions
{
    public class ApiException : Exception
    {
        public ApiException(string Message) : base(Message)
        {

        }
        public ApiException(string Message, Exception InnerException) : base(Message, InnerException)
        {

        }
    }
}
