using utils;
using UnityEngine;
using UnityEngine.UI;

public class LevelUi : MonoBehaviour
{
    public bool isSelected; //是否选中
    private int nowIndex; //关卡所在索引
    [HideInInspector] public int mapIndex; //它所在的地图索引

    public Sprite levelBg; //背景

    public Sprite starNone; //没星星
    public Sprite starOne; //一个星星
    public Sprite starTwo; //二个星星
    public Sprite starThree; //三个星星

    private void Awake()
    {
        print("Screen.width " + Screen.width);
    }

    /// <summary>
    /// 开始的方法
    /// </summary>
    void Start()
    {
        if (isSelected)
            GetComponent<Image>().overrideSprite = levelBg; //设置下背景
    }

    /// <summary>
    /// 设置当前关卡
    /// </summary>
    public void SetNowIndex(int nowIndex)
    {
        this.nowIndex = nowIndex; //设置当前关卡的索引
        if (nowIndex == 0)
            isSelected = true; //选择了
        else
        {
            int starNum = PlayerPrefUtil.GetMapLevelStarNum(mapIndex, nowIndex - 1); //得到上一关卡星星数

            if (starNum > 0) //如果上一个关卡的星星数大于0那么解锁本关
                isSelected = true;
        }


        if (isSelected)
        {
            GetComponent<Image>().overrideSprite = levelBg; //替换下背景
            GameObject textObj = transform.GetChild(1).gameObject; //找到数字子物体
            Image starNumImg = transform.GetChild(0).GetComponent<Image>(); //星星数图片
            starNumImg.gameObject.SetActive(true); //启用
            textObj.SetActive(true); //启用

            textObj.GetComponent<Text>().text = nowIndex + 1 + ""; //设置关卡文本

            int starNum = PlayerPrefUtil.GetLevelStarNum(nowIndex); //得到当前关卡星星数

            switch (starNum)
            {
                case 0:
                    starNumImg.overrideSprite = starNone;
                    break;
                case 1:
                    starNumImg.overrideSprite = starOne;
                    break;
                case 2:
                    starNumImg.overrideSprite = starTwo;
                    break;
                case 3:
                    starNumImg.overrideSprite = starThree;
                    break;
            }
        }
    }

    /// <summary>
    /// 鼠标点击之后的选择事件
    /// </summary>
    public void Select()
    {
        if (isSelected)
        {
            PlayerPrefUtil.SetNowLevel(nowIndex); //设置当前关卡
            if (nowIndex == 0)
            {
                SceneLoadUtil.LoadStoreScene(); //加载故事场景
            }
            else
            {
                SceneLoadUtil.LoadGameScene(); //加载游戏场景
            }
        }
    }
}