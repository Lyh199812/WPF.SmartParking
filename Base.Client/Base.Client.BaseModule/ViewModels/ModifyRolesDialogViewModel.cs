﻿using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.Entity;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class ModifyRolesDialogViewModel : IDialogAware
    {
        public string Title => "用户角色分配";

        public event Action<IDialogResult> RequestClose2;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            userId = parameters.GetValue<int>("userId");
            roleIdList = parameters.GetValue<List<int>>("roles");// 用户已有的角色ID
        }

        private List<int> roleIdList = new List<int>();
        private int userId = 0;
        public ObservableCollection<RoleModel> Roles { get; set; } = new ObservableCollection<RoleModel>();

        IRoleBLL _roleBLL;
        IUserBLL _userBLL;
        public ModifyRolesDialogViewModel(IRoleBLL roleBLL, IUserBLL userBLL)
        {
            _roleBLL = roleBLL;
            _userBLL = userBLL;

            this.InitRoles();
        }

        private async void InitRoles()
        {
            var roles = await _roleBLL.GetAll();// 获取系统数据库中所有可用的角色
            Roles.Clear();
            roles.ForEach(r => Roles.Add(new RoleModel
            {
                RoleId = r.roleId,
                RoleName = r.roleName,
                IsSelected = roleIdList.Contains(r.roleId)
            }));
        }

        public ICommand ConfirmCommand
        {
            get => new DelegateCommand(async () =>
            {
                // 保存当前用户的角色信息
                if (userId > 0)
                {
                    var roles = this.Roles.Where(r => r.IsSelected).ToList().Select(r => r.RoleId).ToList();
                    bool result = await _userBLL.SaveRoles(
                        new Entity.UserEntity
                        {
                            userId = userId,
                            roles = roles.Select(r => new RoleEntity { roleId = r }).ToList()
                        });

                    if (result)
                        RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                    else
                    {
                        // 提示异常信息
                    }
                }
            });
        }

        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        public DialogCloseListener RequestClose { get; }

    }
}