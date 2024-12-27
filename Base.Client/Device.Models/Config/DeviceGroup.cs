﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Device.Models.Config;

namespace Device.Models.Config
{
    public class DeviceGroup
    {
        /// <summary>
        /// 通信组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 起始地址
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public ushort Length { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 存储区
        /// </summary>
        public string StoreArea { get; set; }

        /// <summary>
        /// 变量集合
        /// </summary>
        public List<DeviceVariable> VarList { get; set; }
    }
}
