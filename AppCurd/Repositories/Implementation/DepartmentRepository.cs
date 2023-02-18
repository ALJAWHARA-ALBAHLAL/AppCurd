using AppCurd.Models;
using AppCurd.Repositories.Contract;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AppCurd.Repositories.Implementation
{
    public class DepartmentRepository : IGenericRepository<Departments>
    {

        private readonly IConfiguration _configuration;

        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async  Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Departments entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Departments>> GetList()
        {
            List<Departments> _List = new List<Departments>();

            using (var connect = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("ps_DepartmentList", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        _List.Add(new Departments()
                        {
                            DepID = Convert.ToInt32(reader["DepID"]),
                            Name = reader["Name"].ToString()
                         });
                    }
                }
            }
            return _List;   
        }

        public async Task<bool> Save(Departments entity)
        {
            throw new NotImplementedException();
        }
    }
}
