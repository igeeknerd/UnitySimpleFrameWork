/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #ABScenePack# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 
 *Date:         #2017.10.25# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 场景管理Assetsbundle类
/// </summary>
public class ABScenePack  {
    /// <summary>
    /// 场景下所有assetbundle包
    /// </summary>
    Dictionary<string,ABRelationPack> packDic = new Dictionary<string,ABRelationPack>();

    Dictionary<string, AssetObjectGroup> objASDic = new Dictionary<string, AssetObjectGroup>();
    /// <summary>
    /// 显示包中所有资源名字
    /// </summary>
    /// <param name="p_name"></param>
    public void ShowAllAssetsNames(string p_name)
    {
        ABRelationPack tmppack;
        if (packDic.ContainsKey(p_name))
        {
            tmppack = packDic[p_name];
            tmppack.ShowAllAssetsNames();
        }
    }

    public bool IsLoadFinish(string p_name)
    {
        if (packDic.ContainsKey(p_name))
        {
            ABRelationPack tmppack = packDic[p_name];
            return tmppack.IsFinsh();
        }
        else
        {
            Debug.Log("关系包" + p_name +"不存在。");
            return false;
        }
    }

    public bool IsLoadingABPack(string p_name)
    {
        if (!packDic.ContainsKey(p_name))
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    //得到bundle中的一个资源
    public UnityEngine.Object GetOneResource(string p_bundlename,string p_resname)
    {
        //如果缓存中含有资源，直接取出
        if (objASDic.ContainsKey(p_bundlename))
        {
            AssetObjectGroup tmpgroup = objASDic[p_bundlename];
            List<UnityEngine.Object> tmpobj = tmpgroup.GetReourceObject(p_resname);
            if (tmpobj != null)
            {
                return tmpobj[0];
            }
        }

        //查看是否加载过资源包
        if (packDic.ContainsKey(p_bundlename))
        {
            ABRelationPack tmppack = packDic[p_bundlename];
            UnityEngine.Object tmpobj = tmppack.GetSingleResource(p_resname);
            AssetObject tmpassobj = new AssetObject(tmpobj);

            //缓存中有这个包，直接添加资源
            if (objASDic.ContainsKey(p_bundlename))
            {
                AssetObjectGroup tmpgroup = objASDic[p_bundlename];
                tmpgroup.AddObject(p_resname, tmpassobj);
            }
            //如果没有这个包，初始化组，在字典中添加组
            else
            {
                AssetObjectGroup tmpgroup = new AssetObjectGroup(p_resname, tmpassobj);
                objASDic.Add(p_bundlename, tmpgroup);
            }

            return tmpobj;
        }
        return null;
    }
    //得到bundle中的多个资源
    public UnityEngine.Object[] GetMultiResources(string p_bundlename, string p_resname)
    {
        //如果缓存中含有资源，直接取出
        if (objASDic.ContainsKey(p_bundlename))
        {
            AssetObjectGroup tmpgroup = objASDic[p_bundlename];
            List<UnityEngine.Object> tmpobj = tmpgroup.GetReourceObject(p_resname);
            if (tmpobj != null)
            {
                return tmpobj.ToArray();
            }
        }

        //查看是否加载过资源包
        if (packDic.ContainsKey(p_bundlename))
        {
            ABRelationPack tmppack = packDic[p_bundlename];
            UnityEngine.Object[] tmpobj = tmppack.GetMultiResources(p_resname);
            AssetObject tmpassobj = new AssetObject(tmpobj);

            //缓存中有这个包，直接添加资源
            if (objASDic.ContainsKey(p_bundlename))
            {
                AssetObjectGroup tmpgroup = objASDic[p_bundlename];
                tmpgroup.AddObject(p_resname, tmpassobj);
            }
            //如果没有这个包，初始化组，在字典中添加组
            else
            {
                AssetObjectGroup tmpgroup = new AssetObjectGroup(p_resname, tmpassobj);
                objASDic.Add(p_bundlename, tmpgroup);
            }

            return tmpobj;
        }
        return null;
    }
    /// <summary>
    /// 删除缓存中的一个资源
    /// </summary>
    /// <param name="p_bundlename"></param>
    /// <param name="p_resname"></param>
    public void DisposeOneRes(string p_bundlename,string p_resname)
    {
        if (objASDic.ContainsKey(p_bundlename))
        {
            AssetObjectGroup tmpgroup = objASDic[p_bundlename];
            tmpgroup.ReleaseOneObject(p_resname);
        }
    }
    /// <summary>
    /// 删除缓存中的多个资源
    /// </summary>
    /// <param name="p_bundlename"></param>
    public void DisposeAllRes(string p_bundlename)
    {
        if (objASDic.ContainsKey(p_bundlename))
        {
            AssetObjectGroup tmpgroup = objASDic[p_bundlename];
            tmpgroup.ReleaseAllObject();
        }

        Resources.UnloadUnusedAssets();
    }
    /// <summary>
    /// 删除缓存中的所有资源
    /// </summary>
    public void DisposeAllBundle()
    {
        List<string> keys = new List<string>();
        keys.AddRange(objASDic.Keys);
        for (int i = 0;i < keys.Count;i++)
        {
            DisposeAllRes(keys[i]);
        }
        objASDic.Clear();
    }
}


class AssetObject
{
    public List<UnityEngine.Object> objs;
    public AssetObject(params UnityEngine.Object[] tmpobj)
    {
        objs = new List<UnityEngine.Object>();
        objs.AddRange(tmpobj);
    }

    public void UnloadObject()
    {
        for (int i =0;i < objs.Count;i++)
        {
            Resources.UnloadAsset(objs[i]);
        }
    }
}


class AssetObjectGroup
{
    public Dictionary<string, AssetObject> objDic;

    public AssetObjectGroup(string p_name,AssetObject p_obj)
    {
        objDic = new Dictionary<string, AssetObject>();
        objDic.Add(p_name,p_obj);
    }

    public void AddObject(string p_name,AssetObject p_obj)
    {
        objDic.Add(p_name,p_obj);
    }

    public void ReleaseAllObject()
    {
        List<string> list = new List<string>();

        /**foreach (string key in objDic.Keys)
        {
            list.Add(key);
        }**/
        list.AddRange(objDic.Keys);
        for (int i = 0;i < list.Count;i++)
        {
            ReleaseOneObject(list[i]);
        }
    }

    public void ReleaseOneObject(string p_name)
    {
        if (objDic.ContainsKey(p_name))
        {
            AssetObject obj = objDic[p_name];
            obj.UnloadObject();
        }
        else
        {
            Debug.Log("当前需要卸载资源不存在");
        }
    }

    public List<UnityEngine.Object> GetReourceObject(string p_name)
    {
        if (objDic.ContainsKey(p_name))
        {
            AssetObject obj = objDic[p_name];
            return obj.objs;
        }
        else
        {
            return null;
        }
    }

}
