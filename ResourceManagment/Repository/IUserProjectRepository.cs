using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public interface IUserProjectRepository
    {
       public IList<UserProjectResponse> GetUserProject(int Depid);
        public Task<IEnumerable<UserProject>> GetUserProjectsid();
        public Task<UserProject> AddUserProject(UserProject userProject);
        public Task<UserProject> UpdateUserProject(UserProject userProject);

    }
}
