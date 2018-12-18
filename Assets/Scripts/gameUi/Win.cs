using utils;
using UnityEngine;

public class Win : MonoBehaviour
{
    public AudioClip winAudio; //胜利的音乐
    public GameObject homeBt; //主页按钮
    public GameObject retryBt; //重试按钮
    public GameObject nextBt; //主页按钮

    /// <summary>
    /// 显示的时候
    /// </summary>
    private void Start()
    {
        int nowLevel = PlayerPrefUtil.GetNowLevel(); //当前关卡
        int nowMap = PlayerPrefUtil.GetNowMap(); //当前地图

        int sumLevel = GameLevelUtil.sLevelNumArray[nowMap];

        if (nowLevel + 1 == sumLevel)
        {
            nextBt.SetActive(false); //失活
            homeBt.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-60, homeBt.GetComponent<RectTransform>().anchoredPosition.y); //重新定位
            retryBt.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(60, retryBt.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    /// <summary>
    /// 胜利
    /// </summary>
    public void ShowStar()
    {
        GameManager.sInstance.ShowStart(); //展示星星
    }

    public void PlayWin()
    {
        AudioUtil.sInstance.StopPlayBGM(); //停止播放背景音乐
        Score.sInstance.SaveScore(); //保存一下分数
        AudioUtil.sInstance.AudioPlay(winAudio); //播放胜利音乐
    }

    /// <summary>
    /// 主页
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1; // timeScale 恢复正常
        SceneLoadUtil.LoadLevelScene(); //去到关卡选择页面
    }

    /// <summary>
    /// 重新尝试
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1; // timeScale 恢复正常
        SceneLoadUtil.ReLoadGameScene(); //重新加载自己 
    }


    /// <summary>
    ///下一关
    /// </summary>
    public void NextLevel()
    {
        Time.timeScale = 1; // timeScale 恢复正常
        PlayerPrefUtil.SetNowLevel(PlayerPrefUtil.GetNowLevel() + 1); //下一关
        SceneLoadUtil.LoadGameScene(); //重新加载自己 
    }
}