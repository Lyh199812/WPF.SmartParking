using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Base.Server.IService;
using Base.Server.Models;
using Base.Server.Service;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        IAutoRegisterService _autoRegisterService;
        public AutoController(IAutoRegisterService autoRegisterService)
        {
            _autoRegisterService = autoRegisterService;
        }
        [HttpPost("list")]
        public IActionResult GetAll([FromForm] IFormCollection formCollection)
        {
            PaginationResult<List<AutoRegister>> result = new PaginationResult<List<AutoRegister>>();
            try
            {
                string key = formCollection["key"].ToString();
                int index = int.Parse(formCollection["index"].ToString());
                int count = int.Parse(formCollection["count"].ToString());

                result.Data = _autoRegisterService.GetAll(key, ref index, ref count);
                result.PageInfo = new PageInfo() { PageIndex = index, RecordCount = count };
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return Ok(result);
        }





        [HttpPost("save")]
        [Authorize]
        public IActionResult UpdateAutoInfo(AutoRegister auto)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _autoRegisterService.SaveAuto(auto);
                result.Data = true;
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }


        [HttpGet("{license}")]
        public IActionResult GetAutoByLicense([FromRoute] string license)
        {
            PaginationResult<AutoRegister> result = new PaginationResult<AutoRegister>();
            try
            {
                result.Data = _autoRegisterService.Query<AutoRegister>(a => a.AutoLicense == license).ToList().First();
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
