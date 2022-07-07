using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        public ProjectRepository(dbResourceMangamentSystemContext context)
        {
            _Context = context;
        }

        public async Task<Project> AddProjects(Project project)
        {
            var list = await _Context.AddAsync(project);
            await _Context.SaveChangesAsync();
            return list.Entity;
        }

        public async Task<Project> DeleteProject(int id)
        {
            var list = await _Context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (list != null)
            {
                _Context.Projects.Remove(list);
                await _Context.SaveChangesAsync();
                return list;
            }
            return null;

        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var list = await _Context.Projects.ToListAsync();
            return list;
        }

        public async Task<Project> GetProjects(int id)
        {
            var list = await _Context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            return list;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var list = await _Context.Projects.FirstOrDefaultAsync(x => x.Id == project.Id);
            if (list != null)
            {
                list.Name = project.Name;
                list.Status = project.Status;
                await _Context.SaveChangesAsync();
            }
            return null;

        }
    }
}
