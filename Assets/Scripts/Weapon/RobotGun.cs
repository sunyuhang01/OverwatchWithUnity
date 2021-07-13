using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 机器人的枪
/// </summary>
public class RobotGun : Gun
{
    protected override void Start()
    {
        base.Start();
        print("Robot--Start");
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        AutoShoot();
    }
    /// <summary>
    /// 自动射击
    /// </summary>
    private void AutoShoot()
    {
        if(shootMode == ShootMode.MainShoot || shootMode == ShootMode.NotShoot)
        {
            MainShoot();
        }
        else if(shootMode == ShootMode.Reloading)
        {
            UpdateAmmo();
        }
    }
}
