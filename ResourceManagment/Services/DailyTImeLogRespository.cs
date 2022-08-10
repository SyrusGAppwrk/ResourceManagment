using ResourceManagment.Implementations;
using ResourceManagment.Models;
using ResourceManagment.ResponseModal;
using System.Globalization;

namespace ResourceManagment.Services
{
    public class DailyTImeLogRespository: IDailyTimeLogRepository
    {
        private readonly appwrkco_msContext _Context;

        public DailyTImeLogRespository(appwrkco_msContext context)
        {
            _Context = context;
        }

        public  IList<DailyTimeLogResponse> GetDailyLogByDate(int Depid, DateTime Srtdate, DateTime enddate)
        {
            var log = (from dl in _Context.DailyTaskLogs 
                       join att in _Context.AssignTasks on dl.AssignTaskId equals att.Id
                       join u in _Context.Users on att.Empid equals u.Id
                       join p in _Context.Projects on att.ProjectId equals p.Id
                       join upc in _Context.Users on att.Pcid equals upc.Id
                       join upm in _Context.Users on att.Pmid equals upm.Id
                       where att.Status==1 && u.Departmentid == Depid && (dl.CreatedDate.Date>=Srtdate.Date  && dl.CreatedDate.Date<= enddate.Date)
                       orderby -dl.CreatedDate.Year
                       orderby -dl.CreatedDate.Month
                       orderby -dl.CreatedDate.Day

                       select new
                       {
                           LogId = dl.Id,
                           AssignId = att.Id,
                           Empname = u.Name,
                           ProjectName = p.Name,
                           Coordinator = upc.Name,
                           Manger = upm.Name,
                           Avalibiltty = dl.Avalibiltty ,
                           BilligHour = dl.BillingHour,
                           Billable = att.Billable,
                           CreateDate = Convert.ToDateTime(dl.CreatedDate).ToString("MM/dd/yyyy"),
                           status = dl.Status,
                           Comments = dl.Comments

                       }).ToList();
            IList<DailyTimeLogResponse> data = new List<DailyTimeLogResponse>();
            foreach (var d in log)
            {
                data.Add(new DailyTimeLogResponse()
                {
                    AssignId = d.AssignId,
                    LogId = d.LogId,
                    EmpName = d.Empname,
                    ProjectName = d.ProjectName,
                    Coordinator = d.Coordinator,
                    Manager = d.Manger,
                    Avalibiltty = d.Avalibiltty,
                    BillingHour = d.BilligHour,
                    createddate = d.CreateDate,
                    status = d.status,
                    Bilable = d.Billable,
                    Comments = d.Comments

                });
            }
            return data;
        }

        public IList<DailyTimeLogResponse> GetDailyTimeLog(int Depid)
        {
            var log = (from att in _Context.AssignTasks
                      join u in _Context.Users on att.Empid equals u.Id
                      join p in _Context.Projects on att.ProjectId equals p.Id
                      join upc in _Context.Users on att.Pcid equals upc.Id
                      join upm in _Context.Users on att.Pmid equals upm.Id
                      join dtt in _Context.DailyTaskLogs on att.Id equals dtt.AssignTaskId into eGroup
                      from dtt in (from dl in eGroup where dl.CreatedDate.Date == DateTime.Now.Date select dl).DefaultIfEmpty()
                      where att.Status == 1 && u.Departmentid == Depid
                      select new
                      {
                          LogId = dtt.Id!=null?dtt.Id:0,
                          AssignId=att.Id,
                          Empname = u.Name,
                          ProjectName=p.Name,
                          Coordinator=upc.Name,
                          Manger=upm.Name,
                          Avalibiltty = dtt.Avalibiltty!=null? dtt.Avalibiltty:" ",
                          BilligHour = dtt.BillingHour!=null? dtt.BillingHour:0,
                          Billable=att.Billable,
                          CreateDate = Convert.ToDateTime(dtt.CreatedDate != null ? dtt.CreatedDate : null).ToString("yyyy-MM-dd"),
                          status=dtt.Status!=null?dtt.Status:null,
                          Comments=dtt.Comments!=null?dtt.Comments:" "

                      }).ToList();
            IList<DailyTimeLogResponse> data = new List<DailyTimeLogResponse>();
            foreach (var d in log)
            {
                data.Add(new DailyTimeLogResponse()
                {
                    AssignId = d.AssignId,
                    LogId = d.LogId,
                    EmpName = d.Empname,
                    ProjectName=d.ProjectName,
                    Coordinator=d.Coordinator,
                    Manager=d.Manger,
                    Avalibiltty = d.Avalibiltty,
                    BillingHour = d.BilligHour,
                    createddate=d.CreateDate,
                    status=d.status,
                    Bilable=d.Billable,
                    Comments=d.Comments
                    
                });
            }
            return data;
        }

        public async Task<DailyTaskLog> PostDailyLog(DailyTaskLog log)
        {
            var user = _Context.DailyTaskLogs.FirstOrDefault(x => x.AssignTaskId == log.AssignTaskId && x.CreatedDate.Date == DateTime.Now.Date);
            if (user == null)
            {
                var dailylog = _Context.Add(log);
                await _Context.SaveChangesAsync();
                return dailylog.Entity;
            }
            else
            {    
            throw new NotImplementedException("Already Exist for Today");
            }
        }

        public async Task<DailyTaskLog> PutDailyLog(DailyTaskLog log)
        {
            var checkdate = _Context.DailyTaskLogs.FirstOrDefault(x =>x.Id==log.Id);
            var totalday = (DateTime.Now.Date-checkdate.CreatedDate.Date).Days  ;
            if (totalday <= 2)
            {
                checkdate.AssignTaskId = log.AssignTaskId;
                checkdate.Avalibiltty=log.Avalibiltty;
                checkdate.BillingHour=log.BillingHour;
                checkdate.Comments = log.Comments;
                await _Context.SaveChangesAsync();
            }
            else 
            { 
                 throw new NotImplementedException("Can't Update");
            }
            return null;

        }
    }
}
