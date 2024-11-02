using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;
using System.Linq;
using Base.Server.Models;

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
        public DbSet<UpgradeFileModel> UpgradeFileModel { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
        public DbSet<RecordInfo> RecordInfo { get; set; }

        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<AutoRegister> AutoRegister { get; set; }

        public DbSet<AutoColor> AutoColor { get; set; }
        public DbSet<LicenseColor> LicenseColor { get; set; }
        public DbSet<FeeMode> FeeModel { get; set; }


        public DbSet<RoleMenu> RoleMenu { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 联合主键
            modelBuilder.Entity<RoleMenu>().HasKey(pk => new { pk.RoleId, pk.MenuId });
            modelBuilder.Entity<UserRole>().HasKey(pk => new { pk.RoleId, pk.UserId });



            // 文件库中时间转换  string<->timespan
            /// 1、创建了ValueConverter对象实例
            /// 2、构造函数两个参数（两个委托）
            ///     -  第一个委托是从第一个泛型类型转换到第二个泛型
            ///     -  第二个委托   反相操作
            //ValueConverter timeValueConverter = new ValueConverter<string, long>
            //    (
            //    str => TimeValueFromString(str),
            //    ts => TimeValueFromLong(ts)
            //    );
            // 配置到EFCore中
           // modelBuilder.Entity<UpgradeFileModel>().Property(p => p.UploadTime).HasConversion(timeValueConverter);


            // 菜单表中字体图标值转换   编号string<->字符string
            ValueConverter iconValueConverter = new ValueConverter<string, string>(

                v => string.IsNullOrEmpty(v) ? null : ((int)v.ToArray()[0]).ToString("x"),

                v => v == null ? "" : ((char)int.Parse(v, NumberStyles.HexNumber)).ToString()

                );
            modelBuilder.Entity<MenuModel>().Property(p => p.MenuIcon).HasConversion(iconValueConverter);


            //modelBuilder.Entity<AutoRegister>().Property(p => p.ValidStartTime).HasConversion(timeValueConverter);
            //modelBuilder.Entity<AutoRegister>().Property(p => p.ValidEndTime).HasConversion(timeValueConverter);
            //// (char)64

            //modelBuilder.Entity<RecordInfo>().Property(p => p.EnterTime).HasConversion(timeValueConverter);
            //modelBuilder.Entity<RecordInfo>().Property(p => p.LeaveTime).HasConversion(timeValueConverter);
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
