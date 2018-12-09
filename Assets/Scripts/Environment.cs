using System;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private float xGridNum; //水平网格数

    private Transform backGrounds; //背景们
    private Transform backLawns; //背景草地
    private Transform lawns; //草地们
    private Transform bricks; //砖块们
    private Transform prospects; //前景草

    public GameObject backGroundPrefab;
    public GameObject backLawnPrefab; //背景草地
    public GameObject lawnPrefab;
    public GameObject brickPrefab;
    public GameObject prospectPrefab; //前景草

    public static int DEFAULT_X_GRID = 20; //默认是20个网格
    public static int MAX_GRID_NUM = 30; //最大网格数

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        xGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数
        InitChildren(); //初始化孩子们

        InitPrefabs(backGroundPrefab, backGrounds);
        InitPrefabs(backLawnPrefab, backLawns);

        InitPrefabs(brickPrefab, bricks);
        InitPrefabs(lawnPrefab, lawns);

        InitPrefabs(prospectPrefab, prospects);
    }

    /// <summary>
    /// 初始化孩子
    /// </summary>
    private void InitChildren()
    {
        backGrounds = transform.GetChild(0);
        backLawns = transform.GetChild(1);
        lawns = transform.GetChild(2);
        bricks = transform.GetChild(3);
        prospects = transform.GetChild(4);
    }

    /// <summary>
    /// 初始化预制体
    /// </summary>
    private void InitPrefabs(GameObject prefab, Transform _parent)
    {
        if (prefab == null)
            return; //为空直接返回

        float startX = 0; //开始的X位置
        if (_parent.GetComponent<CameraMonitor>() != null)
        {
            startX = _parent.GetComponent<CameraMonitor>().movePercent * (-xGridNum / 4);
        }

        float maxXGrid = Math.Max(MAX_GRID_NUM, xGridNum * 2f);
        float width = prefab.GetComponent<Renderer>().bounds.size.x;
        float x = (width - maxXGrid + xGridNum / 2) / 2 + startX;
        float y = prefab.transform.position.y; //y的位置
        float z = prefab.transform.position.z;

        int backGroundNum = (int) (maxXGrid / width) + 1;

        for (int i = 0; i < backGroundNum; i++)
        {
            GameObject instantPrefab = Instantiate(prefab); //实例化一个背景
            instantPrefab.transform.position = new Vector3(x, y, z);
            x += width - 0.02f;
            instantPrefab.transform.parent = _parent;
        }
    }
}