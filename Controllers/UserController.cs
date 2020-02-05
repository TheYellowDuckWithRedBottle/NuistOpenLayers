using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using XinYiThree.Models;
using XinYiThree.ViewModels;

namespace XinYiThree.Controllers
{
   // [Authorize(Roles ="管理员")]
    public class UserController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                return View("EditUser");
            }
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string id ,UserEidtViewModel userEidtViewModel)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty,"未找到该用户");
            }

            user.Email = userEidtViewModel.Email;
            user.UserName = userEidtViewModel.UserName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "更新用户信息时发生错误");
            return View(user);

        }
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddViewModel userAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userAddViewModel);
            }
            var user = new ApplicationUser
            {
                UserName = userAddViewModel.UserName,
                Email = userAddViewModel.Email,
                SID=userAddViewModel.SID,
                IdCardNo=userAddViewModel.IdCardNo,
                BirthDate=userAddViewModel.BirthDate
            };
            var result = await _userManager.CreateAsync(user, userAddViewModel.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", await _userManager.Users.ToListAsync());
            }
            else
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(userAddViewModel);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
          
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"删除用户发生错误");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty,"不存在该用户");
            }
            return View("Index",await _userManager.Users.ToListAsync());
        }

    }
}
