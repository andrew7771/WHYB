using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;
using WHYB.BLL.Interfaces;
using WHYB.WEB.Models;

namespace WHYB.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
           if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await _userService.Authenticate(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    _userService.AuthenticationManager.SignOut();
                    _userService.AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            _userService.AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
          if (ModelState.IsValid)
            {
                //await SetInitialDataAsync();
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await _userService.Create(userDto);
                
                if (operationDetails.Succedeed)
                {
                    ClaimsIdentity claim = await _userService.Authenticate(userDto);
                    if (claim != null)
                    {
                        _userService.AuthenticationManager.SignOut();
                        _userService.AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true}, claim);

                        string userId =  await _userService.FindUserIdByEmailAsync(userDto.Email);

                        return RedirectToAction("CompleteRegistration", "Account", new {userid = userId});
                    }

                    return View("Error");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }


        public async Task<ActionResult> CompleteRegistration(string userId)
        {
            string code = await _userService.GenerateEmailConfirmationTokenAsync(userId);

            if (Request.Url != null)
            {
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = userId, code = code}, protocol: Request.Url.Scheme);

                await _userService.SendEmailasync(userId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }
            return View();
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await _userService.ConfirmEmailAsync(userId, code);
            if (result.Succedeed)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Горбунков",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}