using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingBack : MonoBehaviour
{
    private Transform backs;

    private Transform backGreens;

    private Transform greens;
    private Transform bricks; //砖块
    public GameObject backGroundPrefab;
    public GameObject backGreenPrefab; //背景草地
    public GameObject brickPrefab;
    public GameObject greenPrefab; //草地图片

    private readonly List<RectTransform> backList = new List<RectTransform>();
    private readonly List<RectTransform> backGreenList = new List<RectTransform>();
    private readonly List<RectTransform> brickList = new List<RectTransform>();
    private readonly List<RectTransform> greenList = new List<RectTransform>();

    private int updateCount;

    void Awake()
    {
        backs = transform.Find("backs"); //背景
        backGreens = transform.Find("backGreens"); //背景
        greens = transform.Find("greens"); //草地
        bricks = transform.Find("bricks"); //砖块
        InitPrefab(backGroundPrefab, 0.6f, 0f, backs, backList); //初始化背景
        InitPrefab(backGreenPrefab, 0.3f, -0.15f, backGreens, backGreenList); //初始化背景草地
        InitPrefab(brickPrefab, 0.21f, -0.4f, bricks, brickList); //初始化砖块
        InitPrefab(greenPrefab, 0.1f, -0.25f, greens, greenList); //初始化草地
    }

    private void FixedUpdate()
    {

        print("Time.deltaTime " + Time.deltaTime);


        if (updateCount == 2)
        {
            Translation(backList, 1);
        }
        
        Translation(backGreenList, 0.5f);
        Translation(brickList, 1);
        Translation(greenList, 1);
        
        updateCount++;
        if (updateCount >= 3)
        {
            updateCount = 0;
        }
        
    }

    /// <summary>
    /// 用于平移物体
    /// </summary>
    private void Translation(List<RectTransform> rectTransforms, float moveX)
    {
        for (int i = 0; i < rectTransforms.Count; i++)
        {
            float x = rectTransforms[i].anchoredPosition.x;
            float y = rectTransforms[i].anchoredPosition.y;
            float width = rectTransforms[i].sizeDelta.x; //宽度
            x -= moveX; //像素减减
            if (x < -Screen.width / 2 - width / 2) //如果过界就滚回去
                x += rectTransforms.Count * width - rectTransforms.Count - 2;
            rectTransforms[i].anchoredPosition = new Vector2(x, y); //设置位置
        }
    }

    /// <summary>
    ///  初始化预制体
    /// </summary>
    /// <param name="prefab">要初始化的物体</param>
    /// <param name="yScale">延y轴缩放比例</param>
    /// <param name="yPosition">y的位置</param>
    /// <param name="parentTrans">预制体的父类</param>
    private void InitPrefab(GameObject prefab, float yScale, float yPosition, Transform parentTrans,
        List<RectTransform> rectTransforms)
    {
        float width = prefab.GetComponent<RectTransform>().sizeDelta.x;
        float height = prefab.GetComponent<RectTransform>().sizeDelta.y;

        float scale = Screen.height * yScale / height; //计算缩放比例
        width *= scale;
        prefab.GetComponent<RectTransform>().sizeDelta = new Vector2((int) width, Screen.height * yScale); //设置尺寸

        int x = (int) ((width - Screen.width) / 2);
        int y = (int) (yPosition * Screen.height);

        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        prefab.transform.parent = parentTrans;
        int xNum = (int) (Screen.width / width) + 1;

        rectTransforms.Add(prefab.GetComponent<RectTransform>());

        for (int i = 0; i < xNum; i++)
        {
            x = (int) (x + width - 1);

            GameObject _gameObject = Instantiate(prefab); //实例化一个背景
            _gameObject.transform.parent = parentTrans; //设为bricks的子物体

            _gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            rectTransforms.Add(_gameObject.GetComponent<RectTransform>());
        }
    }
}