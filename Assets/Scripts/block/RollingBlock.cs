using UnityEngine;

public class RollingBlock : Block
{
    private Rigidbody2D blockRigidbody;
    private AudioSource audioSource; //播放音效的组件
    private bool isPlayAudio; //是否正在展示音乐
    private float nowVolume; //音量

    protected override void Awake()
    {
        base.Awake();
        blockRigidbody = GetComponent<Rigidbody2D>(); //刚体
        audioSource = GetComponent<AudioSource>(); //声音组件
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioUtil.sInstance.hasVoice)
        {
            if (audioSource.isPlaying)
                audioSource.Stop(); //停止播放
        }

        if (Mathf.Abs(blockRigidbody.velocity.x) > 0.1f)
        {
            if (!isPlayAudio && AudioUtil.sInstance.hasVoice) //如果没有播放滚动音乐
            {
                audioSource.Play(); //播放音效
                isPlayAudio = true; //已经开始播放了
            }

            float volume = (int) (Mathf.Abs(blockRigidbody.velocity.x) * 10);

            volume /= 10;
            volume = Mathf.Clamp(volume, 0, 1);

            if (nowVolume != volume)
            {
                nowVolume = volume;
                audioSource.volume = nowVolume; //设置一下音量
            }
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop(); //停止播放

            isPlayAudio = false; //不在播放音乐
        }
    }
}