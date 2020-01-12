using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller
{
    public class RoleCtroller : Singleton<RoleCtroller>
    {
        public void Init()
        {
            EventDispatcher.Instance.AddEventListen(ProtoCodeDef.LogOnGameServerProto, LogOnGameServerProtoCallBack);
            EventDispatcher.Instance.AddEventListen(ProtoCodeDef.CreateRoleProto, CreateRoleProtoCallBack);
            EventDispatcher.Instance.AddEventListen(ProtoCodeDef.EnterGameProto, EnterGameoCallBack);
            EventDispatcher.Instance.AddEventListen(ProtoCodeDef.DeleteRoleProto, DeleteRoleCallBack);
        }

        private void DeleteRoleCallBack(Role role, byte[] content)
        {
            RoleOperation_DeleteRoleProto proto = RoleOperation_DeleteRoleProto.ToProto(content);
            IDictionary<string, object> dic = new Dictionary<string, object>();

            dic["Id"] = proto.RoleID;
            dic["Status"] = (byte)EnumEntityStatus.Deleted;
            dic["UpdateTime"] = DateTime.Now; ;
            MFReturnValue<object> ret = RoleCacheModel.Instance.Update("[Status]=@Status,[UpdateTime]=@UpdateTime", "Id=@Id", dic);

            DeleteRoleReturn(role, ret);
        }

        private void DeleteRoleReturn(Role role, MFReturnValue<object> ret)
        {
            RoleOperation_DeleteRoleReturnProto proto = new RoleOperation_DeleteRoleReturnProto();
            if (ret.HasError)
            {
                proto.IsSucess = false;
            }
            else
            {
                proto.IsSucess = true;
            }
            role.Client_Socket.SendMsg(proto.ToArray());
        }
        private void EnterGameoCallBack(Role role, byte[] content)
        {
            RoleOperation_EnterGameProto proto = RoleOperation_EnterGameProto.ToProto(content);
            role.CurRoleId = proto.RoleID;
            EnterGameoReturn(role);
            SelectRoleInfoReturn(role);
            SelectSkillReturn(role);
        }

        private void EnterGameoReturn(Role role)
        {
            RoleOperation_EnterGameReturnProto proto = new RoleOperation_EnterGameReturnProto();
            proto.IsSucess = true;
            role.Client_Socket.SendMsg(proto.ToArray());
        }

        /// <summary>
        /// 朝朝角色信息
        /// </summary>
        /// <param name="role"></param>
        private void SelectRoleInfoReturn(Role role)
        {
            RoleOperation_SelectRoleInfoReturnProto proto = new RoleOperation_SelectRoleInfoReturnProto();
            RoleEntity entity = RoleCacheModel.Instance.GetEntity(role.CurRoleId);
            if (entity != null)
            {
                proto.IsSucess = true;
                proto.RoleId = entity.Id.Value;
                proto.RoleNickName = entity.NickName;
                proto.Level = entity.Level;
                proto.JobId = entity.JobId;
                proto.Money = entity.Money;
                proto.Gold = entity.Gold;
                proto.Exp = entity.Exp;
                proto.MaxHP = entity.MaxHP;
                proto.MaxMP = entity.MaxMP;
                proto.CurrentHP = entity.CurrentHP;
                proto.CurrentMP = entity.CurrentMP;
                proto.Attack = entity.Attack;
                proto.Defense = entity.Defense;
                proto.Hit = entity.Hit;
                proto.Dodge = entity.Dodge;
                proto.Cri = entity.Cri;
                proto.Res = entity.Res;
                proto.Fighting = entity.Fighting;
                proto.LastSceneId = entity.LastWorldMapSceneId;
            }
            else
            {
                proto.IsSucess = false;
                proto.MessageId = 1000;
            }
            role.Client_Socket.SendMsg(proto.ToArray());
        }

        /// <summary>
        /// 查找技能
        /// </summary>
        /// <param name="role"></param>
        private void SelectSkillReturn(Role role)
        {
            List<RoleSkillEntity> lst = RoleSkillDBModel.Instance.GetList(condition: string.Format("[RoleId] = {0}", role.CurRoleId));
            if (lst != null)
            {
                RoleData_SkillReturnProto proto = new RoleData_SkillReturnProto();
                proto.SkillCount = lst.Count;
                for (int i = 0; i < lst.Count; i++)
                {
                    RoleData_SkillReturnProto.SkillData skillData = new RoleData_SkillReturnProto.SkillData();
                    skillData.SkillId = lst[i].SkillId;
                    skillData.SKillLevel = lst[i].SkillLevel;
                    skillData.SlotsNO = lst[i].SlotsNO;
                    proto.Skills.Add(skillData);
                }
                role.Client_Socket.SendMsg(proto.ToArray());
            }

        }

        private void CreateRoleProtoCallBack(Role role, byte[] content)
        {
            RoleOperation_CreateRoleProto proto = RoleOperation_CreateRoleProto.ToProto(content);

            //查询是否又相同昵称的
            int count = RoleCacheModel.Instance.GetCount(string.Format("[NickName] = '{0}'", proto.NickName));
            MFReturnValue<object> ret = null;
            if (count == 0)
            {
                //创建
                RoleEntity entity = new RoleEntity();
                entity.JobId = proto.JobID;
                entity.NickName = proto.NickName;
                entity.Sex = (byte)EnumSex.Female;
                entity.Status = EnumEntityStatus.Released;
                entity.Level = 1;
                entity.AccountId = role.AccountId;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.LastWorldMapSceneId = 1;
                //暂定
                JobEntity jobEntity = JobDBModel.Instance.Get(proto.JobID);
                JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(entity.Level);
                entity.MaxHP = jobLevelEntity.HP;
                entity.CurrentHP = entity.MaxHP;
                entity.MaxMP = jobLevelEntity.MP;
                entity.CurrentMP = entity.MaxMP;
                entity.Attack = (int)Math.Round(jobEntity.Attack * jobLevelEntity.Attack * 0.01f);
                entity.Defense = (int)Math.Round(jobEntity.Defense * jobLevelEntity.Defense * 0.01f);
                entity.Hit = (int)Math.Round(jobEntity.Hit * jobLevelEntity.Hit * 0.01f);
                entity.Dodge = (int)Math.Round(jobEntity.Dodge * jobLevelEntity.Dodge * 0.01f);
                entity.Cri = (int)Math.Round(jobEntity.Cri * jobLevelEntity.Cri * 0.01f);
                entity.Res = (int)Math.Round(jobEntity.Res * jobLevelEntity.Res * 0.01f);
                entity.Gold = 0;
                entity.Money = 0;
                entity.Exp = 0;
                entity.Fighting = entity.Attack * 4 + entity.Defense * 4 + entity.Hit * 2 + entity.Dodge * 2 + entity.Res + entity.Cri;
                ret = RoleCacheModel.Instance.Create(entity);
                role.CurRoleId = (int)ret.OutputValues["Id"];
            }
            else
            {
                ret = new MFReturnValue<object>();
                ret.HasError = true;
                ret.ReturnCode = 1000;
            }
            SelectRoleInfoReturn(role);
            CreateRoleProtoReturn(ret, role);
            SelectSkillReturn(role);
        }

        private void CreateRoleProtoReturn(MFReturnValue<object> ret, Role role)
        {
            RoleOperation_CreateRoleReturnProto proto = new RoleOperation_CreateRoleReturnProto();
            if (ret.HasError)
            {
                proto.IsSucess = false;
                proto.MessageID = 100;
            }
            else
            {
                proto.IsSucess = true;
            }
            role.Client_Socket.SendMsg(proto.ToArray());
        }
        private void LogOnGameServerProtoCallBack(Role role, byte[] content)
        {
            RoleOpration_LogOnGameServerProto logOnProto = RoleOpration_LogOnGameServerProto.ToProto(content);
            int accountId = logOnProto.AccoutID;
            role.AccountId = accountId;
            LogonGameServerReturn(role, accountId);
        }

        public void Test()
        {

        }
        private void LogonGameServerReturn(Role role, int accountId)
        {
            RoleOperation_LogOnGameServerReturnProto retureProto = new RoleOperation_LogOnGameServerReturnProto();
            List<RoleEntity> list = RoleCacheModel.Instance.GetList(condition: string.Format("{0}={1}", "[AccountId]", accountId));
            if (list != null && list.Count > 0)
            {
                retureProto.RoleCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    RoleOperation_LogOnGameServerReturnProto.RoleItem item = new RoleOperation_LogOnGameServerReturnProto.RoleItem();
                    item.RoleId = list[i].Id.Value;
                    item.NickName = list[i].NickName;
                    item.Level = list[i].Level;
                    item.RoleJob = (byte)list[i].JobId;
                    retureProto.Roles.Add(item);
                }
            }
            else
            {
                retureProto.RoleCount = 0;
            }
            role.Client_Socket.SendMsg(retureProto.ToArray());
        }

        public override void Dispose()
        {
            base.Dispose();
            EventDispatcher.Instance.RemoveEventListen(ProtoCodeDef.LogOnGameServerProto, LogOnGameServerProtoCallBack);
            EventDispatcher.Instance.RemoveEventListen(ProtoCodeDef.CreateRoleProto, CreateRoleProtoCallBack);
            EventDispatcher.Instance.RemoveEventListen(ProtoCodeDef.EnterGameProto, EnterGameoCallBack);
            EventDispatcher.Instance.RemoveEventListen(ProtoCodeDef.DeleteRoleProto, DeleteRoleCallBack);
        }
    }
}
