using BookMvcApp.Models;
using BookMvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookMvcApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(Login), "Home");
                }

                ModelState.AddModelError(string.Empty, "Registration failed");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _authService.LoginAsync(model);
                if (token != null)
                {
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Books");
                }

                ModelState.AddModelError(string.Empty, "Login failed");
            }

            return View(model);
        }

        [HttpPost]
        [Route("Auth/Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (result)
            {
                HttpContext.Session.Remove("JWToken");
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Books");
        }
    }
}