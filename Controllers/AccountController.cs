using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XinYiThree.Models;
using XinYiThree.ViewModels;

namespace XinYiThree.Controllers
{
   
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        [HttpPost]
       public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Map");
                }
            }
            ModelState.AddModelError("","用户名或者密码错误");
            return View(loginViewModel);
            
        }
        [HttpGet]
        public IActionResult Signin()
        {
            Console.WriteLine("进入登陆");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(SignViewModel signViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = signViewModel.Username ,
                    SID =signViewModel.SID,
                    Email=signViewModel.Email,
                    IdCardNo=signViewModel.IdCardNo,
                    BirthDate=signViewModel.BirthDate
                };
                var result =await  _userManager.CreateAsync(user, signViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View(signViewModel);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
