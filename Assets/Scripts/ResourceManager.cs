using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理资源
/// </summary>
public static class ResourceManager
{
    private static Dictionary<string, GameObject> bullets;
    private static Dictionary<string, GameObject> uis;
    static ResourceManager()
    {
        InitDic(out bullets, "Prefabs/Weapons/Bullets");
        InitDic(out uis, "Prefabs/UI");
    }
    /// <summary>
    /// 获取子弹预制件
    /// </summary>
    /// <returns></returns>
    public static GameObject GetBulletPrefab(string bulletName)
    {
        return bullets[bulletName];
    }
    public static GameObject GetUIPrefab(string uiName)
    {
        return uis[uiName];
    }
    /// <summary>
    /// 初始化字典
    /// </summary>
    /// <param name="dic">字典对象</param>
    /// <param name="path">获取资源路径</param>
    private static void InitDic(out Dictionary<string, GameObject> dic, string path)
    {
        dic = new Dictionary<string, GameObject>();
        var loadedResources = Resources.LoadAll<GameObject>(path);
        foreach (var element in loadedResources)
        {
            dic.Add(element.name, element);
        }
    }
}
