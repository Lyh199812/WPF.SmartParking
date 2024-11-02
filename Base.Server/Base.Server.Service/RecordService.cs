using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.EFCore;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Service
{
    public class RecordService : BaseService, IRecordService
    {
        public RecordService(IDbContext dbConfig) : base(dbConfig)
        {
        }

        public RecordInfo GetRecordInfo(string license)
        {
            // 先取已支付的最大时间，如果没有，就是0
            // 先付费  再
            //var maxTime = this.Query<RecordInfo>(p => p.AutoLicense == license && p.State == 1)?.ToList()
            //    .Max(pm => GetTimespan(pm.LeaveTime));

            //maxTime = maxTime == null ? 0 : maxTime;

            return this.Query<RecordInfo>(p => p.AutoLicense == license && p.State == 0).ToList().First();
        }

        public void Save(RecordInfo recordInfo)
        {
            if (recordInfo.RecordId == 0)
            {
                // 添加用户
                dbContext.Entry(recordInfo).State = EntityState.Added;
                dbContext.SaveChanges();
            }
            else
            {
                this.Update<RecordInfo>(recordInfo);
            }
            dbContext.Entry(recordInfo).State = EntityState.Detached;
        }
    }
}
