using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        public UserRepository(dbResourceMangamentSystemContext context)
        {
            _Context = context;
        }

        public async Task<User> AddUser(User user)
        {
            var users = await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
            return users.Entity;
        }

        public async Task<User> DeleteUser(int id)
        {
            var users = await _Context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (users != null)
            {
                 _Context.Users.Remove(users);
                await _Context.SaveChangesAsync();
                return users;
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetUserDepartment(int Depid)
        {
            var uses=await _Context.Users.Where(x=>x.Departmentid==Depid).ToListAsync();
            return uses;
        }

        public async Task<IEnumerable<User>> GetUserRole(int Roleid)
        {
            var uses = await _Context.Users.Where(x => x.RoleId == Roleid).ToListAsync();
            return uses;
        }

        public async Task<User> GetUsers(int id)
        {
            var users = await _Context.Users.FirstOrDefaultAsync(x => x.Id == id);
                return users;
            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _Context.Users.ToListAsync();
            return users;
        }

        public async Task<User> UpdateUser(User user)
        {
            var users = await _Context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (user != null)
            {
                users.Name=user.Name;
                users.Email=user.Email;
                users.Password=user.Password;
                users.Status=user.Status;
                users.RoleId=user.RoleId;
                users.Departmentid = user.Departmentid;
                await _Context.SaveChangesAsync();
            }
            return null;

        }
    }
}
