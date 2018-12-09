using UnityEngine;

public class YellowBird : Bird
{
    public AudioClip skillAudio; //技能的声音

    protected override void ShowSkill()
    {
        base.ShowSkill();
        path.AddSkillPath(transform.position); //添加一个特效
        AudioUtil.sInstance.AudioPlay(skillAudio, transform.position); //播放一个技巧声音
        Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效
        rigidBody2D.velocity *= 2; //速度增大
        birdAnim.ShowSkill(); //改为展示技能的样式
    }
    
}