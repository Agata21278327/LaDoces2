using LaDoces2.Models;
using LaDoces2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LaDoces2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAcount> _userManager;
        private readonly SignInManager<UserAcount> _signManager;
        public AccountController(UserManager<UserAcount> userManager,
        SignInManager<UserAcount> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var result = await _signManager.PasswordSignInAsync(user.UserName,

                loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(loginVM.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "Falha ao fazer login");
            return View(loginVM);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroViewModel registroVm)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAcount
                {
                    UserName = registroVm.UserName,
                    Nome = registroVm.Nome,
                    Endereco = registroVm.Endereco,
                    Numero = registroVm.Numero,
                    Bairro = registroVm.Bairro,
                    Cidade = registroVm.Cidade,
                    Estado = registroVm.Estado,
                    Cep = registroVm.Cep
                };
                var result = await _userManager.CreateAsync(user, registroVm.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao tentar registrar o usuario");
                }
            }
            return View(registroVm);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}