
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

            // ע�����������ݿ������ĺ���������
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // ���� HTTP ����ܵ�
            ConfigureMiddleware(app);

            app.Run();
        }

        // ����: ���÷���
        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // ע�� IDbContext �ӿں;���ʵ�� AppDbContext
            services.AddTransient<IDbContext, EFCoreContext>();

            // ���� Jwt ��֤
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // ����Ĭ����֤��ʽΪ JWT
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // ���� HTTPS Ԫ����Ҫ�������ڿ�������
                    options.SaveToken = true; // ���� Token �Ա��������
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, // ��֤ǩ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456123456123456123456123456123456123456123456123456123456")), // ��Կ
                        ValidIssuer = "webapi.cn", // JWT ��ǩ����
                        ValidAudience = "WebApi", // JWT ������
                        ValidateIssuer = false, // �Ƿ���֤ Issuer
                        ValidateAudience = false // �Ƿ���֤ Audience
                    };
                });

            // ע�� DbContext ������ MySQL ���ݿ�����
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFCoreContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))) // �滻Ϊʵ�� MySQL �汾
            );

            // ע�� IUserService �ӿں;���ʵ�� UserService


            // ע�����������
            services.AddControllers();

            // ���� Swagger ��֧�� API �ĵ�
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // ����: �����м��
        static void ConfigureMiddleware(WebApplication app)
        {
            // ����ǿ������������� Swagger ���� API �ĵ�
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartParking.Server v1"));
            }

            // ʹ����֤�м����������֤��ص� HTTP ����
            app.UseAuthentication();

            // ʹ����Ȩ�м����ȷ���û�����Ȩ�����ض���Դ
            app.UseAuthorization();

            // ӳ�������·��
            app.MapControllers();
        }

    }
}
