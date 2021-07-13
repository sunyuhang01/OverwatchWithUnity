using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 统一解决UI显示上的一些问题
/// </summary>
public static class UIHelper
{
    public static float BiggerThenZero(float num)
    {
        if(num < 0)
        {
            return 0;
        }
        else
        {
            return num;
        }
    }
    /// <summary>
    /// 屏幕坐标转换为局部坐标
    /// </summary>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="width">屏幕宽度</param>
    /// <param name="height">屏幕高度</param>
    /// <returns></returns>
    public static Vector3 ScreenToLocalPoint(Vector3 screenPos, float width, float height)
    {
        screenPos.x -= width / 2;
        screenPos.y -= height / 2;
        screenPos.z = screenPos.z > 0 ? screenPos.z : 0;
        return screenPos;
    }
}
