using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 计时类，包含最大时间和当前时间
/// </summary>
public class Timer
{
    public float currentTime;
    public float maxTime;
    /// <summary>
    /// 是否开始计时
    /// </summary>
    public bool isAwake;
    public Timer(){
        Reset();
        SetMaxTime(0);
    }
    public Timer(float maxTime){
        Reset();
        SetMaxTime(maxTime);
    }

    public void Reset(){
        currentTime = 0;
        isAwake = false;
    }

    public void SetMaxTime(float maxTime){
        this.maxTime = maxTime;
    }

    public void AddTime(float time){
        currentTime += time;
    }
    public bool IsMax(){
        return currentTime >= maxTime;
    }
    /// <summary>
    /// 按时间更新数据
    /// </summary>
    /// <param name="time">更新时增加的时间</param>
    public void UpdateTimer(float time)
    {
        if (!isAwake)
        {
            return;
        }
        if(currentTime >= maxTime)
        {
            Reset();
        }
        else
        {
            AddTime(time);
        }
    }
    /// <summary>
    /// 激活计时器
    /// </summary>
    public void AwakeTimer()
    {
        isAwake = true;
    }
    public float TimePercent()
    {
        return currentTime / maxTime;
    }
}
