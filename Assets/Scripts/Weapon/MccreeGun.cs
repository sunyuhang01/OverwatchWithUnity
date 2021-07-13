using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 麦克雷的枪
/// 左键单点，右键六连
/// </summary>
public class MccreeGun : Gun
{
    /// <summary>
    /// 主要攻击间隔
    /// </summary>
    
    private Camera myCamera;
    public float subAttackTime = 0.1f;

    protected override void Start()
    {
        base.Start();
        myCamera = GetComponentInParent<Camera>();
        print("Mccree--Start");
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (heroInfo.state == HeroState.Dizzy)
        {
            shootMode = ShootMode.NotShoot;
            return;
        }
        if(shootMode == ShootMode.NotShoot)
        {
            //不在射击时
            if(currentAmmoBullets == 0)
            {
                UpdateAmmo();
            }
            //左键射击
            if (Input.GetMouseButton(0))
            {
                shootMode = ShootMode.MainShoot;
            }
            //右键射击
            if (Input.GetMouseButton(1))
            {
                shootMode = ShootMode.SubShoot;
            }
            if(Input.GetAxis("Reload") > 0)
            {
                shootMode = ShootMode.Reloading;
            }
        }
        else if(shootMode == ShootMode.MainShoot)
        {
            //左键射击时
            MainShoot();
        }
        else if(shootMode == ShootMode.SubShoot)
        {
            //右键射击时
            SubShoot();
        }
        else if(shootMode == ShootMode.Reloading)
        {
            //装子弹时
            UpdateAmmo();
        }
    }

   
    private void SubShoot()
    {
        if (maxAttackTime == 0)
        {
            maxAttackTime = subAttackTime;
            curAttackTime = 0;
        }
        if (currentAmmoBullets > 0)
        {
            //有子弹时把子弹打完
            if (curAttackTime == 0)
            {
                base.Firing();
                print("right shoot--" + currentAmmoBullets);
                curAttackTime += Time.deltaTime;
            }
            else if (curAttackTime >= maxAttackTime)
            {
                curAttackTime = 0;
            }
            else
            {
                curAttackTime += Time.deltaTime;
            }
        }
        else
        {
            //射击完成
            maxAttackTime = 0;
            shootMode = ShootMode.NotShoot;
        }
    }
}
