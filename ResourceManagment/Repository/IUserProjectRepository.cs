using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public interface IUserProjectRepository
    {
       public IList<UserProjectResponse> GetUserProject(int Depid);
       public IList<UserProjectResponse> GetUserProjectbyDate(DateTime Sdate,DateTime endate);
        public Task<IEnumerable<UserProject>> GetUserProjectsid();
        public Task<UserProject> AddUserProject(UserProject userProject);
        public Task<UserProject> UpdateUserProject(UserProject userProject);

    }
}
