using UnityEngine;
using UnityEngine.UI;

public class GridLayoutManager : MonoBehaviour
{
    public Sprite backSprite; //背景图片
    public LevelUi levelUi; //关卡UI
    private int cellSize, space;
    private int mapIndex; //得到自己对应的地图索引

    private void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == gameObject.name)
                mapIndex = i; //设置一下索引
            print("panel 所在索引 " + mapIndex);
        }

        InitSize(); //初始化尺寸
        InitLevel(); //初始化关卡
    }


    /// <summary>
    /// 初始化尺寸
    /// </summary>
    private void InitSize()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

        cellSize = (int) (Screen.width * 0.06f); //尺寸
        space = (int) (Screen.width * 0.025f); //分割宽度

        levelUi.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize); //设置一下尺寸

        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize); //尺寸
        gridLayoutGroup.spacing = new Vector2(space, space); //设置分割距离


        GetComponent<RectTransform>().sizeDelta =
            new Vector2((int) (Screen.width * 0.7f), Screen.height * 0.75f); //设置网格布局的宽高
    }

    /// <summary>
    /// 初始化关卡
    /// </summary>
    public void InitLevel()
    {
        levelUi.levelBg = backSprite; //设置一下背景图
        int levelNum = GameLevelUtil.sLevelNumArray[mapIndex]; //得到关卡数
        for (int i = 0; i < levelNum; i++)
        {
            LevelUi _levelUi = Instantiate(levelUi, transform);
            _levelUi.mapIndex = mapIndex; //设置一下地图索引
            _levelUi.SetNowIndex(i); //设置当前索引
        }
    }
}