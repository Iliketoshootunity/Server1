using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Controller;
using GameServer.DBModel;

namespace GameServer
{
    class Program
    {
        private static string m_Ip = "192.168.1.4";
        private static int m_Point = 1038;
        private static Socket m_ServerSocker;

        /// <summary>
        /// 初始化所有控制器
        /// </summary>
        static void InitAllCtroller()
        {
            RoleCtroller.Instance.Init();
        }
        static void Main(string[] args)
        {
            InitAllCtroller();
            byte[] buffer = Encoding.UTF8.GetBytes("我来了，我征服");
            Console.WriteLine(buffer[0] + "," + buffer[buffer.Length - 1] + "," + buffer.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
            //服务器socker
            m_ServerSocker = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定ip端口
            m_ServerSocker.Bind(new IPEndPoint(IPAddress.Parse(m_Ip), m_Point));
            //开启今天连接
            m_ServerSocker.Listen(3000);
            Console.WriteLine("服务器开启监听接受");
            Thread accept = new Thread(ListenClientAccept);
            accept.Start();
            RoleCtroller.Instance.Test();
            Console.ReadLine();

        }

        private static void ListenClientAccept()
        {
            while (true)
            {
                Socket scoket = m_ServerSocker.Accept();
                Console.WriteLine(string.Format("客户端{0}连接", scoket.RemoteEndPoint));
                Role role = new Role();
                ClientSocket cs = new ClientSocket(scoket, role);
                RoleMgr.Instance.Roles.Add(role);

            }
        }
    }
}
