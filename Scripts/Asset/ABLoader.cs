/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #ABLoader# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 
 *Date:         #2017-10-20# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void delloadprogress(string p_bundlename,float p_progress);
public delegate void delloaddone(string p_bundlename);
/// <summary>
/// 从服务器或者从硬盘加载资源类
/// </summary>
public class ABLoader:IDispose{
    /// <summary>
    /// 资源包名字 
    /// </summary>
    private string mabname;
    /// <summary>
    /// 资源包路径
    /// </summary>
    private string mabpath;
    /// <summary>
    /// www加载器
    /// </summary>
    private WWW abloader;
    /// <summary>
    /// 加载进度
    /// </summary>
    private float loadprogressnum = 0;
    private delloadprogress loadProgressFuc;
    private delloaddone loadDoneFuc;
    private ABResLoader resloader;
    /// <summary>
    /// 负责将资源包从硬盘或者网络中下载到内存
    /// </summary>
    /// <param name="p_progress"></param>
    /// <param name="p_done"></param>
    public ABLoader(delloadprogress p_progress,delloaddone p_done)
    {
        mabname = "";
        mabpath = "";
        loadprogressnum = 0;
        loadProgressFuc = p_progress;
        loadDoneFuc = p_done;
    }

    public void SetBundleName(string p_name)
    {
        mabname = p_name;
    }

    /// <summary>
    /// 加载资源资源包
    /// </summary>
    public void LoadABPack(string p_fullpath)
    {
        mabpath = p_fullpath;
    }

    /// <summary>
    /// 加载进度协程
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadCoro()
    {
        abloader = new WWW(mabpath);
        //加载没有结束
        while (!abloader.isDone)
        {
            loadprogressnum = abloader.progress;
            if(this.loadProgressFuc != null)
            {
                this.loadProgressFuc(this.mabname,this.loadprogressnum);
            }
            yield return loadprogressnum;
            loadprogressnum = abloader.progress;
        }

        //加载结束
        if (loadprogressnum > 1.0f)
        {
            if (this.loadProgressFuc != null)
            {
                this.loadProgressFuc(this.mabname, this.loadprogressnum);
            }

            if(this.loadDoneFuc != null)
            {
                this.loadDoneFuc(this.mabname);
                //加载结束后把bundle传入资源包封包中
                resloader = new ABResLoader(abloader.assetBundle);
            }
            
        }
        else
        {
            Debug.LogError("加载没有完成，未知错误。" );
        }
        abloader = null;
    }
    /// <summary>
    /// 得到一个资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object GetOneResource(string p_name)
    {
        if (this.resloader == null) return null;
        return this.resloader.LoadOneRes(p_name);
    }
    /// <summary>
    /// 得到一个资源的所有的子资源
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public UnityEngine.Object[] GetMutiResources(string p_name)
    {
        if (this.resloader == null) return null;
        return this.resloader.LoadResources(p_name);
    }
    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="p_obj"></param>
    public void UnLoadResource(UnityEngine.Object p_obj)
    {
        if (this.resloader!=null)
        {
            this.resloader.UnLoadRes(p_obj);
        }
    }
    /// <summary>
    /// 销毁
    /// </summary>
    public void Dispose()
    {
        if (this.resloader != null)
        {
            this.resloader.Dispose();
            this.resloader = null;
        }
    }
    /// <summary>
    /// 显示assetsbundle中所有的资源名字
    /// </summary>
    public void ShowAllAssetsNames()
    {
        if (resloader != null)
        {
            resloader.showAllNameInBundle();
        }
    }
}
