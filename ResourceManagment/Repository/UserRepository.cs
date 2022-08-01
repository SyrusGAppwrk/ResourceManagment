using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

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
                users.ContactNo=user.ContactNo;
                users.Skills=user.Skills;
                users.Experience=user.Experience;   
                await _Context.SaveChangesAsync();
            }
            return null;

        }

        IList<UserProfile> IUserRepository.GetUserProfiles(int id,int roleid)
        {
            var userbyId = (from u in _Context.Users
                            join d in _Context.Departments on u.Departmentid equals d.Id 
                            join r in _Context.Roles on u.RoleId equals r.Id
                            where u.Id == id || u.RoleId==roleid
                            select new
                            {
                                userid = u.Id,
                                UserName = u.Name,
                                Email = u.Email,
                                Department = d.Name,
                                Experience = u.Experience,
                                Skills = u.Skills,
                                Contact = u.ContactNo,
                                Role = r.Name,
                                Roleid=u.RoleId,
                                Departmentid=u.Departmentid, 
                                status=u.Status,

                            }).ToList();
        IList<UserProfile> data = new List<UserProfile>();
            foreach (var item in userbyId)
            {
                data.Add(new UserProfile()
                 {
                    id=item.userid,
                    Name=item.UserName,
                    Email=item.Email,
                    Department=item.Department,
                    Experience=item.Experience,
                    Skills=item.Skills,
                    ContactNo=item.Contact,
                    Role=item.Role,
                    Roleid=item.Roleid,
                    Departmentid=item.Departmentid,
                    Status=item.status
                });
            }
            return data;
        }
    }
}
