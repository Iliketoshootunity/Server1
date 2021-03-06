﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class RoleSkillEntity : MFAbstractEntity
{
    #region 重写基类属性
    /// <summary>
    /// 主键
    /// </summary>
    public override int? PKValue
    {
        get
        {
            return this.Id;
        }
        set
        {
            this.Id = value;
        }
    }
    #endregion

    #region 实体属性

    /// <summary>
    /// 编号
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public EnumEntityStatus Status { get; set; }

    /// <summary>
    ///所属角色ID 
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///技能编号 
    /// </summary>
    public int SkillId { get; set; }

    /// <summary>
    ///技能等级 
    /// </summary>
    public int SkillLevel { get; set; }

    /// <summary>
    ///技能槽编号（1，2，3） 没有插入技能槽 则值为-1  
    /// </summary>
    public byte SlotsNO { get; set; }

    /// <summary>
    ///创建时间 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///更新时间 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    #endregion
}
