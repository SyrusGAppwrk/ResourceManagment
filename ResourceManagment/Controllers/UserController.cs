using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;
using ResourceManagment.ResponseModal;

namespace ResourceManagment.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not Found"));
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetUsers(int id)
        {
            try
            {
                var users = await _userRepository.GetUsers(id);
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not found"));
            }
        }

        [HttpGet]
        [Route("GetUserRole")]
            
        public async Task<ActionResult> GetUserRole(int Roleid)
        {
            try
            {
                var users = await _userRepository.GetUserRole(Roleid);
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);

            }
            catch (Exception)
            {

                throw;
            }    
        }
        [HttpGet]
        [Route("GetUserDepartment")]
        public async Task<ActionResult> GetUserDepartment(int Depid)
        {
            try
            {
                var users=await _userRepository.GetUserDepartment(Depid);
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _userRepository.AddUser(user);
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, CreatedUser);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not found"));
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id,User user)
        {
            try
            {
                if (user.Id != id)
                {
                    return NotFound("Id Not Found");
                }
                
                return await _userRepository.UpdateUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetUserProfiles")]
        public async Task<ActionResult> GetUserProfiles(int id)
        {
            try
            {
                var users =  _userRepository.GetUserProfiles(id);
                if (users == null)
                {
                    return BadRequest();
                }
                return Ok(users);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data Not found"));
            }
        }
    }
}
