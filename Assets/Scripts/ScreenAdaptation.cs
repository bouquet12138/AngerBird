using UnityEngine;

public class ScreenAdaptation : MonoBehaviour
{
    [HideInInspector] public float moveX;
    public static ScreenAdaptation sInstance;

    /// <summary>
    /// 初次唤醒的时候
    /// </summary>
    private void Awake()
    {
        sInstance = this;
        float xGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数

        moveX = (Environment.DEFAULT_X_GRID - xGridNum) / 2;
        transform.position =
            new Vector3(moveX, 0, 0); //移动物体方向
    }
}