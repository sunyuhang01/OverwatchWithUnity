using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 闪光弹，对打中的敌人有晕眩效果
/// </summary>
public class FlashBomb : Bullet
{
    /// <summary>
    /// 晕眩时间
    /// </summary>
    public float dizzyTime = 3;
    protected override void Awake()
    {
        base.Awake();
        hasSpecialEffect = true;
    }
    protected override void SpecialEffects()
    {
        print("晕眩效果");
        targetCollider.GetComponentInParent<HeroInfo>().Dizzy(dizzyTime);
    }
}
