using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 枪
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    
    /// <summary>
    /// 发射子弹时的音频片段
    /// </summary>
    public AudioClip clip;
    /// <summary>
    /// 弹匣容量
    /// </summary>
    public int ammoCapacity = 10;
    /// <summary>
    /// 当前弹匣内子弹数
    /// </summary>
    public int currentAmmoBullets;
    /// <summary>
    /// 重装子弹时间
    /// </summary>
    public float reloadTime = 3f;
    public float currentReloadTime;
    /// <summary>
    /// 开火点位置
    /// </summary>
    public Transform firePoint;
    /// <summary>
    /// 子弹预制件的名字
    /// </summary>
    public string bulletName;
    public HeroInfo heroInfo;
    protected ShootMode shootMode;
    private AudioSource audioSource;

    public float mainAttackInterval = 0.45f;
    protected float curAttackTime = 0;
    protected float maxAttackTime = 0; //用于实现武器攻击间隔

    protected virtual void Start()
    {
        print("Gun--Start");
        firePoint = transform.Find("FirePoint");
        currentAmmoBullets = ammoCapacity;
        currentReloadTime = 0;
        shootMode = ShootMode.NotShoot;
        heroInfo = GetComponentInParent<HeroInfo>();
    }
    protected virtual void FixedUpdate()
    {
        if(heroInfo.state == HeroState.Normal && currentAmmoBullets == 0)
        {
            shootMode = ShootMode.Reloading;
        }
    }
    /// <summary>
    /// 开火
    /// </summary>
    public void Firing()
    {
        //准备子弹
        //判断弹匣内是否包含子弹
        if(currentAmmoBullets == 0)
        {
            UpdateAmmo();
        }
        else
        {
            //发射子弹
            //创建子弹、播放音频、播放动画
            //播放声音
            currentAmmoBullets--;
            GameObject bullet = GameObject.Instantiate(ResourceManager.GetBulletPrefab(bulletName), firePoint.position, firePoint.rotation);
            //audioSource.PlayOneShot(clip);
        }
        
    }
    /// <summary>
    /// 装填子弹
    /// </summary>
    public void UpdateAmmo()
    {
        if(currentReloadTime == 0 && (currentAmmoBullets < ammoCapacity))
        {
            print("播放装子弹动画");
            shootMode = ShootMode.Reloading;
            currentReloadTime += Time.deltaTime;
        }
        else if(currentReloadTime >= reloadTime)
        {
            print("装完子弹");
            currentAmmoBullets = ammoCapacity;
            currentReloadTime = 0;
            shootMode = ShootMode.NotShoot;
        }
        else
        {
            currentReloadTime += Time.deltaTime;
        }
        
    }
    protected virtual void MainShoot()
    {
        if (maxAttackTime == 0)
        {
            maxAttackTime = mainAttackInterval;
            curAttackTime = 0;
        }
        if (curAttackTime == 0)
        {
            Firing();
            print("main shoot");
            curAttackTime += Time.deltaTime;
        }
        else if (curAttackTime >= maxAttackTime)
        {
            //射击完成
            maxAttackTime = 0;
            shootMode = ShootMode.NotShoot;
        }
        else
        {
            curAttackTime += Time.deltaTime;
        }
    }
}
