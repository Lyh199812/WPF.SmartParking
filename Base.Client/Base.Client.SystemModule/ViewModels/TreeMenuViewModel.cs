﻿using Base.Client.Common;
using Base.Client.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Unity;
using Base.Client.Common;
using Base.Client.IBLL;
using Base.Client.SystemModule.Models;
using Base.Client.BLL;

namespace Base.Client.SystemModule.ViewModels
{
    public class TreeMenuViewModel
    {
        // 需要多数据库的单列模式的数据转换成树状结构     递归
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();

        //public MenuItemModel RootMenu { get; set; } = new MenuItemModel();


        List<MenuEntity> origMenus { get; set; }

        private IRegionManager _regionManager = null;
        public TreeMenuViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            // 需要初始化的进候进行菜单数据的获取填充
            // 通过Api接口

            Task.Run(async () =>
            {
                string Model = "LocalMode";
                if (Model == "LocalMode")
                {
                     origMenus = new List<MenuEntity>
                    {
                        new MenuEntity
                        {
                            menuId = 10,
                            menuHeader = "系统维护",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue643",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                    
                        new MenuEntity
                        {
                            menuId = 100,
                            menuHeader = "升级文件上传",
                            targetView = "FileUploadView",
                            parentId = 10,
                            menuIcon = string.Empty,
                            index = 1,
                            menuType = 1,
                            state = 1
                        },
                    
                        new MenuEntity
                        {
                            menuId = 101,
                            menuHeader = "菜单管理",
                            targetView = "MenuManagementView",
                            parentId = 10,
                            menuIcon = string.Empty,
                            index = 2,
                            menuType = 1,
                            state = 1
                        },
                    
                        new MenuEntity
                        {
                            menuId = 102,
                            menuHeader = "系统用户",
                            targetView = "UserManagementView",
                            parentId = 10,
                            menuIcon = string.Empty,
                            index = 3,
                            menuType = 1,
                            state = 1
                        },

                        //配方管理
                        new MenuEntity
                        {
                            menuId = 20,
                            menuHeader = "机器视觉",
                            targetView = "",
                            parentId = 0,
                            menuIcon = "\ue646",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                        //预处理
                        new MenuEntity
                        {
                            menuId = 200,
                            menuHeader = "预处理",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue78f",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                        new MenuEntity
                        {
                            menuId = 2000,
                            menuHeader = "镜像反转",
                            targetView = "MirrorView",
                            parentId = 200,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        }
                        ,
                        new MenuEntity
                        {
                            menuId = 2001,
                            menuHeader = "缩放因子设置",
                            targetView = "ZoomFactorView",
                            parentId = 200,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },

                        //模板匹配
                          new MenuEntity
                        {
                            menuId = 300,
                            menuHeader = "模板匹配",
                            targetView = "",
                            parentId = 20,
                            menuIcon = "\ue600",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                          new MenuEntity
                        {
                            menuId = 3000,
                            menuHeader = "形状模板创建",
                            targetView = "ShapeTemplateCreatorView",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                           new MenuEntity
                        {
                            menuId = 3001,
                            menuHeader = "形状模板搜索",
                            targetView = "ShapeTemplateSearcherView",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                           
                         new MenuEntity
                        {
                            menuId = 3002,
                            menuHeader = "NCC模板创建",
                            targetView = "NCCTemplateCreatorView",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                          new MenuEntity
                        {
                            menuId = 3003,
                            menuHeader = "NCC模板搜索",
                            targetView = "NCCTemplateSearcherView",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                           new MenuEntity
                        {
                            menuId = 3003,
                            menuHeader = "形变模板创建",
                            targetView = "",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                          new MenuEntity
                        {
                            menuId = 3004,
                            menuHeader = "形变模板搜索",
                            targetView = "",
                            parentId = 300,
                            menuIcon = "",
                            index = 0,
                            menuType = 0,
                            state = 1
                        },
                    };
                    unityContainer.Resolve<Dispatcher>().Invoke(() =>
                    {
                        this.FillMenus(Menus, 0);
                    });
                }
                else
                {
                    origMenus = unityContainer.Resolve<GlobalValue>().UserInfo.menus;
                    // origMenus = await menuBLL.GetMenus(0);// 通过接口获取 菜单 数据       

                    unityContainer.Resolve<Dispatcher>().Invoke(() =>
                    {
                        this.FillMenus(Menus, 0);
                    });
                }
      
            });

            //origMenus = menuBLL.GetMenus(0).GetAwaiter().GetResult();
            //this.FillMenus(Menus, 0);
        }

        ///递归
        ///
        private void FillMenus(ObservableCollection<MenuItemModel> menus, int parentId)
        {
            // 根据菜单ID查找对应的直接子节点，第一层的菜单，父节点为0
            var sub = origMenus.Where(m => m.parentId == parentId).OrderBy(o => o.index);

            if (sub.Count() > 0)
            {
                foreach (var item in sub)
                {
                    //MenuItemModel mm = new MenuItemModel(_regionManager)
                    MenuItemModel mm = new MenuItemModel(_regionManager)
                    {
                        MenuHeader = item.menuHeader,
                        MenuIcon = item.menuIcon,
                        TargetView = item.targetView
                    };
                    menus.Add(mm);

                    FillMenus(mm.Children, item.menuId);
                }
            }
        }
    }
}
