
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Base.Server.EFCore;
using Base.Server.IService;
using Base.Server.Service;
using System.Text;

namespace SmarkParking.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 注入对象，添加数据库上下文和其他服务
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // 配置 HTTP 请求管道
            ConfigureMiddleware(app);

            app.Run();
        }

        // 方法: 配置服务
        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 注入 IDbContext 接口和具体实现 AppDbContext
            services.AddTransient<IDbContext, EFCoreContext>();

            // 配置 Jwt 认证
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // 设置默认认证方式为 JWT
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // 禁用 HTTPS 元数据要求，适用于开发环境
                    options.SaveToken = true; // 保存 Token 以便后续调用
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, // 验证签名
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456123456123456123456123456123456123456123456123456123456")), // 密钥
                        ValidIssuer = "webapi.cn", // JWT 的签发者
                        ValidAudience = "WebApi", // JWT 的受众
                        ValidateIssuer = false, // 是否验证 Issuer
                        ValidateAudience = false // 是否验证 Audience
                    };
                });

            // 注册 DbContext 并配置 MySQL 数据库连接
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFCoreContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))) // 替换为实际 MySQL 版本
            );

            // 注入 IUserService 接口和具体实现 UserService


            // 注册控制器服务
            services.AddControllers();

            // 配置 Swagger 以支持 API 文档
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // 方法: 配置中间件
        static void ConfigureMiddleware(WebApplication app)
        {
            // 如果是开发环境，启用 Swagger 生成 API 文档
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartParking.Server v1"));
            }

            // 使用认证中间件，处理认证相关的 HTTP 请求
            app.UseAuthentication();

            // 使用授权中间件，确保用户被授权访问特定资源
            app.UseAuthorization();

            // 映射控制器路由
            app.MapControllers();
        }

    }
}
