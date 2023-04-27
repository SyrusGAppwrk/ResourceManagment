using ResourceManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMangerTest.MockData
{
    public class DepartmentListMockData
    {
        public static List<Department> GetDepartment()
        {
            return new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "Development Team",
                    CreatedDate = DateTime.Now,
                    Status = 1,
                    Users={}
                },
                new Department
                {
                    Id = 2,
                    Name = "Desgin Team",
                    CreatedDate = DateTime.Now,
                    Status = 1,
                   Users={}
                }
            };
        }
    }
}
