using System;
using birds;
using UnityEngine;

public class BlueBird : Bird
{
    public CloneBlueBird cloneBlueBirdPrefab; //克隆生成的小鸟

    protected override void ShowSkill()
    {
        base.ShowSkill();
        path.AddSkillPath(transform.position); //添加一个特效
        Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效

        CloneBlueBird cloneBird1 = Instantiate(cloneBlueBirdPrefab, transform.position, Quaternion.identity); //生成一个克隆小鸟
        cloneBird1.path = path; //将路径对象赋过来

        print("小鸟速度" + rigidBody2D.velocity);


        float x = rigidBody2D.velocity.x; //x轴速度
        float y = rigidBody2D.velocity.y; //y轴速度

        float x1 = (float) (x * Math.Cos(Math.PI / 12) - y * Math.Sin(Math.PI / 12));
        float y1 = (float) (x * Math.Sin(Math.PI / 12) + y * Math.Cos(Math.PI / 12));

        float x2 = (float) (x * Math.Cos(Math.PI / 12) + y * Math.Sin(Math.PI / 12));
        float y2 = (float) (-x * Math.Sin(Math.PI / 12) + y * Math.Cos(Math.PI / 12));

        cloneBird1.GetComponent<Rigidbody2D>().velocity = new Vector2(x1, y1); //设置一下速度

        print("克隆小鸟1的速度 " + cloneBird1.GetComponent<Rigidbody2D>().velocity);

        CloneBlueBird cloneBird2 = Instantiate(cloneBlueBirdPrefab, transform.position, Quaternion.identity); //生成一个克隆小鸟
        cloneBird2.path = path; //将路径对象赋过来

        cloneBird2.GetComponent<Rigidbody2D>().velocity = new Vector2(x2, y2); //设置一下速度
        print("克隆小鸟2的速度 " + cloneBird2.GetComponent<Rigidbody2D>().velocity);
    }
}