using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanelManager : MonoBehaviour
{
    public GameObject mapSelectPrefab; //地图选择的预制体
    public GameObject comeSoonPrefab; //即将推出的预制体


    public List<Sprite> mapBg; //背景图片们
    public List<int> needStars; //过关需要的星星数

    private void Awake()
    {
        RectTransform mapSelectRect = mapSelectPrefab.GetComponent<RectTransform>();
        RectTransform comeSoonRect = comeSoonPrefab.GetComponent<RectTransform>();


        float passWidth = mapSelectRect.sizeDelta.x; //关卡的宽
        float passHeight = mapSelectRect.sizeDelta.y; //关卡的高


        float spaceWidth = 0.15f * Screen.height; //每个预制体之间的间隔

        float scale = Screen.height * 0.6f / passHeight; //计算缩放比例
        passWidth *= scale;

        mapSelectRect.sizeDelta = new Vector2(passWidth, Screen.height * 0.6f); //设置主物体大小

        RectTransform rect1 = mapSelectPrefab.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rect2 = mapSelectPrefab.transform.GetChild(1).GetComponent<RectTransform>();

        rect1.sizeDelta = new Vector2(passWidth, Screen.height * 0.6f); //背景大小
        rect2.localScale = new Vector2(scale * rect2.localScale.x, scale * rect2.localScale.y); //缩放一下

        comeSoonRect.sizeDelta = new Vector2(passWidth, Screen.height * 0.6f); //马上见的尺寸


        int panelWidth = (int) (spaceWidth * (mapBg.Count + 2) + passWidth * (mapBg.Count + 1)); //面板的宽度


        GetComponent<HorizontalLayoutGroup>().padding = new RectOffset((int) spaceWidth, 0,
            (int) (0.1f * Screen.height), (int) (0.3f * Screen.height)); //上面0.1 屏幕高的距离 下面 0.3屏幕高的距离

        for (int i = 0; i < mapBg.Count; i++)
        {
            GameObject mapSelectGameObject = Instantiate(mapSelectPrefab); //生成一个地图选择预制体

            mapSelectGameObject.transform.parent = transform; //挂载到自身身上

            MapSelect mapSelect = mapSelectGameObject.GetComponent<MapSelect>(); //地图选择

            mapSelect.SetBg(mapBg[i]); //设一下背景
            mapSelect.needStartNum = needStars[i]; //设一下需要的星星数
            mapSelect.SetNowIndex(i); //设置是哪个关卡
        }

        GameObject comeSoon = Instantiate(comeSoonPrefab); //生成一个预制体
        comeSoon.transform.parent = transform; //挂载到自身上    

        GetComponent<RectTransform>().offsetMax =
            new Vector2(panelWidth - Screen.width, GetComponent<RectTransform>().offsetMax.y);
    }
}