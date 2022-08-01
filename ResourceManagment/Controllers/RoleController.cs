using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleRepository.GetRoles());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data retrinving Problem");
            }
        }
    }
}
