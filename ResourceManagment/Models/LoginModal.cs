using System.ComponentModel.DataAnnotations;

namespace ResourceManagment.Models
{
    public class LoginModal
    {
        [Required, EmailAddress]
        public string Username { get; set; } = null;
        [Required]
        public string Password { get; set; } = null;
    }
    public class LoginresponseModal
    {
        public string ReturenedToken { get; set; } = null;
        public User User { get; set; } = null;
        public string Message { get; set; } = null;

    }
}
