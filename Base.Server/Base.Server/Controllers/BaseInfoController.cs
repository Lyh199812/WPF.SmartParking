using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseInfoController : ControllerBase
    {
        IBaseInfoService _baseInfoService;
        public BaseInfoController(IBaseInfoService baseInfoService)
        {
            _baseInfoService = baseInfoService;
        }
        [HttpGet("autocolor")]
        public IActionResult GetAutoColors()
        {
            Result<List<AutoColor>> result = new Result<List<AutoColor>>();
            try
            {
                result.Data = _baseInfoService.Query<AutoColor>(ac => true).ToList();
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("licecolor")]
        public IActionResult GetLicenseColors()
        {
            Result<List<LicenseColor>> result = new Result<List<LicenseColor>>();
            try
            {
                result.Data = _baseInfoService.Query<LicenseColor>(ac => true).ToList();
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("feemode")]
        public IActionResult GetFeeModes()
        {
            Result<List<FeeMode>> result = new Result<List<FeeMode>>();
            try
            {
                result.Data = _baseInfoService.Query<FeeMode>(ac => true).ToList();
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save_auto_color")]
        public IActionResult SaveAutoColor(AutoColor autoColor)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _baseInfoService.SaveAutoColor(autoColor);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.Data = false;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save_license_color")]
        public IActionResult SaveLicenseColor(LicenseColor licenseColor)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _baseInfoService.SaveLicenseColor(licenseColor);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.Data = false;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("save_fee_mode")]
        public IActionResult SaveFeeMode(FeeMode feeModel)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _baseInfoService.SaveFeeMode(feeModel);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.Data = false;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
    }
}
