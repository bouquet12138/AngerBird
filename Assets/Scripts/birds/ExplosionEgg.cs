using System.Collections.Generic;
using UnityEngine;

public class ExplosionEgg : MonoBehaviour
{
    private readonly List<Pig> pigList = new List<Pig>(); //初始化一下集合
    private readonly List<Block> blockList = new List<Block>(); //木块的集合

    public GameObject boom; //爆炸特效
    public AudioClip boomClip; //爆炸声音


    /// <summary>
    /// 碰撞器进入
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy") //如果是敌军
        {
            if (other.gameObject.GetComponent<Pig>() != null)
            {
                pigList.Add(other.gameObject.GetComponent<Pig>()); //将猪加进来
            }
            else if (other.gameObject.GetComponent<Block>() != null)
            {
                blockList.Add(other.gameObject.GetComponent<Block>()); //将木块添加进来
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
    }

    /// <summary>
    /// 碰撞体进入
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            AudioUtil.sInstance.AudioPlay(boomClip, transform.position); //播放爆炸音效
            Instantiate(boom, transform.position, Quaternion.identity); //生成一个爆炸特效
            for (int i = 0; i < pigList.Count; i++)
                if (pigList[i] != null && pigList[i])
                    pigList[i].Dead(); //猪毁灭

            for (int i = 0; i < blockList.Count; i++)
                if (blockList[i] != null)
                    blockList[i].Dead(); //木板毁灭

            Destroy(gameObject); //销毁这个游戏物体
        }
    }
}