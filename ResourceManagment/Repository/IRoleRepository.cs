using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
       // Task<Department> GetDepartments(int id);
       // Task<Department> AddDepartments(Department department);
     //  Task<Department> UpdateDepartment(Department department);
       // Task<Department> DeleteDepartment(int id);

    }
}
