using Blogger.Business.Services.Abstracts;
using Blogger.Core.Models;
using Blogger.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
		{
			_userManager = userManager;
			_signManager = signManager;
		}

		private readonly SignInManager<AppUser> _signManager;

		public IActionResult Register() 
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(MemberLoginVm memberLoginVm)
		{
			var user = await _userManager.FindByNameAsync(memberLoginVm.Username);

			if(user == null)
			{
				ModelState.AddModelError("", "Username or password is invalid");
				return View();
			}

			var result = await _signManager.PasswordSignInAsync(user, memberLoginVm.Password, false, false);

			if(!result.Succeeded)
			{
				ModelState.AddModelError("", "Username or password is invalid");
				return View();
			}

			return RedirectToAction("Index", "Home");

		}

		[HttpPost]
		public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVm)
		{
			var user = await _userManager.FindByNameAsync(memberRegisterVm.Username);

			if (user != null)
			{
				ModelState.AddModelError("Username", "Username already exist");
				return View();
			}

			user = await _userManager.FindByEmailAsync(memberRegisterVm.Email);

			if(user != null)
			{
				ModelState.AddModelError("Email", "Email already exist");
				return View();
			}

			user = new AppUser
			{
				UserName = memberRegisterVm.Username,	
				FullName = memberRegisterVm.Fullname,
				Email = memberRegisterVm.Email
			};

			var result = await _userManager.CreateAsync(user, memberRegisterVm.Password);

			if(!result.Succeeded)
			{
				foreach(var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
					return View();
				}
			}

			await _userManager.AddToRoleAsync(user, "Member");

			return RedirectToAction("Login", "Account");

		}

		public async Task<IActionResult> SignOut()
		{
			await _signManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
