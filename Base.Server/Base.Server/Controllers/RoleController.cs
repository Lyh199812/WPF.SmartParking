using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Base.Server.IService;
using Base.Server.Models;
using Base.Server.Service;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // 获取权限组
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            Result<List<RoleInfo>> result = new Result<List<RoleInfo>>();
            try
            {
                result.Data = _roleService.Query<RoleInfo>(r => r.state == 1).ToList();
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("rols/{userIds}")]
        public IActionResult GetRoleByUser([FromRoute] string userId)
        {
            Result<List<RoleInfo>> result = new Result<List<RoleInfo>>();
            try
            {
                result.Data = _roleService.Query<RoleInfo>(r => r.state == 1).ToList();
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        // 这个接口支持新增和修改
        [HttpPost("save")]
        public IActionResult Save(RoleInfo roleInfo)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _roleService.Save(roleInfo);
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

        [HttpPost("save_role")]
        public IActionResult SaveRole(RoleInfo roleInfo)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _roleService.SaveRole(roleInfo);
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
    }
}
