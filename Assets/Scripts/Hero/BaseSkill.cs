using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能类的基类
/// </summary>
public class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// 技能1的冷却时间
    /// </summary>
    public float skill1Cooldown;
    public Timer skill1Timer;
    /// <summary>
    /// 技能2的冷却时间
    /// </summary>
    public float skill2Cooldown;
    public Timer skill2Timer;
    /// <summary>
    /// 大招充能点数
    /// </summary>
    public float maxUltimateCharge = 100;
    public Timer ultimateCharge;
    /// <summary>
    /// 大招自动充能一点所需时间
    /// </summary>
    private float onePointTimeSpace = 0.1f;
    private Timer onePointTimer;
    /// <summary>
    /// 大招持续时间
    /// </summary>
    public float maxUltimateTime = 5f;
    public Timer ultimateTime;
    protected HeroInfo heroInfo;
    protected Camera mainCamera;
    protected CharacterController controller;
    protected FPSController fpsInfo;
    protected Gun gun;
    protected virtual void Start()
    {
        print("Base Skill Start");
        heroInfo = GetComponent<HeroInfo>();
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        fpsInfo = GetComponentInParent<FPSController>();
        gun = GetComponentInChildren<Gun>();
        skill1Timer = new Timer(skill1Cooldown);
        skill2Timer = new Timer(skill2Cooldown);
        ultimateCharge = new Timer(maxUltimateCharge);
        onePointTimer = new Timer(onePointTimeSpace);
        ultimateTime = new Timer(maxUltimateTime);
    }
    protected virtual void Update()
    {
        //随时间自动更新的信息
        AutoInfoUpdate();
        //技能操作
        //处于正常状态下才可以使用小技能
        if (heroInfo.state == HeroState.Normal)
        {
            //1技能
            if (!skill1Timer.isAwake && Input.GetButtonDown("Skill1"))
            {
                print("使用1技能");
                heroInfo.state = HeroState.Skill1;

            }
            //2技能
            else if (!skill2Timer.isAwake && Input.GetButtonDown("Skill2"))
            {
                print("使用2技能");
                heroInfo.state = HeroState.Skill2;
            }
        }
        //大招可以在非异常状态下使用
        if (heroInfo.state == HeroState.Normal || heroInfo.state == HeroState.Skill1 || heroInfo.state == HeroState.Skill2)
        {
            if (ultimateCharge.IsMax() && Input.GetButtonDown("Ultimate Skill"))
            {
                print("使用大招");
                heroInfo.state = HeroState.UltimateSkill;
            }

        }
        //使用技能
        if (heroInfo.state == HeroState.Skill1 || heroInfo.state == HeroState.Skill2 || heroInfo.state == HeroState.UltimateSkill)
        {
            UseSkill();
        }

    }

    private void AutoInfoUpdate()
    {
        //大招充能
        if (!ultimateCharge.IsMax())
        {
            if (!onePointTimer.IsMax())
            {
                onePointTimer.AddTime(Time.deltaTime);
            }
            else
            {
                ChargeUltimate(1);
                onePointTimer.Reset();
            }
        }
        else if (ultimateCharge.currentTime > ultimateCharge.maxTime)
        {
            ultimateCharge.currentTime = ultimateCharge.maxTime;
        }
        skill1Timer.UpdateTimer(Time.deltaTime);
        skill2Timer.UpdateTimer(Time.deltaTime);
    }

    public void ChargeUltimate(float value)
    {
        ultimateCharge.AddTime(value);
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    protected void UseSkill()
    {
        if(heroInfo.state == HeroState.Skill1)
        {
            if (!UseSkill1())
            {
                skill1Timer.AwakeTimer();
                heroInfo.state = HeroState.Normal;
            }
        }
        if (heroInfo.state == HeroState.Skill2)
        {
            if (!UseSkill2())
            {
                skill2Timer.AwakeTimer();
                heroInfo.state = HeroState.Normal;
            }
        }
        if(heroInfo.state == HeroState.UltimateSkill)
        {
            if (!UseUltimateSkill())
            {
                heroInfo.state = HeroState.Normal;
            }
        }
    }
    /// <summary>
    /// 使用1技能
    /// </summary>
    /// <returns>正在使用返回true，使用完毕返回false</returns>
    protected virtual bool UseSkill1()
    {
        print("正在使用1技能");
        return false;

    }
    protected virtual bool UseSkill2()
    {
        print("正在使用2技能");
        return false;
    }
    protected virtual bool UseUltimateSkill()
    {
        print("正在使用大招");
        return false;
    }
}
