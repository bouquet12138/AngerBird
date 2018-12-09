using UnityEngine;

public class LevelBackGround : MonoBehaviour
{
    public GameObject backGroundPrefab;
    public GameObject brickPrefab;

    void Awake()
    {
        InitBackGround();
        InitBrick();
    }

    /// <summary>
    /// 初始化背景
    /// </summary>
    private void InitBackGround()
    {
        float width = backGroundPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float height = backGroundPrefab.GetComponent<RectTransform>().sizeDelta.y;

        float scale = Screen.height * 0.8f / height; //计算缩放比例
        width *= scale;
        backGroundPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(width, Screen.height * 0.8f);

        float x = (width - Screen.width) / 2;
        float y = 0.1f * Screen.height;

        backGroundPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);


        int xNum = (int) (Screen.width / width); //背景图的数量

        for (int i = 0; i < xNum; i++)
        {
            x += width;
            GameObject back = Instantiate(backGroundPrefab); //实例化一个背景
            back.transform.parent = transform; //设为子物体
            back.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }

    /// <summary>
    /// 初始化砖块
    /// </summary>
    private void InitBrick()
    {
        float width = brickPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float height = brickPrefab.GetComponent<RectTransform>().sizeDelta.y;

        float scale = Screen.height * 0.2f / height; //计算缩放比例
        width *= scale;
        brickPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(width, Screen.height * 0.2f);

        float x = (width - Screen.width) / 2;
        float y = -0.4f * Screen.height;

        brickPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        int xNum = (int) (Screen.width / width);

        for (int i = 0; i < xNum; i++)
        {
            x += width - 1;

            GameObject brick = Instantiate(brickPrefab); //实例化一个背景
            brick.transform.parent = transform; //设为子物体

            brick.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }
}