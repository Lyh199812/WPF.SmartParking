using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.Models;

namespace Base.Server.IService
{
    public interface IMenuService : IBaseService
    {
        List<MenuModel> GetAllMenus();
        List<MenuModel> GetMenusByUserId(int userid);
        void SaveMenu(MenuModel data);
    }
}
