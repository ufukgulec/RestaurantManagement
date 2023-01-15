using RestaurantManagement.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application
{
    public interface IServiceOfWork : IDisposable
    {
        IOpenAI OpenAI { get; }
    }
}
