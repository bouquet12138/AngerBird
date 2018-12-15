using utils;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int needStartNum; //需要几颗星星才能解锁

    public AudioClip selectAudio; //选中的声音

    //TODO:改为private
    public bool isSelect; //是否选中

    private GameObject lockImg; //锁
    private GameObject stars; //星星

    private Text needStarsText; //需要的星星数
    private Text hasStarsCount; //这个地图已经有多少星星了

    private Image bgImage; //背景Image

    private int nowIndex; //地图所在索引

    private void Awake()
    {
        lockImg = transform.GetChild(1).Find("lock").gameObject; //锁对象
        stars = transform.GetChild(1).Find("stars").gameObject; //星星对象
        needStarsText = lockImg.transform.Find("needStarsCount").GetComponent<Text>(); //得到需要星星数的文本
        bgImage = transform.GetChild(0).GetComponent<Image>(); //背景图
        hasStarsCount = stars.transform.Find("starsCount").GetComponent<Text>(); //得到显示星星数的文本
    }

    /// <summary>
    /// 设置背景图
    /// </summary>
    public void SetBg(Sprite sprite)
    {
        bgImage.overrideSprite = sprite; //设置背景图
    }

    /// <summary>
    /// 设置当前是哪个关卡
    /// </summary>
    /// <param name="nowIndex"></param>
    public void SetNowIndex(int nowIndex)
    {
        this.nowIndex = nowIndex;

        if (PlayerPrefUtil.GetTotalStars() >= needStartNum)
            isSelect = true;

        needStarsText.text = needStartNum + ""; //设进去

        if (isSelect)
        {
            lockImg.SetActive(false); //失活
            stars.SetActive(true); //启用

            int mapTotalStars = PlayerPrefUtil.GetMapTotalStars(nowIndex);
            int levelNum = GameLevelUtil.sLevelNumArray[nowIndex]; //得到关卡数
            hasStarsCount.text = mapTotalStars + "/" + levelNum * 3; //设置文本
        }
    }

    public void Select()
    {
        if (isSelect)
        {
            AudioUtil.sInstance.AudioPlay(selectAudio); //播放选择的声音
            PlayerPrefUtil.SetNowMap(nowIndex); //存储一下当前地图索引
            Back.sInstance.SwitchPanels(nowIndex); //切换一下面板
        }
    }
}