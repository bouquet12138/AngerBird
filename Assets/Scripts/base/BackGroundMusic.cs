using System.Collections.Generic;
using UnityEngine;

using utils;

public class BackGroundMusic : MonoBehaviour
{
    public List<AudioClip> backGroundMusic; //背景音乐
    private AudioSource audioSource; //声音源组件


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        int nowMap = PlayerPrefUtil.GetNowMap();
        audioSource.clip = backGroundMusic[nowMap]; //设置背景音乐
    }
}