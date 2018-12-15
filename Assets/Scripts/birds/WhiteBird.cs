using birds;
using UnityEngine;

public class WhiteBird : Bird
{
    public ExplosionEgg explosionEgg; //爆炸的蛋
    public AudioClip eggClip; //下蛋的声音

    protected override void ShowSkill()
    {
        base.ShowSkill();
        path.AddSkillPath(transform.position); //添加一个特效
        AudioUtil.sInstance.AudioPlay(eggClip, transform.position); //下蛋声音

        Instantiate(explosionEgg, transform.position, Quaternion.identity); //生成一个蛋
        birdAnim.ShowSkill(); //改为展示技能的样式
    }
}