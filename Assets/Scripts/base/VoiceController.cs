using utils;
using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Start()
    {
        if (AudioUtil.sInstance.hasVoice)
            transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// 改变声音选项
    /// </summary>
    public void ChangeVoice()
    {
        AudioUtil.sInstance.hasVoice = !PlayerPrefUtil.isVoice();
        PlayerPrefUtil.setVoice(AudioUtil.sInstance.hasVoice); //设置一下声音变量
        if (AudioUtil.sInstance.hasVoice)
        {
            AudioUtil.sInstance.PlayBGM(); //播放背景音乐
            transform.GetChild(0).gameObject.SetActive(false); //有声音
        }
        else
        {
            AudioUtil.sInstance.StopPlayBGM(); //停止背景音乐
            transform.GetChild(0).gameObject.SetActive(true); //静音
        }
    }
}