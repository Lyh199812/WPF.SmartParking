using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("all")]
        [Authorize]
        public IActionResult GetUsers(string key)
        {
            //0：无效
            //1：有效
            //2：可以登录系统   参考
            Result<List<UserModel>> result = new Result<List<UserModel>>();
            try
            {
                result.Data = _userService.Query<UserModel>(u => u.state == 1 && (string.IsNullOrEmpty(key) ||
                u.RealName.Contains(key) || u.UserName.Contains(key)
                )).ToList();

                // 获取每个用户的权限组
                foreach (var item in result.Data)
                {
                    List<int> roleIds = _userService.Query<UserRole>(ur => ur.UserId == item.UserId).ToList()
                         .Select(u => u.RoleId).ToList();
                    item.Roles = _userService.Query<RoleInfo>(ri => roleIds.Contains(ri.RoleId)).ToList();
                }
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save")]
        [Authorize]
        public IActionResult UpdateUserInfo(UserModel user)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                //if (user.UserId == 0)

                //    //_userService.Insert<UserModel>(user);
                //else
                //    //_userService.Update<UserModel>(user);
                _userService.SaveUser(user);
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }


        [HttpGet("byrole/{roleId}")]
        public IActionResult GetUsersByRole([FromRoute] int roleId)
        {
            Result<List<UserModel>> result = new Result<List<UserModel>>();
            try
            {
                var ids = _userService.Query<UserRole>(r => r.RoleId == roleId).ToList().Select(u => u.UserId);

                result.Data = _userService.Query<UserModel>(u => u.state == 1 && ids.Contains(u.UserId)).ToList();
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save_roles")]
        public IActionResult SaveRoles(UserModel user)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _userService.SaveRoles(user);
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("resetpwd")]
        public IActionResult ResetPassword([FromForm] IFormCollection form)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _userService.ResetPassword(int.Parse(form["userId"]));
                result.Data = true;
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.Data = false;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("check/{username}")]
        public IActionResult CheckUser([FromRoute] string username)
        {
            var ui = _userService.Query<UserModel>(u => u.UserName == username).ToList();
            if (ui.Count > 0)
                return Ok(true);// 已存在
            return Ok(false);// 不存在
        }
    }


    // 不理解   知道有这种方式    知道怎么写（应用）   原理（高级班）
}
