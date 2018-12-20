using birds;
using UnityEngine;

public class GreenBird : Bird
{
    public AudioClip circleRoundAudio; //回旋的声音

    /// <summary>
    /// 重写虚方法
    /// </summary>
    protected override void ShowSkill()
    {
        base.ShowSkill();
        Vector2 speed = rigidBody2D.velocity;
        speed.x *= -1;
        rigidBody2D.velocity = speed; //设置速度
        transform.localScale = new Vector3(-1, 1, 1); //反转180度

        Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效
        path.AddSkillPath(transform.position); //添加一个特效
        birdAnim.ShowSkill(); //改为展示技能的样式
        AudioUtil.sInstance.AudioPlay(circleRoundAudio); //播放回旋声音
    }
}