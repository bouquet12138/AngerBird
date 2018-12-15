using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10; //被销毁的最大的速度
    public float minSpeed = 5; //被销毁的最小的速度

    public GameObject boom; //爆炸效果
    public GameObject score; //成绩

    public int addScore = 10000; //打死一个猪所加的成绩

    private int blood = 3; //血量

    private PigAnim pigAnim; //得到小猪身上的动画

    public AudioClip hurtAudio; //受伤时的声音
    public AudioClip deadAudio; //死掉时的声音

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        pigAnim = GetComponent<PigAnim>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == 0);
        }
    }

    /// <summary>
    ///  碰撞检测
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        //print("速度 " + other.relativeVelocity.magnitude);
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
                    for (int i = 0; i < transform.childCount; i++)
                        transform.GetChild(i).gameObject.SetActive(i == 1);
                    break;
                case 1:
                    for (int i = 0; i < transform.childCount; i++)
                        transform.GetChild(i).gameObject.SetActive(i == 2);

                    AudioUtil.sInstance.AudioPlay(hurtAudio, transform.position); //播放受伤的音效
                    break;
                case 0:
                    Dead();
                    break;
            }
        }
    }

    /// <summary>
    /// 实时更新
    /// </summary>
    private void Update()
    {
        if (transform.position.y < -5)
        {
            Dead(); //小猪死亡
        }
    }


    /// <summary>
    /// 小猪死掉了
    /// </summary>
    public void Dead()
    {
        GameManager.sInstance.pigs.Remove(this); //移除小猪
        Score.sInstance.AddScore(addScore);
        AudioUtil.sInstance.AudioPlay(deadAudio, transform.position); //播放死掉的声音
        Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
        Instantiate(score, transform.position, Quaternion.identity); //生成加分效果
        Destroy(gameObject); //销毁自己
    }
}