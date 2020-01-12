using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class RoleMgr
    {

        private List<Role> roles;
        public List<Role> Roles { get { return roles; } }

        #region 单例
        private static object lock_Obj = new object();

        private static RoleMgr _instance;

        public static RoleMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lock_Obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new RoleMgr();
                        }
                    }
                }
                return _instance;
            }
        }

        private RoleMgr()
        {
            roles = new List<Role>();
        }
        #endregion
    }
}
