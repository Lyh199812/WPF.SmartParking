using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.Models;

namespace Base.Server.IService
{
    public interface IAutoRegisterService : IBaseService
    {
        // 分页 -》    当前页的数据     分页信息（包含多少条数据     
        List<AutoRegister> GetAll(string key, ref int pageIndex,ref int perPageCount);
        void SaveAuto(AutoRegister auto);
    }
}
