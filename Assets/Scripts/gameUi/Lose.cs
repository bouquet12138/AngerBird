using System.Collections;
using System.Collections.Generic;
using utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public AudioClip loseAudio; //失败的声音

    /// <summary>
    /// 播放失败的音乐
    /// </summary>
    public void PlayLose()
    {
        AudioUtil.sInstance.AudioPlay(loseAudio); //播放失败声音
    }

    /// <summary>
    /// 主页
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1; // timeScale 恢复正常
        SceneLoadUtil.LoadLevelScene();
    }

    /// <summary>
    /// 重新尝试
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1; // timeScale 恢复正常
        SceneLoadUtil.ReLoadGameScene(); //重新加载自己
    }
}