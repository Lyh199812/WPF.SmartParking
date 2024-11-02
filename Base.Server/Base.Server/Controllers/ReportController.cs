using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using Base.Server.Models;
using System.Collections.Generic;
using Base.Server.IService;

namespace Base.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        // 起始时间 - 结束时间
        // http://localhost:5000/api/report/20220326200000/20220326220000
        [HttpGet("{start}/{end}")]
        public IActionResult GetParkingReport([FromRoute] string start, [FromRoute] string end)
        {
            Result<List<ReportModel>> result = new Result<List<ReportModel>>();
            try
            {
                if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
                    throw new ArgumentException("时间参数传入无效");

                DateTime st = DateTime.ParseExact(start, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                DateTime et = DateTime.ParseExact(end, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);

                result.Data = _reportService.GetParkingReport(st, et);
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
