using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using RestaurantManagement.Application.Provider;

namespace RestaurantManagement.Persistence.Provider
{
    public class MsSQLProvider : IDbProvider
    {
        public SqlConnection _connection = new SqlConnection("server=UFUK;database=ManagementDB;integrated security=true;TrustServerCertificate=True;");
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
        public DataTable GetData(string query)
        {
            try
            {
                OpenConnection();
                using (_sqlCommand = new SqlCommand())
                {
                    _sqlCommand.CommandText = query;

                    _sqlCommand.CommandType = CommandType.Text;
                    _sqlCommand.Connection = _connection;
                    using (_sqlDataAdapter = new SqlDataAdapter(_sqlCommand))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            _sqlDataAdapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return dt;
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }

        }
        ~MsSQLProvider() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqlCommand.Dispose();
                _sqlCommand = null;
                _sqlDataAdapter.Dispose();
                _sqlDataAdapter = null;
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
