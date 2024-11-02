using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.EFCore;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Service
{
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(IDbContext dbConfig) : base(dbConfig)
        {
        }
        public List<MenuModel> GetAllMenus()
        {
            return (from menu in dbContext.Set<MenuModel>()
                    where menu.State == 1
                    select menu).ToList();
        }
        public List<MenuModel> GetMenusByUserId(int userid)
        {
            // 获取所有权限
            var roles = (from ur in dbContext.Set<UserRole>()
                         join role in dbContext.Set<RoleInfo>() on ur.RoleId equals role.RoleId
                         where ur.UserId == userid && role.state == 1
                         select ur.RoleId).ToList();

            // 菜单的去重
            var query = from menu in dbContext.Set<MenuModel>()
                        join role_menu in dbContext.Set<RoleMenu>()
                        on menu.MenuId equals role_menu.MenuId
                        where roles.Contains(role_menu.RoleId) && menu.State == 1
                        select menu;

            return query.Distinct().ToList();
        }

        public void SaveMenu(MenuModel data)
        {
            if (data.MenuId == 0)
            {
                var index = dbContext.Set<MenuModel>().Max(i => i.Index);
                data.Index = index + 1;
                data.State = 1;

                // 添加菜单 
                dbContext.Entry(data).State = EntityState.Added;
                dbContext.SaveChanges();
            }
            else
            {
                //dbContext.Entry(data).State = EntityState.Modified;
                this.Update<MenuModel>(data);
            }
            dbContext.Entry(data).State = EntityState.Detached;
        }

        

    }
}
