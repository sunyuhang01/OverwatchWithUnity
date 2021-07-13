using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 麦克雷技能
/// 1技能：翻滚
/// 2技能：闪光弹
/// 大招：午时已到
/// </summary>
public class MccreeSkill : BaseSkill
{
    private Transform flashPoint;
    private float rollingDistance = 5;
    private float rollingTime = 0.3f;
    private float currentTime;
    /// <summary>
    /// 获取水平方向
    /// </summary>
    private float hor;
    /// <summary>
    /// 获取垂直方向
    /// </summary>
    private float ver;
    private Vector3 targetPos;
    public Vector3 direction;
    protected override void Start()
    {
        base.Start();
        flashPoint = mainCamera.transform.Find("FlashPoint");
        Skill1Init();
        
    }
    /// <summary>
    /// 1技能：翻滚
    /// 向移动方向翻滚指定距离并且装填子弹
    /// </summary>
    /// <returns></returns>
    protected override bool UseSkill1()
    {
        if(hor == -2 && ver == -2)
        {
            Skill1Init();
            hor = Input.GetAxis("Horizontal");
            ver = Input.GetAxis("Vertical");
            print(hor.ToString() + " " + ver.ToString());
            direction = this.transform.forward;
            if(ver < 0)
            {
                direction = -direction;
            }
            if(hor != 0)
            {
                direction *= ver == 0 ? 0 : 1;
            }
            if(hor > 0)
            {
                direction = (direction + this.transform.right).normalized;
            }
            else if(hor < 0)
            {
                direction = (direction - this.transform.right).normalized;
            }
            //direction.y -= fpsInfo.gravity * Time.deltaTime;
            
            //targetPos = this.transform.position + new Vector3(-hor.Value, 0, -ver.Value).normalized * rollingDistance;
            return true;
        }
        else
        {
            if(Time.time >= currentTime + rollingTime)
            {
                gun.currentAmmoBullets = gun.ammoCapacity;
                print("使用完毕");
                hor = -2;
                ver = -2;
                return false;
            }
            else
            {
                
                controller.Move(direction * Time.deltaTime * rollingDistance / rollingTime);
                //this.transform.Translate(direction * Time.deltaTime, Space.World);
                //this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, rollingDistance / rollingTime * Time.fixedDeltaTime);
                return true;
            }
            
        }


    }
    private void Skill1Init()
    {
        currentTime = Time.time;
        hor = -2;
        ver = -2;
    }
    /// <summary>
    /// 2技能：闪光弹
    /// </summary>
    /// <returns>正在使用返回true，使用完毕返回false</returns>
    protected override bool UseSkill2()
    {
        GameObject bullet = Instantiate(ResourceManager.GetBulletPrefab("FlashBomb"), flashPoint.position, flashPoint.rotation);
        return false;
    }
    /// <summary>
    /// 大招：午时已到
    /// </summary>
    /// <returns></returns>
    protected override bool UseUltimateSkill()
    {
        gun.currentAmmoBullets = gun.ammoCapacity;
        ultimateCharge.Reset();
        return false;
    }
}
