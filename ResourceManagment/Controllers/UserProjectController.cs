using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("Api/v2")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private readonly IUserProjectRepository _userProjectRepository;
        public UserProjectController(IUserProjectRepository userProjectRepository)
        {
            _userProjectRepository = userProjectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<UserProject>> GetUserProject(int Depid)
        {
            try
            {
                var data = _userProjectRepository.GetUserProject(Depid);
                return Ok(data);

            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }

        [HttpGet]
        [Route("GetUserProjectsid")]
        public async Task<ActionResult<UserProject>> GetUserProjectsid()
        {
            try
            {
                var data = await _userProjectRepository.GetUserProjectsid();
                return Ok(data);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddUserProject(UserProject userProject)
        {
            try
            {
                if (userProject == null) 
                {
                    return BadRequest();
                }
                var createduserproject = await _userProjectRepository.AddUserProject(userProject);
                return CreatedAtAction(nameof(GetUserProject), new { id = createduserproject.Id }, createduserproject)
;
                    
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserProject>> UpdateUserProject(int id ,UserProject userProject)
        {
            try
            {
                if (userProject.Id != id)
                {
                    return BadRequest();

                }
                return await _userProjectRepository.UpdateUserProject(userProject);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
