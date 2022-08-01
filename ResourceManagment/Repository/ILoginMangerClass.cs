using Microsoft.IdentityModel.Tokens;
using ResourceManagment.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResourceManagment.Repository
{
    public class ILoginMangerClass : ILoginManger
    {
        private dbResourceMangamentSystemContext _Context;
        private readonly IConfiguration _configuration;
        public ILoginMangerClass(dbResourceMangamentSystemContext context,IConfiguration configuration)
        {
            _Context = context;
            _configuration = configuration;
        }

        public LoginresponseModal validateUser(User user)
        {
            // Check User Creds --validate user
            var result = _Context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password==user.Password);
            var returnedToken = "";
            //LoginResponse 
            LoginresponseModal obj = new LoginresponseModal();
            if (result == null)
            {
                return obj;
            }
            // Token Generated If User is valid 

                var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JwtConfig:ValidIssuer"],
                        audience: _configuration["JwtConfig:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                        );

            // Generated Toekn will be returend 
                returnedToken = new JwtSecurityTokenHandler().WriteToken(token);
            
            obj.Message = "";
            obj.ReturenedToken = returnedToken;
            obj.User = result;
            return obj;
        }
    }
}
