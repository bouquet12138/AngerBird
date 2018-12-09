using utils;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Awake()
    {
        Instantiate(Resources.Load(PlayerPrefUtil.GetNowMapLevelName())); //加载关卡
    }
}