using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Exam2.Web.Models.Auth
{
    public class RegisterVM
    {
        [EmailAddress]
        public string Email{ get; set; }
        [PasswordPropertyText]
        public string Password{ get; set; }
        [PasswordPropertyText]
        [Compare(nameof(Password))]
        public string RePassword{ get; set; }
    }
}
