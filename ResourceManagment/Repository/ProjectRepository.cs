using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagment.Models;
using Microsoft.AspNetCore.Http;

namespace ResourceManagment.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly dbResourceMangamentSystemContext _Context;
        private IWebHostEnvironment _appEnvironment;
        public ProjectRepository(dbResourceMangamentSystemContext context, IWebHostEnvironment appEnvironment)
        {
            _Context = context;
            _appEnvironment = appEnvironment;   
        }

        public async Task<Project> AddProjects( Project project)
        {
            var list = await _Context.AddAsync(project);
            await _Context.SaveChangesAsync();
            return list.Entity;
        }

       
        public async Task<Project> DeleteProject(int id)
        {
            var list = await _Context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (list != null)
            {
                _Context.Projects.Remove(list);
                await _Context.SaveChangesAsync();
                return list;
            }
            return null;

        }
        public async Task<Project> GetProjects(int id)
        {
            var list = await _Context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            return list;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var list = await _Context.Projects.FirstOrDefaultAsync(x => x.Id == project.Id);
            if (list != null)
            {
                list.Name = project.Name;
                list.Status = project.Status;
                list.ClientName = project.ClientName;
                list.Platformm = project.Platformm;
                list.Tech = project.Tech;
                list.Code = project.Code;
                list.Url = project.Url;
                list.Sdate = project.Sdate;
                list.Edate = project.Edate;
                await _Context.SaveChangesAsync();
            }
            return null;

        }

        IList<Project> IProjectRepository.GetProjects()
        {
            //var list = await _Context.Projects.ToListAsync();
            var list = (from p in _Context.Projects
                        select new
                        {
                            p.Id,
                            p.Name,
                            p.ClientName,
                            p.Platformm,
                            p.Tech,
                            p.Code,
                            p.Url,
                            sdate = Convert.ToDateTime(p.Sdate).ToString("yyyy-MM-dd"),
                            edate = Convert.ToDateTime(p.Edate).ToString("yyyy-MM-dd"),
                            p.Status
                        }).ToList();
            IList<Project>data=new List<Project>();
            foreach (var item in list)
            {
                data.Add(new Project()
                {
                    Id= item.Id,
                    Name= item.Name,
                    ClientName= item.ClientName,
                    Platformm= item.Platformm,
                    Tech= item.Tech,
                    Code= item.Code,
                    Url= item.Url,
                    SrtDate=item.sdate,
                    EndDate=item.edate,
                    Status= item.Status
                });
            }
            return data;
        }
    }
}
