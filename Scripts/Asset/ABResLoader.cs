/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #ABResLoader# 
 *Author:       #John.Wu# 
 *Version:      #0.10# 
 *UnityVersion：#2017-1# 
 *Date:         #2017-10-20# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

/// <summary>
/// 从资源包中加载资源和卸载资源
/// </summary>
public class ABResLoader:IDisposable{
    private AssetBundle mbundle;
    public ABResLoader(AssetBundle p_bundle)
    {
        this.mbundle = p_bundle;
    }
    /// <summary>
    /// 从资源包中加载一个资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadOneRes(string p_name)
    {
        if (this.mbundle == null || !this.mbundle.Contains(p_name))
        {
            Debug.Log("资源" + p_name +"不存在");
            return null;
        }
        return mbundle.LoadAsset(p_name);
    }
    /// <summary>
    /// 从资源包中加载资源和子资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadResources(string p_name)
    {
        if (this.mbundle == null || !this.mbundle.Contains(p_name))
        {
            Debug.Log("资源" + p_name + "不存在");
            return null;
        }
        return mbundle.LoadAssetWithSubAssets(p_name);
    }

    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="p_obj"></param>
    public void UnLoadRes(UnityEngine.Object p_obj)
    {
        Resources.UnloadAsset(p_obj);
    }

    /// <summary>
    /// 销毁加载过的资源
    /// </summary>
    public void Dispose()
    {
        if(this.mbundle == null){
            return;
        }
        mbundle.Unload(false);
    }
    /// <summary>
    /// 显示资源包的名字
    /// </summary>
    public void showAllNameInBundle()
    {
        string[] anames = mbundle.GetAllAssetNames();
        StringBuilder sb = new StringBuilder();
        for (int i = 0;i < anames.Length;i++)
        {
            sb.Append(anames[i]);
            sb.Append("|");
        }
        Debug.Log("资源包 " + anames +" 中包含 : " +sb.ToString() );
    }

    
}
