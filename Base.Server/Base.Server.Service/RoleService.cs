using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IDbContext dbConfig) : base(dbConfig)
        {
        }

        public void Save(RoleInfo roleInfo)
        {
            if (roleInfo.RoleId == 0)
            {
                // 添加用户
                dbContext.Entry(roleInfo).State = EntityState.Added;
                dbContext.SaveChanges();
            }
            else
            {
                this.Update<RoleInfo>(roleInfo);
            }
            dbContext.Entry(roleInfo).State = EntityState.Detached;
        }

        public void SaveRole(RoleInfo roleInfo)
        {
            if (roleInfo.UserIds != null)
            {
                // 每次保存的时候都是重新所把有的相关ID传来
                // 先移除，再保存
                var urs = dbContext.Set<UserRole>().Where(u => u.RoleId == roleInfo.RoleId).ToList();
                // 移除当前权限下的所有用户
                urs.ForEach(u => dbContext.Set<UserRole>().Remove(u));
                // 将最新的用户列表 保存进去
                roleInfo.UserIds.ForEach(u => dbContext.Set<UserRole>().Add(new UserRole { UserId = u, RoleId = roleInfo.RoleId }));
            }
            // 更新RoleMenu
            if (roleInfo.MenuIds != null)
            {
                var ms = dbContext.Set<RoleMenu>().Where(u => u.RoleId == roleInfo.RoleId).ToList();
                ms.ForEach(m => dbContext.Set<RoleMenu>().Remove(m));

                roleInfo.MenuIds.ForEach(m => dbContext.Set<RoleMenu>().Add(new RoleMenu { MenuId = m, RoleId = roleInfo.RoleId }));
            }

            dbContext.SaveChanges();
        }
    }
}
