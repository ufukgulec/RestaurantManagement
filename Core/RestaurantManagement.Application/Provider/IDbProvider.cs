using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Provider
{
    public interface IDbProvider
    {
        void OpenConnection();
        void CloseConnection();
        DataTable GetData(string query);
        void Dispose();
    }
}
