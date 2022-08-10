using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Repository
{
    public class AssignTaskRepository:IAssignTaskRepository
    {
        private readonly appwrkco_msContext _Context;
        public AssignTaskRepository(appwrkco_msContext context)
        {
            _Context = context;
        }

        public IList<AssignTaskResponse>GetAssginTask(int Depid)
        {

            var assignlist = (from at in _Context.AssignTasks
                              join u in _Context.Users on at.Empid equals u.Id
                              join p in _Context.Projects on at.ProjectId equals p.Id
                              join upc in _Context.Users on at.Pcid equals upc.Id
                              join upm in _Context.Users on at.Pmid equals upm.Id
                              where u.Departmentid == Depid
                              orderby -at.CreatedDate.Year
                              orderby -at.CreatedDate.Month
                              orderby -at.CreatedDate.Day

                              select new
                              {
                                  id = at.Id,
                                  EmpName = u.Name,
                                  ProjectName = p.Name,
                                  Coodinator = upc.Name,
                                  Manger = upm.Name,
                                  Billable=at.Billable,
                                  CreatedDdate = Convert.ToDateTime(at.CreatedDate).ToString("yyyy/MM/dd"),
                                  status = at.Status,
                              }).ToList();
            IList<AssignTaskResponse> data = new List<AssignTaskResponse>();
            foreach (var Item in assignlist)
            {
                data.Add(new AssignTaskResponse()
                {
                    id=Item.id,
                    EmpName=Item.EmpName,
                    ProjectName=Item.ProjectName,
                    Coordinator=Item.Coodinator,
                    Manger=Item.Manger,
                    Billable=Item.Billable,
                    CreatedDate =Item.CreatedDdate,
                    status=Item.status
                });
            }
            return data;
        }

        public  async Task<AssignTask> PostAssignTask(AssignTask assign)
        {
            var users =  _Context.AssignTasks.FirstOrDefault(x => x.Empid == assign.Empid && x.ProjectId==assign.ProjectId);
            if (users == null)
            {
                var emp = _Context.AssignTasks.Add(assign);
                await _Context.SaveChangesAsync();
                return emp.Entity;
            }
            else
            {
                throw new ApplicationException("Data Already Exists");
            }
        }

        public async Task<AssignTask> UpdateAssignTask(AssignTask assign)
        {
            var users = _Context.AssignTasks.FirstOrDefault(x => x.Id == assign.Id);
            if (users != null)
            {
                users.Status=assign.Status;
                await _Context.SaveChangesAsync();
            }
            return null;

        }
    }

}
