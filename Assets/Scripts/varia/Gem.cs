using UnityEngine;

public class Gem : MonoBehaviour
{
    public AudioClip gemClip; //吃到钻石音效
    public int addScore = 5000; //添加的分数
    public GameObject addScoreObject; //加分特效

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("other.gameObject.name " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            DestroyGem();
        }
        else if (other.relativeVelocity.magnitude > 8)
        {
            DestroyGem();
        }
    }

    /// <summary>
    /// 销毁钻石
    /// </summary>
    private void DestroyGem()
    {
        Score.sInstance.AddScore(addScore); //加5000分	
        AudioUtil.sInstance.AudioPlay(gemClip, transform.position); //播放钻石声音
        Instantiate(addScoreObject, transform.position, Quaternion.identity); //生成加分效果
        Destroy(gameObject); //销毁自己
    }
}