using Exam2.Web.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam2.Web.Controllers
{
    public class AuthController : Controller
    {
        public AuthController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private SignInManager<IdentityUser> _signInManager { get; }
        private UserManager<IdentityUser> _userManager { get; }
        private RoleManager<IdentityRole> _roleManager { get; }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if(user == null)
            {
                ModelState.AddModelError("","Email or password invalid!");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(user,model.Password,false,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password invalid!");
                return View(model);
            }

            return RedirectToAction("Index", "Admin");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new IdentityUser(model.Email);
            user.Email = model.Email;
            var result =await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User != null)
                await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> InitRoles()
        {
            if (HttpContext.User != null)

                if(await _roleManager.RoleExistsAsync("Admin") != true)
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
            
            if(await _roleManager.RoleExistsAsync("Member") != true)
                    await _roleManager.CreateAsync(new IdentityRole("Member"));

            if (await _userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var user = new IdentityUser("admin") { Email = "admin@admin.com" };
                await _userManager.CreateAsync(user, "admin123");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
    
            return RedirectToAction(nameof(Login));
        }

    }
}
