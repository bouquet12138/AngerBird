using UnityEngine;

public class Quit : MonoBehaviour
{
    public GameObject quitPanel; //退出面板

    /// <summary>
    /// 展示退出面板
    /// </summary>
    public void ShowQuitPanel()
    {
        quitPanel.SetActive(true); //激活退出面板
    }

    /// <summary>
    /// 隐藏退出面板
    /// </summary>
    public void HideQuitPanel()
    {
        quitPanel.SetActive(false); //使退出面板失活
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}