using System.Collections;
using System.Collections.Generic;
using utils;
using UnityEngine;

public class AsyncLoading : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        SceneLoadUtil.AsyncLoadGameScene(); //异步加载游戏场景
    }

    // Update is called once per frame
    void Update()
    {
    }
}