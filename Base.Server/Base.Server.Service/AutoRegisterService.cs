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
    public class AutoRegisterService : BaseService, IAutoRegisterService
    {
        public AutoRegisterService(IDbContext dbConfig) : base(dbConfig)
        {

        }

        // 数据操作
        public List<AutoRegister> GetAll(string key, ref int pageIndex, ref int perPageCount)
        {
            // 获取表中的   第2页   第11条记录开始的10条记录
            // List    


            // 查找出所有相关的数据    计算总条数
            int count = dbContext.Set<AutoRegister>()
                .Where(q => q.State == 1 && (string.IsNullOrEmpty(key) || q.AutoLicense.Contains(key)))
                .Count();

            int checkIndex = (pageIndex - 1) * perPageCount;
            if (checkIndex >= count)
                pageIndex = 1;
            // 根据分布取数据
            var list = dbContext.Set<AutoRegister>()
                            .Where(q => q.State == 1 && (string.IsNullOrEmpty(key) || q.AutoLicense.Contains(key)));
            list = list.Skip((pageIndex - 1) * perPageCount)
                .Take(perPageCount);
            list = list.OrderBy(q => q.AutoLicense);


            var values = (from q in dbContext.Set<AutoRegister>()
                            .Where(q => q.State == 1 && (string.IsNullOrEmpty(key) || q.AutoLicense.Contains(key)))
                            .Skip((pageIndex - 1) * perPageCount)// 获取一个集合中数据的时候路过多少条记录
                            .Take(perPageCount)// 从一个集合中指定起始位置获取指定条数的记录，
                            .OrderBy(q => q.AutoLicense)// 排序
                            .ToList()
                          join lc in dbContext.Set<LicenseColor>() on q.LicenseColorId equals lc.ColorId
                          join ac in dbContext.Set<AutoColor>() on q.AutoColorId equals ac.ColorId
                          join am in dbContext.Set<FeeMode>() on q.FeeModeId equals am.FeeModelId
                          //where q.State == 1
                          select new AutoRegister
                          {
                              AutoId = q.AutoId,
                              AutoLicense = q.AutoLicense,
                              LicenseColorId = q.LicenseColorId,
                              LicenseColorName = lc.ColorName,
                              AutoColorId = q.AutoColorId,
                              AutoColorName = ac.ColorName,
                              FeeModeId = q.FeeModeId,
                              FeeModeName = am.FeeModelName,
                              Description = q.Description,
                              ValidCount = q.ValidCount,
                              ValidEndTime = q.ValidEndTime,
                              ValidStartTime = q.ValidStartTime
                          }).ToList();

            perPageCount = count;//  不正确的写法，不建议   临时借用一下

            return values;
        }

        public void SaveAuto(AutoRegister auto)
        {
            // 用户管理来    获取用户信息的时候  使用Query   信息跟踪
            if (auto.AutoId == 0)
            {
                // 添加用户
                dbContext.Entry(auto).State = EntityState.Added;
                dbContext.SaveChanges();
            }
            else
            {
                this.Update<AutoRegister>(auto);
            }
            //释放对象 
            dbContext.Entry(auto).State = EntityState.Detached;
        }
    }
}
