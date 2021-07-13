using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家信息类，暂时包含出生点位置
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    /// <summary>
    /// 出生点
    /// </summary>
    public Transform homeTF;
    private void Start()
    {
        homeTF = this.transform;
    }
}
