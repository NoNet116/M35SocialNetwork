using App.Data.Models;
using AutoMapper;
using M35SocialNetwork.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace M35SocialNetwork.Controllers
{

    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountManagerController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /* [Route("Login")]
         [HttpGet]
         public IActionResult Login()
         {
             return View("Home/Login");
         }*/

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userModel = _mapper.Map<User>(model);
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Account", "AccountManager");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
           


            return View("Views/Home/Index.cshtml");
        }


        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("Account")]
        [HttpGet]
        public async Task<IActionResult> Account()
        {
             var user = User;

             var result = await _userManager.GetUserAsync(user);

            var model = new AccountViewModel(result);

            /*model.Friends = await GetAllFriend(model.User);*/

            return View("Views/AccountManager/index.cshtml", model);
        }
    }
}
