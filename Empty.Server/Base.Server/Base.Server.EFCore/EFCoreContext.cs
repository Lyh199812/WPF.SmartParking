using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;
using System.Linq;

namespace Base.Server.EFCore
{
    public class EFCoreContext : DbContext, IDbContext
    {
        public EFCoreContext(DbContextOptions<EFCoreContext> options)
            : base(options)
        {
        }

        public DbContext GetDbContext()
        {
            return this;
        }

        // 对接数据库   用户数据    DBContext  声明一个对应的集合对象
        //public DbSet<MenuModel> Menus { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="time">2022-01-24 20:00:00</param>
        /// <returns></returns>
        private long TimeValueFromString(string time)
        {
            // C#进行获取时间戳的方式
            return (DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Ticks - 621355968000000000) / 10000000;
        }

        private string TimeValueFromLong(long time)
        {
            return new DateTime(time * 10000000 + 621355968000000000).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
