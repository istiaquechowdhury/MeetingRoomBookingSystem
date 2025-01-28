﻿ using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using MeetingRoomBooking.DataAccess.Identity;
using Microsoft.AspNetCore.Authorization;
using MeetingRoomBooking.Presentation.Models;
using MeetingRoomBooking.Domain;

namespace MeetingRoomBooking.Presentation.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<AccountController> _logger;
      //  private readonly IEmailUtility _emailUtility;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger
           // IEmailUtility emailUtility
           )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            //_emailUtility = emailUtility;

        }




        [AllowAnonymous]

        public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = new RegistrationModel();
            model.ReturnUrl = returnUrl;
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegistrationModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Action("ConfirmEmail",
                    //    "Account",
                    //    values: new { area = "", userId = userId, code = code, returnUrl = model.ReturnUrl },
                    //    protocol: Request.Scheme
                    //);
                                            var callbackUrl = Url.Action(
                            "ConfirmEmail", // Action name
                            "Account",      // Controller name
                            new { userId = userId, code = code }, // Route values
                            protocol: Request.Scheme);

                    //_emailUtility.SendEmail(
                    //            model.Email,
                    //            model.Email,
                    //            "Confirm your Email",
                    //            $"<p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p>",
                    //            isHtml: true
                    //);
                    //_emailUtility.SendEmail(model.Email, model.Email, "Confirm your Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(model.ReturnUrl);

                        
                        return RedirectToAction("Login", "Account");
                    }

                }
                foreach (var error in result.Errors)
                {
                    //ModelState.AddModelError(string.Empty, error.Description);
                    if (error.Code == "DuplicateUserName") 
                    {
                        TempData["error"] = "Username is already taken.";
                    }
                    else if(error.Code == "DuplicateEmail")
                    {
                        TempData["error"] = "Email is already taken.";
                    }
                    else
                    {
                        TempData["error"] = error.Description;
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
           
            return View(model);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogInAsync(string returnUrl = null)
        {
            var model = new SignInModel();

            model.ReturnUrl = returnUrl == null ? Url.Content("~/") : returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> LogInAsync(SignInModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["error"] = "Email or Password is invalid";
                    return View(model);
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(user.UserName,model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {

                    return RedirectToAction("Lockout");
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    TempData["error"] = "Email or Password is invalid";
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
           
            returnUrl = Url.Action("Login", "Account");
            return LocalRedirect(returnUrl);
        }
   
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);
            if (result.Succeeded)
            {
                return View("ConfirmEmail"); // Display confirmation success page
            }
            else
            {
                return View("Error"); // Display an error page
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return Json(false); // Email is already taken
            }
            return Json(true); // Email is available
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirmation(string email, string returnUrl = null)
        {
            ViewData["Email"] = email;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
      

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return Json(false); // Username is already taken
            }
            return Json(true); // Username is available
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
