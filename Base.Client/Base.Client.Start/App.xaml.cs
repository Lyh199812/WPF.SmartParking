using Base.Client.BLL;
using Base.Client.Common;
using Base.Client.DAL;
using Base.Client.IBLL;
using Base.Client.IDAL;
using Base.Client.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace Base.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            // 一种特定格式的字符串
            string json = "{\"menuId\": 3,  \"menuHeader\": \"系统维护\",\"targetView\": null}";


            //System.Text.Json.JsonSerializer.Serialize(对象);
            //System.Text.Json.JsonSerializer.Deserialize<对象类型>("json");


            // WebApi 功能测试
            // 客户端   通过http://localhost:22643/WeatherForecast
            // 请求数据资源
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJleHAiOjE2NDI4NjM3MjYsImlzcyI6IndlYmFwaS5jbiIsImF1ZCI6IldlYkFwaSJ9.p2MtlcqBVebWBWPwc3W3EyqsLPeuf4AKkxvw2XzwT2g");
            //var resp = httpClient.GetAsync("http://localhost:22643/api/login").GetAwaiter().GetResult();
            //string values = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();    // Json
            // 进一步的需要进行反序列化

            // ADO.NET   请求数据库

            // 判断条件    true      C++
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            if (Container.Resolve<Views.WinLogin>().ShowDialog() == false)
                //    shell.Show();// 不能使用ShowDialog，会导致后续Module无法加载
                //else
                Environment.Exit(0);// 直接强制退出
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(GlobalValue), () => new GlobalValue());
            containerRegistry.Register(typeof(MessageService), () => new MessageService(Container.Resolve<IDialogService>()));
            //ConfigurationManager    包一层
            containerRegistry.Register(typeof(Dispatcher), obj => Application.Current.Dispatcher);

            containerRegistry.Register<IWebDataAccess, WebDataAccess>();
            containerRegistry.Register<ILocalDataAccess, LocalDataAccess>();

            // Dal的注册
            containerRegistry.Register<ILoginDal, LoginDal>();
            containerRegistry.Register<IFileDal, FileDal>();
            containerRegistry.Register<IMenuDal, MenuDal>();
            containerRegistry.Register<IUserDal, UserDal>();
            containerRegistry.Register<IRoleDal, RoleDal>();
            containerRegistry.Register<IAutoDal, AutoDal>();
            containerRegistry.Register<IBaseDal, BaseDal>();
            containerRegistry.Register<IRecordDal, RecordDal>();
            containerRegistry.Register<IPayDal, PayDal>();
            containerRegistry.Register<IReportDal, ReportDal>();

            // BLL的注册
            containerRegistry.Register<ILoginBLL, LoginBLL>();
            containerRegistry.Register<IFileBLL, FileBLL>();
            containerRegistry.Register<IMenuBLL, MenuBLL>();
            containerRegistry.Register<IUserBLL, UserBLL>();
            containerRegistry.Register<IRoleBLL, RoleBLL>();
            containerRegistry.Register<IAutoBLL, AutoBLL>();
            containerRegistry.Register<IBaseBLL, BaseBLL>();
            containerRegistry.Register<IRecordBLL, RecordBLL>();
            containerRegistry.Register<IPayBLL, PayBLL>();
            containerRegistry.Register<IMonitorBLL, MonitorBLL>();
            containerRegistry.Register<IReportBLL, ReportBLL>();

            // 注册弹出窗口的父窗口
            containerRegistry.RegisterDialogWindow<Views.DialogWindow>();


            containerRegistry.RegisterDialog<MessageView>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            // 自动更新的时候    自动扫描目录
            return new DirectoryModuleCatalog()
            {
                ModulePath = Environment.CurrentDirectory + "\\Modules"
            };
        }
    }
}
