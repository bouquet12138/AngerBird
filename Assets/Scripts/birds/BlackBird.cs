using System.Collections.Generic;
using birds;
using effect;
using UnityEngine;

public class BlackBird : Bird
{
    private readonly List<Pig> pigList = new List<Pig>(); //初始化一下集合
    private readonly List<Block> blockList = new List<Block>(); //木块的集合

    public GameObject blackBoom; //黑色小鸟爆炸的特效
    public GameObject littleBlackBoom; //黑色小鸟爆炸的特效
    public AudioClip explosionClip; //爆炸的声音
    private bool isBoom; //是否爆炸

    public Bomb bomb;


    /// <summary>
    /// 重写虚方法
    /// </summary>
    protected override void ShowSkill()
    {
        base.ShowSkill();
        CameraVary.sInstance.BirdUse = false; //小鸟不使用了
        isBoom = true; //爆炸了
        path.AddSkillPath(transform.position); //添加一个特效
        AudioUtil.sInstance.AudioPlay(explosionClip, transform.position); //播放爆炸音效
        Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效
        Instantiate(blackBoom, transform.position, Quaternion.identity); //生成一个碰撞特效
        BoomPig();
        OnClear();
        bomb.TargetRadius = 3; //目标尺寸
        bomb.Power = 8; //炸弹力量
        bomb.MaxSpeed = 30; //最大速度
        Instantiate(bomb, transform.position, Quaternion.identity); //生成一个爆炸效果
    }


    /// <summary>
    /// 该清除的清除
    /// </summary>
    private void OnClear()
    {
        rigidBody2D.velocity = Vector2.zero; //速度归零
        Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
        CurrentState = BIRD_COLLIDER; //当前状态改为 小鸟已碰撞
        Collider2D[] collider2Ds = GetComponents<Collider2D>(); //碰撞体

        for (int i = 0; i < collider2Ds.Length; i++)
            collider2Ds[i].enabled = false; //不可用

        birdAnim.gameObject.SetActive(false); //看不见了
    }

    /// <summary>
    /// 重写飞行结束后的方法
    /// </summary>
    protected override void FlyNext()
    {
        if (!isBoom)
        {
            Instantiate(littleBlackBoom, transform.position, Quaternion.identity); //生成爆炸效果
            Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
            AudioUtil.sInstance.AudioPlay(explosionClip, transform.position); //播放爆炸音效
            BoomPig();
            bomb.TargetRadius = 1.5f; //目标尺寸
            bomb.Power = 5; //炸弹力量
            bomb.MaxSpeed = 20; //最大速度

            Collider2D[] collider2Ds = GetComponents<Collider2D>(); //碰撞体

            for (int i = 0; i < collider2Ds.Length; i++)
                collider2Ds[i].enabled = false; //不可用

            birdAnim.gameObject.SetActive(false); //小鸟动画不可见

            Instantiate(bomb, transform.position, Quaternion.identity); //生成一个爆炸效果
            Invoke("NextBird", 2f);
        }
        else
        {
            NextBird(); //下一只小鸟
        }
    }

    /// <summary>
    /// 下一只小鸟
    /// </summary>
    private void NextBird()
    {
        CameraVary.sInstance.Home(); //相机归位
        GameManager.sInstance.NextBird(); //替换下一只小鸟
        Destroy(gameObject); //移除小鸟
    }

    /// <summary>
    /// 让猪 木板爆炸
    /// </summary>
    private void BoomPig()
    {
        for (int i = 0; i < pigList.Count; i++)
        {
            if (pigList[i] != null && pigList[i])
                pigList[i].Dead(); //猪毁灭
        }

        for (int i = 0; i < blockList.Count; i++)
        {
            if (blockList[i] != null)
                blockList[i].Dead(); //木板毁灭
        }
    }
}