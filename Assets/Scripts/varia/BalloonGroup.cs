using System.Collections.Generic;
using UnityEngine;

public class BalloonGroup : MonoBehaviour
{
    public readonly List<Balloon> balloons = new List<Balloon>(); //存放气球的列表

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            balloons.Add(transform.GetChild(i).GetComponent<Balloon>()); //添加气球
        }
    }
    
    /// <summary>
    /// 失去平衡
    /// </summary>
    public void LoseBalance()
    {
        foreach (Balloon balloon in balloons)
        {
            balloon.GroupMemberDestroy();
        }
    }
    
}