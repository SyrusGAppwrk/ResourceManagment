using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Implementations;
using ResourceManagment.Models;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class DailyTimeLogController : ControllerBase
    {
        private readonly IDailyTimeLogRepository _dailyTimeLogRepository;
        public DailyTimeLogController(IDailyTimeLogRepository dailyTimeLogRepository)
        {
            _dailyTimeLogRepository = dailyTimeLogRepository;
        }

        [HttpGet("{Depid:int}")]
        public async Task<ActionResult<DailyTimeLogResponse>> GetDailyTimeLog(int Depid)
        {
            try
            {
                var data = _dailyTimeLogRepository.GetDailyTimeLog(Depid);
                return Ok(data);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpGet]
        [Route("GetDailyLogByDate/{Depid}/{Srtdate}/{endate}")]
        public async Task<ActionResult<DailyTimeLogResponse>> GetDailyLogByDate(int Depid,DateTime Srtdate,DateTime endate)
        {
            try
            {
                var data = _dailyTimeLogRepository.GetDailyLogByDate(Depid, Srtdate, endate);
                return Ok(data);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpPost]
        [Route("PostDailyLog")]
        public async Task<ActionResult> PostDailyLog(DailyTaskLog dailyTask )
            {
            try
            {
                if (dailyTask == null)
                {
                    return BadRequest();
                }
                var Createdtask = await _dailyTimeLogRepository.PostDailyLog(dailyTask);
                   var data= CreatedAtAction(nameof(GetDailyTimeLog), new { id = Createdtask.Id }, Createdtask);
                return Ok(data);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ("Record Already Exists"));
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DailyTaskLog>> PutDailyLog(int id, DailyTaskLog taskLog)
        {
            try
            {
                if (taskLog.Id != id)
                {
                    return BadRequest();

                }
                return await _dailyTimeLogRepository.PutDailyLog(taskLog);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Can't Update !"));
            }
        }
    }
}
