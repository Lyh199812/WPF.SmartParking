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
    public class BaseInfoService : BaseService, IBaseInfoService
    {
        public BaseInfoService(IDbContext dbConfig) : base(dbConfig)
        {

        }

        public void SaveAutoColor(AutoColor autoColor)
        {
            dbContext.Entry(autoColor).State = EntityState.Added;
            dbContext.SaveChanges();
            dbContext.Entry(autoColor).State = EntityState.Detached;
        }

        public void SaveLicenseColor(LicenseColor licenseColor)
        {
            dbContext.Entry(licenseColor).State = EntityState.Added;
            dbContext.SaveChanges();
            dbContext.Entry(licenseColor).State = EntityState.Detached;
        }

        public void SaveFeeMode(FeeMode feeMode)
        {
            dbContext.Entry(feeMode).State = EntityState.Added;
            dbContext.SaveChanges();
            dbContext.Entry(feeMode).State = EntityState.Detached;
        }
    }
}
