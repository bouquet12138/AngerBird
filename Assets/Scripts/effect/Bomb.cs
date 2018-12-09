using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸弹对象
/// </summary>
public class Bomb : MonoBehaviour
{
    //public bool boom; //爆炸
    public float targetRadius = 4f; //目标半径
    public float totalTime = 0.5f; //爆炸需要的时间
    private CircleCollider2D collider; //碰撞体
    private float nowTime; //当前运行时间


    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>(); //得到碰撞体
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        collider.radius = Math.Min(targetRadius / totalTime * nowTime, targetRadius);

        if (nowTime >= totalTime)
        {
            Destroy(gameObject); //销毁自己
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*if (!boom)
            return;*/


        Vector2 relativePosition = other.transform.position - transform.position; //相对位置
        print("相对位置 " + relativePosition);

        int direX = relativePosition.x > 0 ? 1 : -1;
        int direY = relativePosition.y > 0 ? 1 : -1;


        print("速度 X " + 5 / relativePosition.x);
        print("速度 y " + 5 / relativePosition.y);
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            float velocityX;
            if (relativePosition.x == 0)
                velocityX = 15;
            else
                velocityX = Math.Abs(10 / relativePosition.x);

            float velocityY;
            if (relativePosition.y == 0)
                velocityY = 15;
            else
                velocityY = Math.Abs(10 / relativePosition.y);

            other.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Clamp(velocityX, 10, 30) * direX,
                    Mathf.Clamp(velocityY, 10, 30) * direY); //设置下速度
        }
    }
}