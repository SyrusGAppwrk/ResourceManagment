using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public interface IProjectRepository
    {
        public IList<Project> GetProjects();
        Task<Project> GetProjects(int id);
        Task<Project> AddProjects(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(int id);
    }
}
