using utils;
using UnityEngine;

namespace gameUi
{
    public class Store : MonoBehaviour
    {
        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Awake()
        {
            int nowMap = PlayerPrefUtil.GetNowMap(); //当前地图
            transform.GetChild(nowMap).gameObject.SetActive(true); //激活游戏介绍
        }
    }
}