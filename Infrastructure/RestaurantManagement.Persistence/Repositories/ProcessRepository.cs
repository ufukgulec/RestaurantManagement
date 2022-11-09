using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public class ProcessRepository : GenericRepository<Process>, IProcessRepository
    {

        public ProcessRepository(ManagementContext context) : base(context)
        {
        }
    }
}
