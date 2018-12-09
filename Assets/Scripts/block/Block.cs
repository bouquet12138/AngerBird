using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float maxSpeed = 10; //被销毁的最大的速度
    public float minSpeed = 5; //被销毁的最小的速度

    public GameObject boom; //爆炸效果
    public GameObject score; //成绩

    public int addScore = 3000; //撞碎一个木块所加的成绩

    private int blood = 3; //血量

    public Sprite collision1Sprite; //碰撞一次的图片
    public Sprite collision2Sprite; //碰撞一次的图片

    public List<AudioClip> collisionAudios; //碰撞时的声音
    public List<AudioClip> damageAudios; //碰撞时的声音
    public List<AudioClip> destroyAudios; //销毁时的声音

    private SpriteRenderer spriteRenderer; //图片渲染器

    /// <summary>
    /// 唤醒时
    /// </summary>
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //渲染器
    }

    /// <summary>
    ///  碰撞检测
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        // print("速度 " + other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > maxSpeed) //死亡
        {
            Dead();
        }
        else if (other.relativeVelocity.magnitude > minSpeed)
        {
            blood--;
            switch (blood)
            {
                case 2:
                    spriteRenderer.sprite = collision1Sprite; //受伤1的图片
                    int index1 = Random.Range(0, collisionAudios.Count);
                    AudioUtil.sInstance.AudioPlay(collisionAudios[index1], transform.position); //播放碰撞的音效
                    break;
                case 1:
                    spriteRenderer.sprite = collision2Sprite; //受伤2图片
                    int index2 = Random.Range(0, damageAudios.Count);
                    AudioUtil.sInstance.AudioPlay(collisionAudios[index2], transform.position); //播放碰撞的音效
                    break;
                case 0:
                    Dead();
                    break;
            }
        }
    }

    /// <summary>
    /// 木块销毁
    /// </summary>
    public void Dead()
    {
        Score.sInstance.AddScore(addScore); //加分
        int index = Random.Range(0, destroyAudios.Count);
        AudioUtil.sInstance.AudioPlay(destroyAudios[index], transform.position); //播放销毁的声音
        Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
        Instantiate(score, transform.position, Quaternion.identity); //生成加分效果
        Destroy(gameObject); //销毁自己
    }
}