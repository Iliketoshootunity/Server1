

public class ProtoCodeDef  {


    public const ushort TestProto = 1;

    /// <summary>
    /// 登录服务器协议编码
    /// </summary>
    public const ushort LogOnGameServerProto = 10001;

    /// <summary>
    /// 服务器返回登录服务器结果——角色列表
    /// </summary>
    public const ushort LogOnGameServerReturnProto = 10002;

    /// <summary>
    /// 创建角色协议
    /// </summary>
    public const ushort CreateRoleProto = 10003;

    /// <summary>
    /// 创建角色结果服务器返回协议
    /// </summary>
    public const ushort CreateRoleReturnProto = 10004;

    /// <summary>
    /// 进入游戏
    /// </summary>
    public const ushort EnterGameProto = 10005;

    /// <summary>
    /// 进入游戏结果服务器返回协议
    /// </summary>
    public const ushort EnterGameReturnProto = 10006;

    /// <summary>
    /// 删除角色
    /// </summary>
    public const ushort DeleteRoleProto = 10007;

    /// <summary>
    /// 删除角色结果服务器返回协议
    /// </summary>
    public const ushort DeleteRoleReturnProto = 10008;

    /// <summary>
    /// 角色信息结果服务器返回协议
    /// </summary>
    public const ushort SelectRoleInfoReturnProto = 10009;

    /// <summary>
    /// 角色技能信息结果服务器返回协议
    /// </summary>
    public const ushort SkillReturnReturnProto = 10010;
}
