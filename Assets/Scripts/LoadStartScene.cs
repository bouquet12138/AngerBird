using utils;
using UnityEngine;

public class LoadStartScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //想清除的时候可以用这个 PlayerPrefs.DeleteAll(); //清除所有
        Invoke("LoadStart", 2f); //2秒后跳转到开始场景
    }

    private void LoadStart()
    {
        SceneLoadUtil.LoadStartScene(); //加载开始场景
    }
}