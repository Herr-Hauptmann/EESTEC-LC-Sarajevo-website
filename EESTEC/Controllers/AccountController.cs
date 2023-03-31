using EESTEC.Data;
using EESTEC.Models;
using EESTEC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EESTEC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<User> userMannager, SignInManager<User> signInManager, AppDbContext appDbContext)
        {
            _userManager = userMannager;
            _signInManager = signInManager;
            _context = appDbContext;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
       [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                bool passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                        RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Greška pri unosu podataka, pokušajte ponovno!";
            return View(login);
        }
        
    }
}
