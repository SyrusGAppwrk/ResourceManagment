using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUsers(int id);
        public IList<UserProfile>GetUserProfiles(int id,int roleid);
        public Task<IEnumerable<User>> GetUserRole(int Roleid);
        public Task<IEnumerable<User>>GetUserDepartment(int Depid);
        public Task<User> AddUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(int id);
           
    }
}
