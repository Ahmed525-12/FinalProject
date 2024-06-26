﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThriftinessCore.Entites.Identity;
using WebApplication1.Helper;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSettings emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSettings = emailSettings;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    DisplayName = model.DisplayName,
                };
                var Result = await _userManager.CreateAsync(User, model.Password);
                await _signInManager.PasswordSignInAsync(User, model.Password, true, false);

                if (Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        /// <summary>
        /// /////////////////////////////// we here did a sign up ////
        /// </summary>
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult GoogleLogIn()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse"),
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var clamis = result.Principal.Identities.FirstOrDefault().Claims.Select(
                claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                }).ToList();
            var CheckUser = await _userManager.FindByEmailAsync(clamis.ElementAt(4).Value);

            if (CheckUser == null)
            {
                var User = new AppUser()
                {
                    UserName = clamis.ElementAt(4).Value.Split("@")[1],
                    Email = clamis.ElementAt(4).Value,
                    DisplayName = clamis.ElementAt(4).Value.Split("@")[0],
                };
                var createuser = await _userManager.CreateAsync(User);
                CheckUser = User;
                await _signInManager.SignInAsync(User, true, "Google");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                await _signInManager.SignInAsync(CheckUser, true, "Google");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM logInVM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(logInVM.Email);
                if (User != null)
                {
                    var flag = await _userManager.CheckPasswordAsync(User, logInVM.Password);

                    if (flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(User, logInVM.Password, true, false);

                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "password Not Found");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found");
                }
            }

            return View(logInVM);
        }

        /// <summary>
        /// /////////////////////////////// we here did a login ////
        /// </summary>
        ///
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Auth", new { email = user.Email, Token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = resetPasswordLink
                    };
                    _emailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckInbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email not Exist");
                }
            }
            return View("ForgetPassword", model);
        }

        public IActionResult CheckInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["token"] = token;
            TempData["email"] = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                else
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, errors.Description);
                    }
                }
            }
            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
       CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(LogIn));
        }
    }
}