using ResourceManagment.Models;

namespace ResourceManagment.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        public RoleRepository(dbResourceMangamentSystemContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var result =  _Context.Roles.ToList();
            return result;
        }
    }
}
