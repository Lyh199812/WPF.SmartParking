using Base.Client.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Common;

namespace Base.Client.SystemModule.ViewModels
{
    public class MainHeaderViewModel : BindableBase
    {
        private string _currentUserName;
        public string CurrentUserName
        {
            get { return _currentUserName; }
            set { SetProperty(ref _currentUserName, value); }
        }

        private string _userAvatar;
        public string UserAvatar
        {
            get { return _userAvatar; }
            set { SetProperty(ref _userAvatar, value); }
        }


        public MainHeaderViewModel(GlobalValue globalValue)
        {
            CurrentUserName = globalValue.UserInfo.realName;
            //http://localhost:22643/api/file/img/1001.png
            // 如果需要Token的话，这里需要下载图片到本地，然后再引用
            UserAvatar = "http://localhost:22643/api/" + globalValue.UserInfo.userIcon;
        }
    }
}
