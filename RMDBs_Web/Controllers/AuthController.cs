using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RMDB_Utility;
using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;

namespace RMDBs_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("login")]

        public IActionResult Login() => View();
        [HttpGet("register")]
        public IActionResult Register() => View();

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.Login(model);
            if (response.IsSuccess)
            {
                HttpContext.Session.SetString("JWToken", response.Result.Token);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Credentials!";
            return View(model);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterationRequestDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.Register(model);
            if (response.IsSuccess)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Registration Failed!";
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(Class1.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }
    }
}
