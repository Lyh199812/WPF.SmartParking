﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core.TemplateMatch
{
    /// <summary>
    /// 模板匹配类型
    /// </summary>
    public enum TemplateMatchType
    {
        ShapeModel,//形状匹配
        NccModel,//灰度匹配
        DeformableShapeModel,//形变匹配
    }
}