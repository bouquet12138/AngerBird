using utils;
using UnityEngine;

public class AudioUtil : MonoBehaviour
{
    private AudioSource audioSource; //声音源组件

    public static AudioUtil sInstance; //单例模式
    [HideInInspector] public bool hasVoice;


    private void Awake()
    {
        sInstance = this; //单例模式

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        hasVoice = PlayerPrefUtil.isVoice();

        if (!hasVoice)
            audioSource.Stop(); //停止播放音乐
        else
            audioSource.Play(); //播放音乐
    }

    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopPlayBGM()
    {
        audioSource.Stop(); //停止播放音乐
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBGM()
    {
        audioSource.Play(); //播放音乐
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">要播放的音效片段</param>
    /// <param name="position">音效播放的位置</param>
    public void AudioPlay(AudioClip clip, Vector3 position)
    {
        if (hasVoice) //有声音才播放
        {
            AudioSource.PlayClipAtPoint(clip, position); //在此处播放音效
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">要播放的音效片段</param>
    /// <param name="position">音效播放的位置</param>
    public void AudioPlay(AudioClip clip)
    {
        if (hasVoice && clip != null) //有声音才播放
        {
            AudioSource.PlayClipAtPoint(clip, transform.position); //在此处播放音效
        }
    }
}