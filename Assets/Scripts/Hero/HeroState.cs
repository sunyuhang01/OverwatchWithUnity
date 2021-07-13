using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 定义英雄当前状态
/// </summary>
public enum HeroState
{
    Normal,  //正常状态
    Dizzy,   //晕眩状态
    Sleep,   //睡眠状态
    Death,   //死亡状态
    Skill1,  //使用技能1
    Skill2,  //使用技能2
    UltimateSkill, //使用大招 

}
