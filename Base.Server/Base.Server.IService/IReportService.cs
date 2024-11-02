using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.Models;

namespace Base.Server.IService
{
    public interface IReportService : IBaseService
    {
        List<ReportModel> GetParkingReport(DateTime st, DateTime et);
    }
}
