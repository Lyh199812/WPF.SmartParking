using System;
using Zhaoxi.SmartParking.Server.EFCore;
using Zhaoxi.SmartParking.Server.IDbConfig;

namespace Zhaoxi.SmartParking.Server.DbConfig
{
    public class DbConfig : IDbConfig.IDbConfig
    {
        EFCoreContext eFCoreContext = null;
        public DbConfig()
        {
            eFCoreContext = new EFCoreContext();
        }
        public EFCoreContext GetDbContext()
        {
            return eFCoreContext;
        }
    }
}
