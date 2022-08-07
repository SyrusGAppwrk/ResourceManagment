using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("api/A1")]
    [ApiController]
    public class AssignTaskController : ControllerBase
    {
        private readonly IAssignTaskRepository _assignTaskRepository;
        public AssignTaskController(IAssignTaskRepository assignTaskRepository)
        {
            _assignTaskRepository = assignTaskRepository;
        }

        [HttpGet("{d:int}")]
        public async Task<ActionResult> GetAssginTask(int d)
        {
            try
            {
                var emp =  _assignTaskRepository.GetAssginTask(d);
                return Ok(emp);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAssignTask(AssignTask assign)
        {
            try
            {
                if (assign == null)
                {
                    return BadRequest();
                }
                var Createdtask = await _assignTaskRepository.PostAssignTask(assign);
                return CreatedAtAction(nameof(GetAssginTask), new { id = Createdtask.Id }, Createdtask);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Record Already Exists"));
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AssignTask>> UpdateAssignTask(int id, AssignTask assgintask)
        {
            try
            {
                if (assgintask.Id != id)
                {
                    return BadRequest();

                }
                 await _assignTaskRepository.UpdateAssignTask(assgintask);
                return Ok("Updated");

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
