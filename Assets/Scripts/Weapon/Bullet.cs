using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹类。计算攻击的目标点，执行移动，创建接触特效
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 即时命中的layermask,检测除自身和子弹外所有collide
    /// </summary>
    public LayerMask layerMaskFast;
    /// <summary>
    /// 延迟弹道的layermask, 只检测环境collide
    /// </summary>
    public LayerMask layerMaskSlow;
    /// <summary>
    /// 子弹攻击力
    /// </summary>
    public float atk = 5f;
    /// <summary>
    /// 子弹速度，0即为即时命中
    /// </summary>
    public float speed = 0;
    /// <summary>
    /// 最长飞行距离
    /// </summary>
    public float flyingDistance = 100;
    /// <summary>
    /// 射线检测子弹击中的目标
    /// </summary>
    public string bodyName = "Col_Body";
    public string headName = "Col_Head";
    public bool isHeroBullet;
    private RaycastHit hit;
    private bool isHit;
    protected Collider targetCollider;
    /// <summary>
    /// 用于计算子弹飞行的目标点
    /// 英雄使用自身的摄像机
    /// 机器人使用FirePoint;
    /// </summary>
    protected Transform moveTF;
    public Vector3 pos1, pos2;
    public Vector3 targetPos;
    /// <summary>
    /// 该子弹是否有特殊效果
    /// </summary>
    protected bool hasSpecialEffect;
    protected PlayerUIController playerUI;
    protected virtual void Awake()
    {
        if(speed == 0)
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
        moveTF = this.transform;
        if (isHeroBullet)
        {
            moveTF = Camera.main.transform;
        }
        isHit = false;
        CalculateTargetPoint();
        hasSpecialEffect = false;
        playerUI = FindObjectOfType<Canvas>().GetComponentInChildren<PlayerUIController>();
    }
    
    protected virtual void Update()
    {
        Debug.DrawLine(this.transform.position, targetPos);
        Movement();
        if(speed == 0 || isHit || (transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            ContactTarget();
        }
    }
    

    /// <summary>
    /// 计算目标点
    /// </summary>
    private void CalculateTargetPoint()
    {
        //即时命中
        if(speed == 0)
        {
            if(Physics.Raycast(moveTF.position, moveTF.forward, out hit, flyingDistance, layerMaskFast))
            {
                //检测到物体
                Debug.Log("Did Hit");
                Debug.Log(hit.collider.name);
                targetPos = hit.point;
                targetCollider = hit.collider;
                isHit = true;
            }
            else
            {
                targetPos = transform.position + transform.forward * flyingDistance;
                isHit = false;
            }
        }
        //延迟弹道
        else
        {
            if (Physics.Raycast(moveTF.position, moveTF.forward, out hit, flyingDistance, layerMaskSlow))
            {
                Debug.Log(hit.collider.name);
                targetPos = hit.point;
                isHit = false;
            }
            else
            {
                targetPos = moveTF.position + moveTF.forward * flyingDistance;
                isHit = false;
            }
        }
        
    }
    /// <summary>
    /// 移动至目标点
    /// </summary>
    private void Movement()
    {
        BulletEffects();
        if(speed == 0)
        {
            transform.Translate(targetPos - transform.position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        }
        
    }
    /// <summary>
    /// 接触目标点
    /// </summary>
    private void ContactTarget()
    {
        print("接触目标点");
        //生成接触特效
        GenerateContactEffect();
        if (isHit)
        {
            //造成伤害
            AttackHit(targetCollider);
            //特殊效果
            if (hasSpecialEffect)
            {
                SpecialEffects();
            }
        }
        //销毁子弹
        GameObject.Destroy(this.gameObject);
    }
    /// <summary>
    /// 创建接触特效
    /// </summary>
    private void GenerateContactEffect()
    {
        //通过代码读取资源
        print("生成击中特效");

    }

    //弹道子弹碰撞效果
    private void OnTriggerEnter(Collider other)
    {
        targetCollider = other;
        isHit = true;
    }
    /// <summary>
    /// 击中碰撞体
    /// </summary>
    /// <param name="collider"></param>
    private void AttackHit(Collider collider)
    {
        //生成击中效果
        if (collider.name == bodyName)
        {
            print("Attack Body");
            if (isHeroBullet)
            {
                playerUI.UpdateShoot(false);
            }
            
            collider.GetComponentInParent<HeroInfo>().ChangeHp(atk);
        }
        else if (collider.name == headName)
        {
            print("Attack Head");
            if (isHeroBullet)
            {
                playerUI.UpdateShoot(true);
            }
            
            collider.GetComponentInParent<HeroInfo>().ChangeHp(atk * 2);
        }
    }
    /// <summary>
    /// 特殊效果
    /// </summary>
    protected virtual void SpecialEffects()
    {
        print("无特殊效果");
    }
    protected virtual void BulletEffects()
    {
        //print("子弹特效");
    }
}
