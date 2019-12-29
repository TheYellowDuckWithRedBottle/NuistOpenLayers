using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using XinYiThree.Models;
using XinYiThree.ViewModels;

namespace XinYiThree.Controllers
{
    [Authorize(Policy ="仅限管理员")]
   // [Authorize(Roles ="管理员")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(CreateRoleViewModel createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createRoleViewModel);
            }
            var role = new IdentityRole
            {
                Name = createRoleViewModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, "添加角色名称错误");
            }
            return View(createRoleViewModel);

        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ModelState.AddModelError(string.Empty,"未找到该角色");
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var RoleEditModel = new RoleEditModel {
                Id = id,
                RoleName = role.Name,
                Users = new List<string>()
            };
            var users =await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if(await _userManager.IsInRoleAsync(user,RoleEditModel.RoleName))
                {
                    RoleEditModel.Users.Add(user.UserName);
                }
            }
            return View("EditRole", RoleEditModel);
            
        }
       [HttpPost]
        public async Task<IActionResult> EditRole(string id,RoleEditModel roleEditModel)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                role.Name = roleEditModel.RoleName;
                var result =await _roleManager.UpdateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty,"更新角色出错");
                return View("Index");
            }
           
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role =await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var users =await _userManager.Users.ToListAsync();

            var vm = new UserRoleViewModel()
            {
                RoldId = role.Id
            };

            foreach (var user in users)
            {
                if(!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Users.Add(user);
                }
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userroleviewmodel)
        {
            var user = await _userManager.FindByIdAsync(userroleviewmodel.UserId);
            var role = await _roleManager.FindByIdAsync(userroleviewmodel.RoldId);
           // var RoleEditModel = new RoleEditModel() { Id = role.Id, RoleName = role.Name, Users = new List<string>() };
            if (user != null && role != null)
            {
               var result=await _userManager.AddToRoleAsync(user,role.Name);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            ModelState.AddModelError(string.Empty, "用户名或角色不存在");
            return View(userroleviewmodel);
        }
        public async Task<IActionResult> RemoveUserFromRole(string UserName,string RoleId)
        {
          
            var role = await _roleManager.FindByIdAsync(RoleId);//根据角色id找到角色
            var user = await _userManager.FindByNameAsync(UserName);
            if(role!=null&&user!=null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);//从角色里移除Application
                if(result.Succeeded)
                {
                    return RedirectToAction("EditRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ModelState.AddModelError(string.Empty, "未找到该角色");
            return View("EditRole");
        }
        public async Task<IActionResult> CheckRoleExist([Bind("RoleName")]string RoleName)
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role != null)
            {
                return Json("角色已经存在了");
            }
            return Json(true);
        }
    }
}
