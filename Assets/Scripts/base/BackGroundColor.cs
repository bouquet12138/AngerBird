using utils;
using UnityEngine;

public class BackGroundColor : MonoBehaviour
{
    private readonly float[,] colorArr =
    {
        {0x92, 0xCC, 0xDE}, {0x90, 0xCD, 0xED}, {0xC1, 0xE9, 0xF2}, {0, 0, 0},
        {0x09, 0x55, 0x68}, {0x0E, 0x0E, 0x1A}, {0x64, 0xD7, 0xF3}
    };

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        int nowMap = PlayerPrefUtil.GetNowMap();
        GetComponent<Camera>().backgroundColor =
            new Color(colorArr[nowMap, 0] / 255, colorArr[nowMap, 1] / 255, colorArr[nowMap, 2] / 255);
    }
}