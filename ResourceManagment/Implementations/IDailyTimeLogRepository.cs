using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Implementations
{
    public interface IDailyTimeLogRepository
    {
        public IList<DailyTimeLogResponse> GetDailyTimeLog(int Depid);
        public IList<DailyTimeLogResponse> GetDailyLogByDate(int Depid,DateTime Srtdate,DateTime enddate);
        public Task<DailyTaskLog> PostDailyLog(DailyTaskLog log);
        public Task<DailyTaskLog> PutDailyLog(DailyTaskLog log);

    }
}
