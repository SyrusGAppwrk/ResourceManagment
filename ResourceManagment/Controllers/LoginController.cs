using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [ApiController]
    [Route("api/L1")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManger _loginManger;
        public LoginController( ILoginManger loginManger)
        {
            _loginManger = loginManger;
        }

        // Login Existing User
        [HttpPost]
        [Route("Emp/login")]
        public IActionResult Login([FromBody] LoginModal existingUser)
        {
            // Check User State
            if (!ModelState.IsValid) 
             return BadRequest(existingUser);

            var uname = existingUser.Username;
            var pwd = existingUser.Password;

            // Map To User
            User user = new User();
            user.Email = uname;
            user.Password = pwd;

            LoginresponseModal res = new LoginresponseModal();
            res = _loginManger.validateUser(user);
            if (res.ReturenedToken == null || res.ReturenedToken == "")
            {
                res = new LoginresponseModal();
                res.Message = "Invalid Credentials";
            }
            return Ok(res);
        }
    }
}
