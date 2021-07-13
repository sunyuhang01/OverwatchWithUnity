using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 路线类，包含路点
/// </summary>
public class WayLine
{
    public Vector3[] wayPoints { get; set; }
    public WayLine(int wayPointCount)
    {
        wayPoints = new Vector3[wayPointCount];
    }
}
