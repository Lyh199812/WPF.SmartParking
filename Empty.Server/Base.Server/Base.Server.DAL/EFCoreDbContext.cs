using Base.Server.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.IDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Base.Server.DAL
{
    public class EFCoreDbContext : DbContext,IDbContext
    {
        // 同一个实例 ，   每次请求都是一个新的实例
        //EFCoreContext eFCoreContext = null;
        public EFCoreDbContext(DbContextOptions<DbContext> options):base(options)
        {
           // eFCoreContext = new EFCoreContext(options);
        }
        public DbContext GetDbContext()
        {
           // return eFCoreContext;
           return this;
        }
    }
}
