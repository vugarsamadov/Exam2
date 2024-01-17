using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Exam2.Web.Models.Auth
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
