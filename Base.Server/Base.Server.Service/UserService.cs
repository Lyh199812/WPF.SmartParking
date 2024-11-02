using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Base.Server.EFCore;
using Base.Server.IService;
using Base.Server.Models;

namespace Base.Server.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IDbContext dbConfig) : base(dbConfig) { }

        public bool Login(string username, string pwd, out UserModel userModel)
        {
            // 加密密码
            string password = GetMD5Str(GetMD5Str(pwd) + "|" + username);
            // 根据有名和加密后的密码进行查询 
            var users = this.Query<UserModel>(u => u.UserName == username && u.Password == password).ToList();
            userModel = users.FirstOrDefault();

            // AuthentationToken 计算Token
            if (users.Count > 0 && this.AuthentationToken(username, out string token))
                userModel.Token = token;

            return users.Any();
        }

        // 需要进行重置密码的时候 ，做123456密码的加密处理再更新到对应的用户


        // 登录密码处理
        // 重置密码
        // 只针对一次加密   
        private string GetMD5Str(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr)) return "";

            byte[] result = Encoding.Default.GetBytes(inputStr);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
        }

        private bool AuthentationToken(string username, out string token)
        {
            token = string.Empty;
            if (string.IsNullOrEmpty(username))
                return false;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username)
            };

            // 密码   16位
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456123456123456123456123456123456123456123456123456123456"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                "webapi.cn",
                "WebApi",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }

        public void SaveUser(UserModel data)
        {
            if (data.UserId == 0)
            {
                // 添加用户
                data.Password = GetMD5Str(GetMD5Str("123456") + "|" + data.UserName);
                dbContext.Entry(data).State = EntityState.Added;
                dbContext.SaveChanges();
            }
            else
            {
                //dbContext.Entry(data).State = EntityState.Modified;
                this.Update<UserModel>(data);
            }
            dbContext.Entry(data).State = EntityState.Detached;

            // 根据ID查询出来   然后进属性赋值 
        }

        // 可在当前用户的权限组
        public void SaveRoles(UserModel data)
        {
            var r = dbContext.Set<UserRole>().Where(u => u.UserId == data.UserId).ToList();
            r.ForEach(i => dbContext.Set<UserRole>().Remove(i));
            data.Roles.ForEach(r => dbContext.Set<UserRole>().Add(new UserRole { UserId = data.UserId, RoleId = r.RoleId }));
            dbContext.SaveChanges();
        }

        // UserId唯一      UserName的唯一 性检查
        public void ResetPassword(int userId)
        {
            dbContext.Set<UserModel>()
                .Where(u => u.UserId == userId).ToList()
                .ForEach(u => u.Password = GetMD5Str(GetMD5Str("123456") + "|" + u.UserName));

            dbContext.SaveChanges();
        }
    }
}
