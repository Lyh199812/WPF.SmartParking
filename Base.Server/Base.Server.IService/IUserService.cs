using System;
using System.Collections.Generic;
using Base.Server.Models;

namespace Base.Server.IService
{
    public interface IUserService : IBaseService
    {
        bool Login(string username, string password,out UserModel userModel);

        void SaveUser(UserModel data);

        void SaveRoles(UserModel data);


        void ResetPassword(int userId);
    }
}
