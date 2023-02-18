using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;

namespace AppCurd.Models
{
    public class Employees
    {
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public Departments RefDepartmentID { get; set; }
    }
}

