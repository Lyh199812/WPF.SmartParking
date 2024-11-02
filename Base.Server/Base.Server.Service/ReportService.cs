using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.EFCore;
using Base.Server.EFCore;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Service
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(IDbContext dbConfig) : base(dbConfig)
        {
        }

        public List<ReportModel> GetParkingReport(DateTime st, DateTime et)
        {
            // 时间戳
            // 把所有有效的订单取出来  State=1  完成   SQL语法  
            var all = dbContext.Set<RecordInfo>().Where(q => q.State == 1).ToList();
            // 根据时间来区分出时间段的数据    Linq
            all = all.Where(q =>
                   DateTime.Compare(q.LeaveTime, st) >= 0 &&
                   DateTime.Compare(q.LeaveTime, et) <= 0).ToList();
            // 汇总计算  ?
            all = all.Select(s => new RecordInfo
            {
                LeaveTime = s.LeaveTime,//   数据 库中的数据   带时分秒   只需要年月日
                Payable = s.Payable,
                PayType = s.PayType,
                Payment = s.Payment,
            }).ToList();
            // 将日期进行GroupBy
            return all.GroupBy(a => new { a.LeaveTime })
                .Select(a => new ReportModel
                {
                    LeaveTime = a.Key.LeaveTime,
                    OrderCount = a.Count(),
                    Payable = a.Sum(sub => sub.Payable),
                    PaymentCash = a.Where(k => k.PayType == 1).Sum(sub => sub.Payment),
                    PaymentElec = a.Where(k => k.PayType == 2).Sum(sub => sub.Payment)
                }).ToList();
        }
    }
}
