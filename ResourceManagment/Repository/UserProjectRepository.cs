using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        public UserProjectRepository(dbResourceMangamentSystemContext context)
        {
            _Context = context;
        }

        public async Task<UserProject> AddUserProject(UserProject userProject)
        {
            var data = await _Context.UserProjects.AddAsync(userProject);
            await _Context.SaveChangesAsync();
            return data.Entity;

        }

        public async Task<IEnumerable<UserProject>> GetUserProjectsid()
        {
            var list = await _Context.UserProjects.ToListAsync();
            return list;
        }

        public async Task<UserProject> UpdateUserProject(UserProject userProject)
        {
            var data = await _Context.UserProjects.FirstOrDefaultAsync(x => x.Id == userProject.Id);
            if (data != null)
            {
                data.UserId = userProject.UserId;
                data.Projectid = userProject.Projectid;
                data.Avalibiltty = userProject.Avalibiltty;
                data.TotalBilling = userProject.TotalBilling;
                data.Pcid = userProject.Pcid;
                data.Pmid = userProject.Pmid;
                data.Status = userProject.Status;
                await _Context.SaveChangesAsync();
            }
            return null;

        }

        IList<UserProjectResponse> IUserProjectRepository.GetUserProject(int Depid)
        {

            var upList = (from up in _Context.UserProjects
                          join u in _Context.Users on up.UserId equals u.Id
                          join p in _Context.Projects on up.Projectid equals p.Id
                          join upc in _Context.Users on up.Pcid equals upc.Id
                          join upm in _Context.Users on up.Pmid equals upm.Id
                          where u.Departmentid == Depid 

                          select new
                          {
                              id = up.Id,
                              UserName = u.Name,
                              userid = u.Id,
                              ProjectName = p.Name,
                              projectid = p.Id,
                              CordinatorName = upc.Name,
                              pcid = upc.Id,
                              ManagerName = upm.Name,
                              pmid = upm.Id,
                              up.Avalibiltty,
                              up.TotalBilling,
                              up.Status
                          }).ToList();
            IList<UserProjectResponse> data = new List<UserProjectResponse>();
            foreach (var item in upList)
            {
                data.Add(new UserProjectResponse()
                {
                    id = item.id,
                    UserName = item.UserName,
                    userid = item.userid,
                    ProjectName = item.ProjectName,
                    projectid = item.projectid,
                    CordinatorName = item.CordinatorName,
                    pcid = item.pcid,
                    ManagerName = item.ManagerName,
                    pmid = item.pmid,
                    Avalibiltty = item.Avalibiltty,
                    TotalBilling = item.TotalBilling
                });
            }
            return data;
        }
    }
}
