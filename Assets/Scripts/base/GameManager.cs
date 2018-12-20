using System.Collections;
using System.Collections.Generic;
using birds;
using utils;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sInstance;

    private readonly List<Bird> birds = new List<Bird>(); //存放场景中的小鸟
    public List<Pig> pigs = new List<Pig>(); //存放场景中的小猪

    private float allScore; //总成绩

    [HideInInspector] public bool gameIsOver; //比赛是否结束
    private GameObject winUi; //胜利的UI
    private GameObject loseUi; //失败的UI

    private GameObject stars; //存放星星的物体

    public Vector3 originPos; //小鸟的初始位置

    public AudioClip birdWinAudio; //小鸟胜利的音乐

    private void Awake()
    {
        sInstance = this; //单例模式

        GameObject canvas = GameObject.Find("Canvas");
        winUi = canvas.transform.Find("WinPanel").gameObject;
        loseUi = canvas.transform.Find("LosePanel").gameObject;
        stars = winUi.transform.Find("bg").Find("stars").gameObject;
    }

    /// <summary>
    /// 这个时候去绑定小鸟们
    /// </summary>
    private void Start()
    {
        Transform birdsTransform = transform.parent.Find("birds"); //找到存放小鸟的游戏物体
        Vector2 springPos = new Vector2();

        for (int i = 0; i < birdsTransform.childCount; i++)
        {
            if (i == 0)
            {
                originPos = birdsTransform.GetChild(i).position; //设置一下位置
                springPos = birdsTransform.GetChild(i).GetComponent<SpringJoint2D>().connectedAnchor;
            }
            else
            {
                birdsTransform.GetChild(i).GetComponent<SpringJoint2D>().connectedAnchor = springPos;
            }

            birds.Add(birdsTransform.GetChild(i).GetComponent<Bird>()); //将小鸟添加进来
            allScore += birds[i].addScore;
        }

        Transform pigTransform = transform.parent.Find("pigs"); //找到存放小猪的游戏物体

        for (int i = 0; i < pigTransform.childCount; i++)
        {
            pigs.Add(pigTransform.GetChild(i).GetComponent<Pig>()); //将小猪添加进去
            allScore += pigs[i].addScore; //所用的成绩加加
        }

        Transform blockTransform = transform.parent.Find("blocks"); //找到存放小猪的游戏物体

        for (int i = 0; i < blockTransform.childCount; i++)
        {
            if (blockTransform.GetChild(i).GetComponent<Block>() != null)
                allScore += blockTransform.GetChild(i).GetComponent<Block>().addScore; //所有的成绩加加
        }

        print("游戏场景的所有成绩 allScore " + allScore);

        Initialized(); //初始化一下
    }

    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].TriggerCollider.enabled = true; //触发碰撞器启用
                birds[i].Ready();
                birds[i].springJoint2D.enabled = true; //启用
                birds[i].enabled = true; //可用
            }
            else
            {
                birds[i].TriggerCollider.enabled = false; //触发碰撞器禁用
                birds[i].springJoint2D.enabled = false; //启用
                birds[i].enabled = false; //可用
            }
        }
    }

    /// <summary>
    /// 下一只小鸟
    /// </summary>
    public void NextBird()
    {
        if (gameIsOver) //游戏结束就返回
            return;

        if (birds.Count > 0)
        {
            birds.RemoveAt(0); //移除第一只小鸟
        }

        if (pigs.Count > 0)
        {
            if (birds.Count > 0) //还有小鸟，替换下一只小鸟
            {
                Initialized(); //替换小鸟
            }
            else
            {
                gameIsOver = true; //游戏结束
                loseUi.SetActive(true); //失败UI可见
            }
        }
        else
        {
            Win(); //游戏胜利
        }
    }

    /// <summary>
    /// 判断是否胜利
    /// </summary>
    public void IsSuccess()
    {
        if (!gameIsOver) //没有游戏结束
        {
            if (pigs.Count <= 0)
            {
                if (birds.Count > 0 && birds[0].IsIdle())
                {
                    Win();
                }
            }
        }
    }

    /// <summary>
    /// 胜利的效果
    /// </summary>
    private void Win()
    {
        gameIsOver = true; //游戏结束
        AudioUtil.sInstance.AudioPlay(birdWinAudio); //播放小鸟胜利音乐
        StartCoroutine("ShowBird"); //开启协程
    }

    /// <summary>
    /// 展示小鸟
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowBird()
    {
        if (birds.Count > 0)
        {
            birds[0].enabled = true; //可用

            for (int i = 0; i < birds.Count; i++)
            {
                yield return new WaitForSeconds(1f);
                birds[i].Win(); //播放胜利特效
            }
        }

        yield return new WaitForSeconds(1f); //休息1秒
        winUi.SetActive(true); //胜利了
    }

    /// <summary>
    /// 展示星星
    /// </summary>
    public void ShowStart()
    {
        int counts = 1; //用户得到的星星数

        if (Score.sInstance.getNowScore() / allScore > 3 / 4f)
            counts = 3; //三颗星
        else if (Score.sInstance.getNowScore() / allScore > 0.5f)
            counts = 2; //三颗星

        if (counts >= 2)
            StartCoroutine("Show", 3);
        else if (counts == 1)
            StartCoroutine("Show", 2);
        else
            StartCoroutine("Show", 1);
    }

    /// <summary>
    /// 显示星星
    /// </summary>
    /// <param name="num">星星数目</param>
    /// <returns></returns>
    private IEnumerator Show(int num)
    {
        saveStar(num); //存储一下

        for (int i = 0; i < num; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true); //激活星星
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// 将星星存储一下
    /// </summary>
    private void saveStar(int num)
    {
        int starNum = PlayerPrefUtil.GetNowMapLevelStarNum(); //得到星星数

        if (num > starNum)
        {
            PlayerPrefUtil.addTotalStar(num - starNum); //添加总星星
            PlayerPrefUtil.addNowMapTotalStar(num - starNum); //添加当前地图星星
            PlayerPrefUtil.SetNowMapLevelStarNum(num); //设置星星数
        }
    }
}