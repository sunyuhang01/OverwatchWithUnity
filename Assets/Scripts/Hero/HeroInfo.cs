using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 英雄信息类,包含英雄基本信息和状态改变（受击、加血、晕眩等）
/// </summary>
public class HeroInfo : MonoBehaviour
{
    public string heroName;
    public float maxHp;
    public float reviveTime = 3f;
    public HeroState state;
    public int SkillNum = 2;

    /// <summary>
    /// 出生点
    /// </summary>
    public Transform homeTF;

    public float currentHp;
    public Timer reviveTimer;

    private float dizzyTime = 0;
    public Timer dizzyTimer;
    private void Start()
    {
        currentHp = maxHp;
        state = HeroState.Normal;

        reviveTimer = new Timer(reviveTime);
        dizzyTimer = new Timer(dizzyTime);
    }
    private void FixedUpdate()
    {
        if(state != HeroState.Death && currentHp == 0)
        {
            Death();
            state = HeroState.Death;
            
        }
        if (state == HeroState.Death)
        {
            Revive();
        }
        if(state == HeroState.Dizzy)
        {
            Dizzy(null);
        }
    }
    /// <summary>
    /// 改变英雄血量
    /// </summary>
    /// <param name="value">受到伤害或治疗量。如果大于零则受到伤害，小于零则受到治疗</param>
    public void ChangeHp(float value)
    {
        print(this.name + "改变" + value.ToString() + "HP");
        if(value >= 0)
        {
            currentHp = value > currentHp ? 0 : currentHp - value;
        }
        else
        {
            currentHp = (currentHp - value) > maxHp ? maxHp : (currentHp - value);
        }
    }
    /// <summary>
    /// 英雄死亡
    /// </summary>
    public void Death()
    {
        //播放死亡动画
        this.transform.position = new Vector3(0, -1000, 0);

        reviveTimer.AwakeTimer();
    }
    /// <summary>
    /// 英雄复活
    /// </summary>
    public void Revive()
    {
        if(!reviveTimer.IsMax())
        {
            reviveTimer.UpdateTimer(Time.fixedDeltaTime);
        }
        else
        {
            currentHp = maxHp;
            reviveTimer.Reset();
            state = HeroState.Normal;
            this.transform.position = homeTF.position;
        }
    }
    /// <summary>
    /// 晕眩状态
    /// </summary>
    /// <param name="time"></param>
    public void Dizzy(float? time)
    {
        if(state != HeroState.Death && time != null)
        {
            state = HeroState.Dizzy;
            dizzyTimer.SetMaxTime(time.Value);
            dizzyTimer.AwakeTimer();
            print(this.name + "被晕了");

        }
        //自己调用持续晕眩状态
        if(time == null)
        {
            if(!dizzyTimer.IsMax())
            {
                dizzyTimer.UpdateTimer(Time.fixedDeltaTime);
            }
            else
            {
                state = HeroState.Normal;
                dizzyTimer.Reset();
                print(this.name + "晕眩结束");
            }
        }
    }
}
