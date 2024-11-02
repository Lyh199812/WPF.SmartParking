using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Base.Server.IService;
using Base.Server.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Base.Server.Service;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserService _userService;
        IMenuService _menuService;
        public LoginController(IUserService userService, IMenuService menuService)
        {
            _userService = userService;
            _menuService = menuService;
        }
        // 数据资源服务程序 
        // FromForm     http://localhost:5000/login
        // 
        [HttpPost]
        public IActionResult Post([FromForm] string username, [FromForm] string password)
        {
            // admin   123456
            // 用户名和密码
            // 返回验证状态
            // 数据库对比

            //EFCoreContext  // 强关联
            //var users = _userService.Query<UserModel>(u => u.UserName == username && u.Password == password).ToList();

            Result<UserModel> result = new Result<UserModel>();

            try
            {
                if (_userService.Login(username, password, out UserModel userModel))
                {
                    // 暂时返回这个状态     后续需要用户相关信息返回（权限菜单）
                    userModel.Menus = _menuService.GetMenusByUserId(userModel.UserId);

                    result.Data = userModel;
                    result.State = 200;
                }
                else
                {
                    result.State = 500;
                    result.ExceptionMessage = "用户名或密码错误";
                }
            }
            catch (Exception ex)
            {
                result.State = 501;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);// 自动序列化成Josn字符串
        }

        // 如果认证不通过   返回401状态
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("测试成功");
        }
    }
}
