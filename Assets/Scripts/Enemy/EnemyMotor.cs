using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人马达，提供移动、旋转、寻路功能
/// </summary>
public class EnemyMotor : MonoBehaviour
{
    public Transform linesTF;
    private WayLine line;
    private int nextIndex;
    public float moveSpeed = 2;
    private Timer stayTimer; //停留计时器
    private float stayTime = 5f;
    private HeroInfo heroInfo;
    private void Start()
    {
        stayTimer = new Timer(stayTime);
        heroInfo = GetComponent<HeroInfo>();
        InitLine();
    }
    private void Update()
    {
        if(heroInfo.state == HeroState.Normal)
        {
            if (!PathFinding())
            {
                print("寻路异常");
            }
        }
    }
    private void InitLine()
    {
        line = new WayLine(linesTF.childCount);
        for(int i = 0; i < linesTF.childCount; i++)
        {
            line.wayPoints[i] = linesTF.GetChild(i).position;
        }
    }
    /// <summary>
    /// 向前移动
    /// </summary>
    public void MovementForward()
    {
        this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
    }
    /// <summary>
    /// 注视旋转
    /// </summary>
    /// <param name="targetPoint">需要注视的目标点</param>
    public void LookRotation(Vector3 targetPoint)
    {
        //当前物体注视目标点旋转
        this.transform.LookAt(targetPoint);
    }
    /// <summary>
    /// 寻路，沿路线移动
    /// </summary>
    /// <returns>只要line不为空就一直寻路</returns>
    public bool PathFinding()
    {
        if (line == null)
        {
            return false;
        }
        Vector3 nextPoint = line.wayPoints[nextIndex];
        if (Vector3.Distance(this.transform.position, nextPoint) >= 0.5f)
        {
            //转向目标点方向
            LookRotation(nextPoint);
            //移动
            MovementForward();
        }
        //到达目标点
        else
        {
            if (!stayTimer.isAwake)
            {
                stayTimer.AwakeTimer();
            }
            else
            {
                //计时结束
                if (stayTimer.IsMax())
                {
                    nextIndex = (nextIndex + 1) % line.wayPoints.Length;
                    stayTimer.Reset();
                }
                //计时
                else
                {
                    stayTimer.UpdateTimer(Time.deltaTime);
                }
            }
            
        }
        return true;
    }
}

