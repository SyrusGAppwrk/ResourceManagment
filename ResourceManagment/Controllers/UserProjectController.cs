using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private readonly IUserProjectRepository _userProjectRepository;
        public UserProjectController(IUserProjectRepository userProjectRepository)
        {
            _userProjectRepository = userProjectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<UserProject>> GetUserProject()
        {
            try
            {
                var data = _userProjectRepository.GetUserProject();
                return Ok(data);

            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
    }
}
