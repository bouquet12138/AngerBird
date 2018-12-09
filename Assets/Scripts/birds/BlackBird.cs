using System.Collections.Generic;
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
        isBoom = true; //爆炸了
        path.AddSkillPath(transform.position); //添加一个特效
        AudioUtil.sInstance.AudioPlay(explosionClip, transform.position); //播放爆炸音效
        Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效
        Instantiate(blackBoom, transform.position, Quaternion.identity); //生成一个碰撞特效
        BoomPig();
        OnClear();
        bomb.targetRadius = 4f;
        bomb.targetRadius = 0.5f;
        Instantiate(bomb, transform.position, Quaternion.identity); //生成一个爆炸效果
    }

    /// <summary>
    /// 碰撞器进入
    /// </summary>
    /// <param name="other"></param>
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy") //如果是敌军
        {
            if (other.gameObject.GetComponent<Pig>() != null)
            {
                if (!pigList.Contains(other.gameObject.GetComponent<Pig>()))
                {
                    pigList.Add(other.gameObject.GetComponent<Pig>()); //将猪加进来
                }
            }
            else if (other.gameObject.GetComponent<Block>() != null)
            {
                if (!blockList.Contains(other.gameObject.GetComponent<Block>()))
                {
                    blockList.Add(other.gameObject.GetComponent<Block>()); //将木块添加进来
                }
            }
        }
    }

    /// <summary>
    /// 触发器离开
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy") //如果是敌军
        {
            if (other.gameObject.GetComponent<Pig>() != null)
            {
                pigList.Remove(other.gameObject.GetComponent<Pig>()); //将猪移除去
            }
            else if (other.gameObject.GetComponent<Block>() != null)
            {
                blockList.Remove(other.gameObject.GetComponent<Block>()); //将木块移出去
            }
        }
    }*/

    /// <summary>
    /// 该清除的清除
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnClear()
    {
        rigidBody2D.velocity = Vector2.zero; //速度归零
        Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
        showPath = false; //不再展示路径
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
            bomb.targetRadius = 1.5f;
            bomb.targetRadius = 0.5f;

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

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (canShowSkill && !isBoom) //如果可以展示技能
        {
            CircleCollider2D[] collider2Ds = GetComponents<CircleCollider2D>(); //碰撞体

            if (collider2Ds != null && collider2Ds.Length != 0)
            {
                collider2Ds[0].radius = collider2Ds[0].radius * 0.6f; //半径变小
            }
        }

        base.OnCollisionEnter2D(other);
    }
}