using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Base.Server.IService;
using Base.Server.Models;
using Base.Server.Service;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        IRecordService _recordService;
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }
        [HttpPost("save")]
        public IActionResult Save(RecordInfo recordInfo)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                _recordService.Save(recordInfo);
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        // 车牌号   多个
        [HttpGet("{license}")]
        public IActionResult GetRecordByLicense([FromRoute] string license)
        {
            Result<RecordInfo> result = new Result<RecordInfo>();
            try
            {
                result.Data = _recordService.GetRecordInfo(license);
            }
            catch (System.Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }
    }
}
