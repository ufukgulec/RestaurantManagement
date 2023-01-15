using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    public interface IOpenAI
    {
        Task<string> CompletionAsync(string text);
        Task<List<string>> ImageGeneratorAsync(string text);
    }
}
