using System;
using System.Collections.Generic;
using utils;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject highScore; //最高分
    private Text scoreText; //分数文本
    private int nowScore;

    public static Score sInstance; //单例模式
    public List<Sprite> spriteNumList; //数字列表
    public GameObject numObject; //用来表示数字的对象

    private GameObject highNum; //最高分的数字
    private GameObject scoreNum; //成绩的数字

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        sInstance = this;
        highScore = transform.parent.Find("HighScore").gameObject; //得到最高分
        highNum = highScore.transform.Find("num").gameObject; //最高分数字
        scoreNum = transform.Find("num").gameObject; //存放数字的地方

        int highestScore = PlayerPrefUtil.GetNowMapLevelScore();

        if (highestScore != 0)
        {
            highScore.SetActive(true); //可见
            AddScoreImage(highestScore, highNum.transform, -25, 10); //加到最高分上
        }
        else
            highScore.SetActive(false); //不可见
    }


    /// <summary>
    /// 添加分数图片
    /// </summary>
    private void AddScoreImage(int scoreInt, Transform parentNum, int x, int y)
    {
        String scoreStr = scoreInt + "";
        int digit = scoreStr.Length; //成绩的位数


        for (int i = parentNum.childCount; i < digit; i++)
        {
            GameObject numImage = Instantiate(numObject, parentNum); //生成一个数字
            numImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + i * 13, y); //设置数字的位置
        }

        for (int i = 0; i < digit; i++)
        {
            String numStr = scoreStr.Substring(i, 1);
            int numInt = int.Parse(numStr); //解析一下数字
            parentNum.GetChild(i).GetComponent<Image>().overrideSprite = spriteNumList[numInt]; //设置一下数字
        }
    }

    /// <summary>
    /// 添加成绩
    /// </summary>
    public void AddScore(int addScore)
    {
        nowScore += addScore; //加分
        AddScoreImage(nowScore, scoreNum.transform, -43, -2);
    }

    /// <summary>
    /// 保存分数
    /// </summary>
    public void SaveScore()
    {
        int highestScore = PlayerPrefUtil.GetNowMapLevelScore();
        if (nowScore > highestScore)
        {
            PlayerPrefUtil.SetNowMapLevelHighestScore(nowScore); //保存一下最高分
        }
    }

    /// <summary>
    /// 得到当前分数
    /// </summary>
    /// <returns></returns>
    public int getNowScore()
    {
        return nowScore;
    }
}