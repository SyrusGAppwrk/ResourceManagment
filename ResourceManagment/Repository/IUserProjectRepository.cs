using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public interface IUserProjectRepository
    {
       public IList<UserProjectResponse> GetUserProject();

    }
}
