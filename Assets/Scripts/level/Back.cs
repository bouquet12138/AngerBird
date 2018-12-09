using utils;
using UnityEngine;

public class Back : MonoBehaviour
{
    public GameObject maps; //地图们
    public GameObject panels; //地图们
    public AudioClip backAudio; //返回的声音 
    private int nowPanel; //当前展示的面板

    public static Back sInstance; //单例模式

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        sInstance = this;
    }

    /// <summary>
    /// 当返回按钮按下时
    /// </summary>
    public void OnBackDown()
    {
        AudioUtil.sInstance.AudioPlay(backAudio); //播放返回的声音
        if (maps.activeSelf)
        {
            SceneLoadUtil.LoadStartScene(); //去到开始游戏页面
        }
        else
        {
            SwitchMap(); //切换到map界面
        }
    }

    /// <summary>
    /// 切换到map界面
    /// </summary>
    public void SwitchMap()
    {
        maps.SetActive(true);
        panels.transform.GetChild(nowPanel).gameObject.SetActive(false); //让当前展示的panel面板禁用掉
    }

    /// <summary>
    /// 切换到panel界面
    /// </summary>
    public void SwitchPanels(int nowPanelIndex)
    {
        nowPanel = nowPanelIndex; //当前要展示的面板
        maps.SetActive(false); //地图隐藏
        panels.transform.GetChild(nowPanel).gameObject.SetActive(true); //启用
    }
}