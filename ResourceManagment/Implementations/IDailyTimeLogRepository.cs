using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Implementations
{
    public interface IDailyTimeLogRepository
    {
        public IList<DailyTimeLogResponse> GetDailyTimeLog(int Depid);
        public Task<DailyTaskLog> PostDailyLog(DailyTaskLog log);
        public Task<DailyTaskLog> PutDailyLog(DailyTaskLog log);

    }
}
