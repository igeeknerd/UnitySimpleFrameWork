/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #ABRelationPack# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 
 *Date:         #2017-10-22# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// assetbundle关系包
/// </summary>
public class ABRelationPack:IDispose{
    /// <summary>
    /// 依赖和需要的包
    /// </summary>
    List<string> NeedABNames;
    /// <summary>
    /// 被依赖和需要的包
    /// </summary>
    List<string> NeededABNames;
    ABLoader assloader;
    // Use this for initialization
    /// <summary>
    /// ASSETBUNDLE关系包
    /// </summary>
    public ABRelationPack()
    {
        NeedABNames = new List<string>();
        NeededABNames = new List<string>();
    }
    /// <summary>
    /// 加载结束
    /// </summary>
    /// <param name="p_name"></param>
    private void LoadFinish(string p_name)
    {
        isFinish = true;
    }
    /// <summary>
    /// 是否加载结束
    /// </summary>
    private bool isFinish;
    /// <summary>
    /// 加载结束
    /// </summary>
    /// <returns></returns>
    public bool IsFinsh()
    {
        return isFinish;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="p_progress"></param>
    public void Init(delloadprogress p_progress)
    {
        isFinish = false;
        assloader = new ABLoader(p_progress, LoadFinish);
    }
    /// <summary>
    /// 被依赖的包名
    /// </summary>
    /// <param name="p_name"></param>
    public void AddNeededABName(string p_name)
    {
        this.NeededABNames.Add(p_name);
    }
    /// <summary>
    /// 依赖的包名
    /// </summary>
    /// <param name="p_name"></param>
    public void AddNeedABName(string p_name)
    {
        this.NeedABNames.Add(p_name);
    }
    /// <summary>
    /// 得到所有被依赖的包名
    /// </summary>
    /// <returns></returns>
    public List<string> GetNeededNames()
    {
        return this.NeededABNames;
    }
    /// <summary>
    /// 得到依赖的包名
    /// </summary>
    /// <returns></returns>
    public List<string> GetNeedNames()
    {
        return this.NeedABNames;
    }
    /// <summary>
    /// 删除被依赖包名
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public bool RemoveNeededName(string p_name)
    {
        for(int i=0;i < this.NeededABNames.Count; i++)
        {
            if (this.NeededABNames[i].Equals(p_name))
            {
                this.NeededABNames.RemoveAt(i);
                break;
            }
        }

        if(this.NeededABNames.Count <= 0)
        {
            Dispose();
            return true;
        }

        return false;
    }
    /// <summary>
    /// 删除依赖的包名
    /// </summary>
    /// <param name="p_name"></param>
    public void RemoveNeedName(string p_name)
    {
        for (int i = 0; i < this.NeedABNames.Count; i++)
        {
            if (this.NeedABNames[i].Equals(p_name))
            {
                this.NeedABNames.RemoveAt(i);
                break;
            }
        }

    }
    /// <summary>
    /// 得到一个资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object GetSignleResource(string p_name)
    {
        return assloader.GetOneResource(p_name);
    }
    /// <summary>
    /// 得到多个资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object[] GetMultiResources(string p_name)
    {
        return assloader.GetMutiResources(p_name);
    }
    /// <summary>
    /// 协程加载进度
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadCoro()
    {
        yield return assloader.LoadCoro();
    }
    /// <summary>
    /// 销毁进度
    /// </summary>
    public void Dispose()
    {
        assloader.Dispose();
    }
}
