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
        [HttpGet]
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
                        return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Greška pri unosu podataka, pokušajte ponovno!";
            return View(login);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);
            if (register.Keyword!="LCSarajevo>>LCTuzla")
            {
                TempData["Error"] = "Pogrešna ključna riječ, pokušajte ponovno!";
                return View(register);
            }
            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null)
            {
                TempData["Error"] = "Već postoji korisnik s unsenom email adresom!";
                return View(register);
            }

            var newUser = new User()
            {
                Email = register.Email,
                UserName = register.Email,
                Name = register.Name,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
