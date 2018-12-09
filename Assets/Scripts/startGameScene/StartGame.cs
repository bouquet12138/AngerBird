using utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public AudioClip confirmClip;//确定的声音 
    /// <summary>
    /// 加载游戏
    /// </summary>
    public void LoadGame()
    {
        SceneLoadUtil.LoadLevelScene(); //加载场景选择页面
        AudioUtil.sInstance.AudioPlay(confirmClip);//播放一个确定的声音
    }
}