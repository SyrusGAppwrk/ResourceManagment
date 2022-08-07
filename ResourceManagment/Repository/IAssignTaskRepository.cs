using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public interface IAssignTaskRepository
    {
        public IList<AssignTaskResponse> GetAssginTask(int Depid);
        public Task<AssignTask> PostAssignTask(AssignTask assign);
        public Task<AssignTask> UpdateAssignTask(AssignTask assign);
    }
}
