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
        IList<UserProjectResponse> IUserProjectRepository.GetUserProject()
        {

            var upList = (from up in _Context.UserProjects
                          join u in _Context.Users on up.Id equals u.Id
                          join p in _Context.Projects on up.Projectid equals p.Id
                          join upc in _Context.Users on up.Pcid equals upc.Id
                          join upm in _Context.Users on up.Pmid equals upm.Id

                          select new
                          {
                              UserName = u.Name,
                              ProjectName = p.Name,
                              CordinatorName = upc.Name,
                              ManagerName = upm.Name,
                              up.Avalibiltty,
                              up.TotalBilling,
                              up.Status
                          }).ToList();
            IList < UserProjectResponse > data= new List<UserProjectResponse>();
            foreach (var item in upList)
            {
                data.Add(new UserProjectResponse() 
                { 
                    UserName = item.UserName ,ProjectName=item.ProjectName,CordinatorName=item.CordinatorName,
                ManagerName=item.ManagerName,Avalibiltty=item.Avalibiltty,
                    TotalBilling=item.TotalBilling,Status=Convert.ToInt32(item.Status)
                });
            }
          return data;
        }
    }
}
