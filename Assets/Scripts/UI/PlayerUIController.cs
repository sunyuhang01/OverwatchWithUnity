using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用于修改Player界面的UI
/// </summary>
public class PlayerUIController : MonoBehaviour
{
    public Gun gun;
    public Text maxAmmoUI, ammoUI, ultPercentUI, hpUI, maxHpUI, reviveUI;
    public Text[] skillsUI;
    public Image ultChargeImageUI, reviveImageUI, aimPointImageUI, shootImageUI, headShootImageUI;
    private Image currentShootImageUI;
    public Image[] skillsImageUI;
    public Slider hpSlider;
    public BaseSkill heroSkill;
    public HeroInfo heroInfo;
    public Timer[] skillsTimer;
    public Timer reviveTimer;
    public Timer shootUITimer;
    private float shootUIMaxTime = 0.5f;
    public float p, a;
    private void Start()
    {
        //获取界面上的text
        //子弹
        maxAmmoUI = this.transform.Find("PlayerMaxAmmo").GetComponent<Text>();
        ammoUI = this.transform.Find("PlayerAmmo").GetComponent<Text>();
        //大招
        ultPercentUI = this.transform.Find("UltPercent").GetComponent<Text>();
        ultChargeImageUI = this.transform.Find("UltEnergy").GetComponent<Image>();
        ultChargeImageUI.fillAmount = 0;
        //血量
        hpUI = this.transform.Find("PlayerHP").GetComponent<Text>();
        maxHpUI = this.transform.Find("PlayerMaxHP").GetComponent<Text>();
        hpSlider = this.transform.Find("PlayerHPBar").GetComponent<Slider>();
        //复活倒计时
        reviveUI = this.transform.Find("ReviveText").GetComponent<Text>();
        reviveImageUI = this.transform.Find("ReviveImg").GetComponent<Image>();
        //获取准心
        aimPointImageUI = this.transform.Find("AimPoint").GetComponent<Image>();
        //获取击中反馈
        shootImageUI = this.transform.Find("Shoot").GetComponent<Image>();
        headShootImageUI = this.transform.Find("HeadShoot").GetComponent<Image>();
        shootUITimer = new Timer(shootUIMaxTime);
        //获取技能
        skillsUI = new Text[2];
        skillsImageUI = new Image[2];
        for(int i = 1; i <= 2; i++)
        {
            skillsUI[i - 1] = this.transform.Find("Skill" + i.ToString() + "Text").GetComponent<Text>();
            skillsImageUI[i - 1] = this.transform.Find("Skill" + i.ToString() + "_2").GetComponent<Image>();
            skillsImageUI[i - 1].fillAmount = 0;
        }
        //获取信息源
        gun = Camera.main.GetComponentInChildren<Gun>();
        heroSkill = Camera.main.GetComponentInParent<BaseSkill>();
        heroInfo = Camera.main.GetComponentInParent<HeroInfo>();
        Init();
    }
    private void FixedUpdate()
    {
        UpdateAmmo();
        UpdateHp();
        UpdateUltimateSkill();
        UpdateSkills();
        UpdateRevive();
        UpdateShoot(null);
    }
    private void Init()
    {
        //获取弹匣容量
        maxAmmoUI.text = "/" + gun.ammoCapacity.ToString();
        UpdateAmmo();
        //获取英雄生命值
        float maxHp = heroInfo.maxHp;
        maxHpUI.text = "/" + maxHp.ToString();
        hpSlider.maxValue = maxHp;
        //获取技能状态
        skillsTimer = new Timer[2]; 
        skillsTimer[0] = heroSkill.skill1Timer;
        skillsTimer[1] = heroSkill.skill2Timer;
        //获取复活状态
        reviveTimer = heroInfo.reviveTimer;
        //隐藏复活倒计时
        reviveUI.text = "";
        reviveImageUI.fillAmount = 0;
        //击中反馈UI重置
        ShootImagesInit();

    }
    private void ShootImagesInit()
    {
        shootImageUI.color = new Color(shootImageUI.color.r, shootImageUI.color.g, shootImageUI.color.b, 0);
        headShootImageUI.color = new Color(headShootImageUI.color.r, headShootImageUI.color.g, headShootImageUI.color.b, 0);
    }
    /// <summary>
    /// 更新当前弹量
    /// </summary>
    private void UpdateAmmo()
    {
        ammoUI.text = gun.currentAmmoBullets.ToString();
    }
    /// <summary>
    /// 更新大招百分比和图片
    /// </summary>
    private void UpdateUltimateSkill()
    {
        float percent = heroSkill.ultimateCharge.currentTime / heroSkill.ultimateCharge.maxTime;
        ultPercentUI.text = Mathf.Floor(percent * 100).ToString();
        ultChargeImageUI.fillAmount = percent;
    }
    /// <summary>
    /// 更新HP值和血条
    /// </summary>
    private void UpdateHp()
    {
        float currentHp = heroInfo.currentHp;
        hpUI.text = currentHp.ToString();
        hpSlider.value = currentHp;
    }
    /// <summary>
    /// 更新小技能冷却和图片
    /// </summary>
    private void UpdateSkills()
    {
        for(int i = 0; i < 2; i++)
        {
            if (skillsTimer[i].isAwake)
            {
                //倒计时
                float percent = (skillsTimer[i].maxTime - skillsTimer[i].currentTime) / skillsTimer[i].maxTime;
                skillsUI[i].text = UIHelper.BiggerThenZero(Mathf.Floor(skillsTimer[i].maxTime - skillsTimer[i].currentTime)).ToString();
                skillsImageUI[i].fillAmount = percent;
            }
            else
            {
                skillsUI[i].text = "";
            }
        }
    }
    /// <summary>
    /// 更新复活倒计时标志
    /// </summary>
    private void UpdateRevive()
    {
        if(heroInfo.state == HeroState.Death)
        {
            aimPointImageUI.gameObject.SetActive(false);
            float percent = (reviveTimer.maxTime - reviveTimer.currentTime) / reviveTimer.maxTime;
            reviveUI.text = UIHelper.BiggerThenZero(Mathf.Floor(reviveTimer.maxTime - reviveTimer.currentTime)).ToString();
            reviveImageUI.fillAmount = percent;
        }
        else
        {
            reviveUI.text = "";
            if (!aimPointImageUI.gameObject.activeSelf)
            {
                aimPointImageUI.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateShoot(bool? isHead)
    {
        if(isHead != null)
        {
            //重置图标
            ShootImagesInit();
            shootUITimer.AwakeTimer();
            if (isHead.Value)
            {
                //爆头
                currentShootImageUI = headShootImageUI;
            }
            else
            {
                //不爆头
                currentShootImageUI = shootImageUI;
            }
        }
        else
        {
            if (shootUITimer.isAwake)
            {
                shootUITimer.UpdateTimer(Time.fixedDeltaTime);
                float percent = p = 1 - shootUITimer.TimePercent();
                currentShootImageUI.color = new Color(currentShootImageUI.color.r,
                                                      currentShootImageUI.color.g,
                                                      currentShootImageUI.color.b,
                                                      (percent > 0 ? percent : 0));
                a = shootImageUI.color.a;

                if (shootUITimer.IsMax())
                {
                    print("重置");
                    shootUITimer.Reset();
                    ShootImagesInit();
                }
            }
        }
    }
}
