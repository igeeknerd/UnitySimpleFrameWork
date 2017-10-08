
using System;
/** 
*Copyright(C) 201x by #John.W# 
*All rights reserved. 
*FileName:     #ManagerBase# 
*Author:       #John.W# 
*Version:      #0.10# 
*UnityVersion：#2017-1# 
*Date:         #2017-10-8# 
*Description:  A Simple Unity Muti-Game FrameWork  
*History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理脚本的链表节点
/// </summary>
public class EventNode
{
    //节点承载的数据
    public MonoBase data;
    //下一个节点
    public EventNode next;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="p_node">本节点中包含的数据</param>
    public EventNode(MonoBase p_node)
    {
        this.data = p_node;
        this.next = null;
    }
}

/// <summary>
/// 管理消息的类
/// </summary>
public class ManagerBase : MonoBase {
    //管理消息节点的字典
    public Dictionary<ushort, EventNode> evtDic = new Dictionary<ushort, EventNode>();
    /// <summary>
    /// 注册一个事件，如果事件字典中已包含消息id，在尾部挂载
    /// </summary>
    /// <param name="p_id">id</param>
    /// <param name="p_node">节点</param>
    public void RegisterMsg(ushort p_id,EventNode p_node)
    {
        if (!evtDic.ContainsKey(p_id))
        {
            evtDic.Add(p_id,p_node);
        }
        else
        {
            EventNode tmpnode = evtDic[p_id];
            while (tmpnode.next != null)
            {
                tmpnode = tmpnode.next;
            }
            //链表的尾挂载上新节点
            tmpnode.next = p_node;
        }
    }
    /// <summary>
    /// 注册多个消息，把脚本挂载到事件中，注册到事件树中
    /// </summary>
    /// <param name="p_base"></param>
    /// <param name="msgids"></param>
    public void RegisterMsgs(MonoBase p_base,ushort[] msgids)
    {
        for (int i = 0;i < msgids.Length;i++)
        {
            EventNode tmpnode = new EventNode(p_base);
            //调用注册单个节点
            RegisterMsg(msgids[i],tmpnode);
        }

    }
    /// <summary>
    /// 删除指定id 的节点
    /// </summary>
    /// <param name="p_id"></param>
    /// <param name="p_base"></param>
    public void UnRegisterMsg(ushort p_id,MonoBase p_base)
    {
        if (!evtDic.ContainsKey(p_id))
        {
            Debug.LogWarning("删除当前id" + p_id + "不存在.");
            return;
        }
        else
        {
            EventNode tmpnode = evtDic[p_id];
            if(tmpnode.data == p_base)
            {
                EventNode headnode = tmpnode;
                if(tmpnode.next != null)
                {
                    tmpnode.data = tmpnode.next.data;
                    tmpnode.next = tmpnode.next.next;
                }
                else//删除头
                {
                    evtDic.Remove(p_id);
                }
            }
            else
            {
                //可能有问题回头再看
                while(tmpnode.next != null && tmpnode.next.data != null&&tmpnode.data != p_base)
                {
                    tmpnode = tmpnode.next;
                }

                if (tmpnode.next.next!=null)
                {
                    tmpnode.next = tmpnode.next.next;
                }
                else
                {
                    tmpnode.next = null;
                }
            }
        }
    }
    /// <summary>
    /// 删除一组节点
    /// </summary>
    /// <param name="p_ids"></param>
    /// <param name="p_base"></param>
    public void UnRegisterMsgs(ushort[] p_ids,MonoBase p_base)
    {
        for (int i=0;i < p_ids.Length; i++)
        {
            this.UnRegisterMsg(p_ids[i],p_base);
        }
    }
    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="p_msg">需要处理的消息</param>
    public override void ProcessEvent(MsgBase p_msg)
    {
        if (!evtDic.ContainsKey(p_msg.msgId))
        {
            Debug.LogError("当前消息不存在 id" + p_msg.msgId + " manager" + p_msg.GetManagerId());
            return;
        }
        else
        {
            //取出链表
            EventNode tmpnode = evtDic[p_msg.msgId];
            do
            {
                tmpnode.data.ProcessEvent(p_msg);
            } while (tmpnode != null);
        }
    }

}
