/// <summary>
/// 类名 : RoleEntity
/// 作者 : 北京-边涯
/// 说明 : 
/// 创建日期 : 2018-11-08 15:57:04
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class RoleEntity : MFAbstractEntity
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
    ///所属账号Id 
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    ///职业编号 
    /// </summary>
    public int JobId { get; set; }

    /// <summary>
    ///昵称 
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///性别 
    /// </summary>
    public byte Sex { get; set; }

    /// <summary>
    ///等级 
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    ///创建时间 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///更新时间 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    ///金币 
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    ///元宝 
    /// </summary>
    public int Money { get; set; }

    /// <summary>
    ///经验值 
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    ///最大生命值 
    /// </summary>
    public int MaxHP { get; set; }

    /// <summary>
    ///最大魔法值 
    /// </summary>
    public int MaxMP { get; set; }

    /// <summary>
    ///当前生命值 
    /// </summary>
    public int CurrentHP { get; set; }

    /// <summary>
    ///当前魔法值 
    /// </summary>
    public int CurrentMP { get; set; }

    /// <summary>
    ///攻击力 
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    ///命中率 
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    ///防御力 
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    ///闪避率 
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    ///暴击率 
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    ///抗性 
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    ///最后进入的世界地图场景ID 
    /// </summary>
    public int LastWorldMapSceneId { get; set; }

    /// <summary>
    ///最后通关的关卡Id 
    /// </summary>
    public int LastPassGameLevleId { get; set; }

    /// <summary>
    ///战斗力 
    /// </summary>
    public int Fighting { get; set; }

    #endregion
}
