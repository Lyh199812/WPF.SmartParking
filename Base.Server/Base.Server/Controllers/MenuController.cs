using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        [Route("all")]// 鉴权    
        [Authorize]
        public IActionResult GetAllMenus()
        {
            Result<List<MenuModel>> result = new Result<List<MenuModel>>();
            try
            {
                var datas = _menuService.Query<MenuModel>(mm => mm.State == 1).ToList();

                result.Data = datas;// _menuService.GetAllMenus(); ;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }
        [HttpGet("byrole/{roleId}")]
        public IActionResult GetMenusIdByRole([FromRoute] int roleId)
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var datas = _menuService.Query<RoleMenu>(mm => mm.RoleId == roleId).Select(mm => mm.MenuId).ToList();

                result.Data = datas;// _menuService.GetAllMenus(); ;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult SaveMenu(MenuModel data)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _menuService.SaveMenu(data);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }
    }
}
