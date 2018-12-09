using System.Collections.Generic;
using UnityEngine;

public class AllBird : MonoBehaviour
{
    public List<GameObject> birds = new List<GameObject>(); //一个集合
    private float nowTime;

    private void Awake()
    {
        BirdFly();
    }

    private void Update()
    {
        nowTime += Time.deltaTime;

        if (nowTime > 3f)
        {
            nowTime = 0;
            BirdFly(); //生成一次小鸟 
        }
    }

    /// <summary>
    /// 小鸟飞的方法
    /// </summary>
    private void BirdFly()
    {
        if (transform.childCount > 20) //避免生成过多小鸟
            return;

        for (int i = 0; i < Random.Range(6, 12); i++)
        {
            GameObject bird = Instantiate(birds[Random.Range(0, birds.Count)]); //初始化一个小鸟
            bird.transform.parent = transform;
        }
    }
}