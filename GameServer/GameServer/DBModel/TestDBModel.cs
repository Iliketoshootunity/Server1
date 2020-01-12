using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.DBModel
{
    public class TestDBModel : Singleton<TestDBModel>
    {
        public void Init()
        {
            EventDispatcher.Instance.AddEventListen(ProtoCodeDef.TestProto, OnTestProtoCallBack);
        }

        private void OnTestProtoCallBack(Role role, byte[] content)
        {
            TestProto proto = TestProto.GetProto(content);
            Console.WriteLine(string.Format("Id:{0}  Name{1}", proto.Id, proto.Name));

            proto.Id = 1002;
            proto.Name = "杨明";
            role.Client_Socket.SendMsg(proto.ToArray());
        }

        public override void Dispose()
        {
            EventDispatcher.Instance.RemoveEventListen(ProtoCodeDef.TestProto, OnTestProtoCallBack);
        }

    }
}
