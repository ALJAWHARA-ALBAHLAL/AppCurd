using AppCurd.Models;
using AppCurd.Repositories.Contract;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AppCurd.Repositories.Implementation
{
  
    public class EmployeeRepository : IGenericRepository<Employees>
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Delete(int id)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("ps_DeleteEmployees", connect);
                cmd.Parameters.AddWithValue("EmpID", id);
           
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                    return true;
                else
                    return false;

            }
        }

        public async Task<bool> Edit(Employees entity)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("ps_EditEmployees", connect);
                cmd.Parameters.AddWithValue("EmpID", entity.EmpID);
                cmd.Parameters.AddWithValue("FullName", entity.FullName);
                cmd.Parameters.AddWithValue("Gender", entity.Gender);
                cmd.Parameters.AddWithValue("Email", entity.Email);
                cmd.Parameters.AddWithValue("Salary", entity.Salary);
                cmd.Parameters.AddWithValue("RefDepartmentID", entity.RefDepartmentID.DepID);

                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0) 
                return true;
                else
                return false;

            } 
        }

        public async Task<List<Employees>> GetList()
        {
            List<Employees> _List = new List<Employees>();

            using (var connect = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("ps_EmployeeLists", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        _List.Add(new Employees()
                        {
                            EmpID=Convert.ToInt32(reader["EmpID"]),
                            FullName= reader["FullName"].ToString(),
                            Gender= reader["Gender"].ToString(),
                            Email= reader["Email"].ToString(),
                            Salary= Convert.ToInt32(reader["Salary"]),
                            RefDepartmentID= new Departments(){
                            DepID=Convert.ToInt32(reader["DepID"]),
                            Name= reader["Name"].ToString(),
                            }
                         });
                    }
                }
            }
            return _List;
        }

        public async Task<bool> Save(Employees entity)
        {
            using (var connect = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("ps_SaveEmployees", connect);
                cmd.Parameters.AddWithValue("FullName", entity.FullName);
                cmd.Parameters.AddWithValue("Gender", entity.Gender);
                cmd.Parameters.AddWithValue("Email", entity.Email);
                cmd.Parameters.AddWithValue("Salary", entity.Salary);
                cmd.Parameters.AddWithValue("RefDepartmentID", entity.RefDepartmentID.DepID);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                if (affectedRows > 0) 
                return true;
                else
                return false;

            }
        }



    }
}
