using UnityEngine;

public class PigAnim : MonoBehaviour
{
    private Animator pigAnimator;
    public AudioClip smileAudio; //笑的声音
    public AudioClip callAudio; //猪哼哼的声音

    private void Awake()
    {
        pigAnimator = GetComponent<Animator>(); //得到动画状态机  
    }

    /// <summary>
    /// 改变猪的当前状态
    /// </summary>
    public void ChangeStatus()
    {
        int value = Random.Range(0, 3);


        if (value == 2) //2是笑
        {
            int v = Random.Range(0, 10);
            if (v % 3 == 0)
                AudioUtil.sInstance.AudioPlay(smileAudio, transform.position); //播放笑的音效
        }
        else if (value == 1) //1是眨眼
        {
            int v = Random.Range(0, 10);
            if (v % 3 == 0)
                AudioUtil.sInstance.AudioPlay(callAudio, transform.position); //播放叫的音效
        }

        pigAnimator.SetInteger("status", value);
    }
}