using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        public DepartmentRepository(dbResourceMangamentSystemContext context)
        {
            _Context = context;
        }

        public async Task<Department> AddDepartments(Department department)
        {
           var result=await _Context.Departments.AddAsync(department);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var result = await _Context.Departments.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (result != null) 
            {
                _Context.Departments.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;

        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var result=await _Context.Departments.ToListAsync();
            return result;
        }

        public async Task<Department> GetDepartments(int id)
            {
            var result = await _Context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
            
        public async Task<Department> UpdateDepartment(Department department)
        {
            var result = await _Context.Departments.FirstOrDefaultAsync(a => a.Id == department.Id);
            if(result!=null)
            {
                result.Name = department.Name;
                result.Status = department.Status;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
