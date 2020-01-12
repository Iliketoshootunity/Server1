using GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EventDispatcher : Singleton<EventDispatcher>
{
    public delegate void OnActionHandler(Role role ,byte[] content);

    public Dictionary<ushort, List<OnActionHandler>> m_Dic = new Dictionary<ushort, List<OnActionHandler>>();


    public void AddEventListen(ushort protoCode, OnActionHandler handler)
    {
        if (!m_Dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> list = new List<OnActionHandler>();
            list.Add(handler);
            m_Dic.Add(protoCode, list);
        }
        else
        {
            m_Dic[protoCode].Add(handler);
        }
    }

    public void RemoveEventListen(ushort protoCode, OnActionHandler handler)
    {
        if (m_Dic.ContainsKey(protoCode))
        {
            m_Dic[protoCode].Remove(handler);
            if (m_Dic[protoCode].Count == 0)
            {
                m_Dic.Remove(protoCode);
            }
        }

    }

    public void Dispatc(ushort protoCode, Role role, byte[] buffer)
    {
        if (m_Dic.ContainsKey(protoCode))
        {

            List<OnActionHandler> list = m_Dic[protoCode];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    list[i](role, buffer);
                }
            }
        }
    }
}

