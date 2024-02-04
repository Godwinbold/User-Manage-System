using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement_CodeWithSL.Models.Entities;
using UserManagement_CodeWithSL.Models.ViewModel;
using UserManagement_CodeWithSL.Services;
using UserManagement_CodeWithSL.Services.Iinterface;

namespace UserManagement_CodeWithSL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailService _emailService;
		public AccountController(UserManager<AppUser> userManager, IEmailService emailService,
			SignInManager<AppUser> signInManager) {
		
			_userManager = userManager;
			_emailService = emailService;
            _signInManager = signInManager;

        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Register(UserToRegisterViewModel model, string returnUrl)
		{
			if(ModelState.IsValid)
			{
				var user = new AppUser
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					PhoneNumber = model.PhoneNumber,
					UserName = model.Email
				};

				var createUserResult = await _userManager.CreateAsync(user, model.Password);
				if (createUserResult.Succeeded)
				{
					//add roles to newly created user
					var AddRoleResult = await _userManager.AddToRoleAsync(user, "regular");
					if (AddRoleResult.Succeeded)
					{
						//send email confirmation link
						var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						var link = Url.Action("ConfirmEmail", "Action", new { user.Email, token, Request.Scheme} );
						var body = @$"Hi{user.FirstName}{ user.LastName},
						please, click the link <a href='{link}'>here</a> to confirm your account's email";

						await _emailService.sendEmailAsync(user.Email, "confirm email", body );

						return RedirectToAction("RegisterCongrats", "Account", new {name = user.FirstName});

                    }              

                    {
                        foreach (var error in AddRoleResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        } 
                    }

                }

				{
					foreach (var error in createUserResult.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult RegisterCongrats( string name)
		{
			ViewBag.Name = name;
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmEmail(string Email, string token)
		{
			var user = await _userManager.FindByEmailAsync(Email);
			if (user != null)
			{
				var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);
				if(confirmEmailResult.Succeeded)
				{
					await  _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				foreach(var error in confirmEmailResult.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}

				return View(ModelState);
            }
            ModelState.AddModelError("", "Email confirmation failed");
            return View(ModelState);

        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
			ViewBag.ReturnUrl = returnUrl;
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(UserToLoginViewModel model, string? returnUrl)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					if (await _userManager.IsEmailConfirmedAsync(user))
					{
						var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (loginResult.Succeeded)
                        {
                            if (string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return LocalRedirect(returnUrl);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invaid credentials");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email not confirmed yet!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invaid credentials");
                }
            }
            return View(model);

        }

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
			if (!ModelState.IsValid)
			{
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //send email forgot password
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("Reset Password", "Action", new { user.Email, token, Request.Scheme });
                    var body = @$"Hi{user.FirstName}{user.LastName},
						please, click the link <a href='{link}'>here</a> to reset your password";

                    await _emailService.sendEmailAsync(user.Email, "Forgot Password", body);

					ViewBag.Message = "Reset password has been sent to your email";
					return View();
                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);

        }
		[HttpGet]
		public IActionResult PasswordReset( string email, string token)
		{
			var resetPasswordModel = new ResetPasswordViewModel{ Email = email, Token = token};

            return View(resetPasswordModel);
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
					if (resetPasswordResult.Succeeded)
					{
						return RedirectToAction("Index", "Account");
					}
					else
					{
						foreach(var error  in resetPasswordResult.Errors)
						{
							ModelState.AddModelError(error.Code, error.Description);
						}
						return View(model) ;
					}
                }
				ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}

	}

}
