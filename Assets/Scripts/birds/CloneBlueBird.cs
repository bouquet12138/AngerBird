using System.Collections.Generic;
using UnityEngine;

public class CloneBlueBird : MonoBehaviour
{
    public GameObject boom; //爆炸效果
    public List<AudioClip> collisionAudios; //碰撞的声音们
    public AudioClip destroyAudio; //销毁的声音


    private bool showPath = true; //是否展示路径
    private int pathId; //路径id
    [HideInInspector] public Path path; //路径 对象

    private SpriteRenderer spriteRenderer; //渲染图片的
    public Sprite hurtSprite; //受伤图片

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //渲染器
        Invoke("Dead", 3); //3秒后销毁小鸟
    }

    /// <summary>
    /// 视图更新的时候
    /// </summary>
    private void Update()
    {
        if (showPath)
        {
            print("克隆小鸟 绘制路径");
            pathId++;
            if (path != null)
            {
                path.AddPath(pathId, transform.position); //添加路径
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            return;

        showPath = false; //不可以展示路径了
        if (other.relativeVelocity.magnitude > 6) //相对速度大于6
        {
            Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效

            int audioIndex = Random.Range(0, collisionAudios.Count);
            AudioUtil.sInstance.AudioPlay(collisionAudios[audioIndex], transform.position); //播放一个碰撞声音
            spriteRenderer.sprite = hurtSprite; //替换为受伤图片
        }
    }

    /// <summary>
    /// 销毁小鸟
    /// </summary>
    private void Dead()
    {
        showPath = false; //不能展示路径了
        Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
        AudioUtil.sInstance.AudioPlay(destroyAudio, transform.position); //播放销毁的声音
        Destroy(gameObject); //移除自己
    }
}