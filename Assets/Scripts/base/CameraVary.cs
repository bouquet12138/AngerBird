using System;
using UnityEngine;

public class CameraVary : MonoBehaviour
{
    private float startX; //相机起始位置
    public bool BirdUse; //小鸟是否正在使用
    private float xGridNum; //水平网格数
    public static CameraVary sInstance; //实例
    private float cameraPosX, cameraPosY; //相机应该在的位置
    public Vector3 nowCenterPosition; //当前小鸟的位置
    public const int MIN_X = -10; //默认到达最小的X
    public const int MAX_X = 20; //能达到的最大X

    private void Awake()
    {
        sInstance = this; //初始化一下
        xGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数


        startX = xGridNum / 2 + MIN_X; //求出开始的位置

        transform.position =
            new Vector3(MAX_X - xGridNum / 2, transform.position.y, transform.position.z); //先设置一下镜头的位置
        nowCenterPosition = transform.position; //记录一下当前中心
        cameraPosX = startX; //起始位置

        Invoke("Home", 2f); //先停一会儿
    }


    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (Math.Abs(scrollWheel) > 0)
        {
            float originalSize = Camera.main.orthographicSize;
            float size = originalSize;
            size -= scrollWheel;
            size = Mathf.Clamp(size, 5, 10); //夹紧一下

            if (size != originalSize)
            {
                Camera.main.orthographicSize = size; //设置一下
                cameraPosX = (size - 5) / 5 * xGridNum / 4 + startX;
                cameraPosY = (size - 5) * 0.6f; //相机的Y
                nowCenterPosition.y = cameraPosY; //设置一下当前中心
            }
        }

        if (Input.GetMouseButton(0) && !BirdUse) //左键按下 并且小鸟没在使用
        {
            float mouseX = Input.GetAxis("Mouse X");
            nowCenterPosition.x -= mouseX * 10; //移动距离增大一点
            print("CameraVary 相机移动" + BirdUse);
        }

        if (!BirdUse)
        {
            RemoveCamera();
        }
    }

    public void SetBirdPosition(Vector3 nowBirdPosition, float radius)
    {
        float originalSize = Camera.main.orthographicSize;

        print("BirdUse ");

        nowCenterPosition.x = nowBirdPosition.x;

        if (nowBirdPosition.y + radius + 0.5f - originalSize - cameraPosY > 0)
        {
            nowCenterPosition.y = nowBirdPosition.y - originalSize + radius + 0.5f; //当前相机中心
        }
        else
        {
            nowCenterPosition.y = cameraPosY; //设回标准位置
        }
    }

    /// <summary>
    /// 夹紧当前的中心
    /// </summary>
    private void ClampNowCenterPosition(float cameraSize)
    {
        float posX = nowCenterPosition.x; //当前X

        //MAX_X - xGridNum * cameraSize / 5/2的缩写
        float cameraMaxMove =
            Math.Max(cameraPosX, MAX_X - xGridNum * cameraSize / 10);


        posX = Mathf.Clamp(posX, cameraPosX, cameraMaxMove); //夹紧一下

        print("cameraMaxMove " + cameraMaxMove + "  cameraPosX " + cameraPosX + "posX " + posX);

        nowCenterPosition.x = posX;
        nowCenterPosition.y = Mathf.Clamp(nowCenterPosition.y, cameraPosY, (20 - cameraSize * 2) / 2 + cameraPosY);

        if (nowCenterPosition.x > cameraPosX - 0.01f && nowCenterPosition.x < cameraPosX + 0.01f)
        {
            nowCenterPosition.x = cameraPosX;
            nowCenterPosition.y = cameraPosY;
        }
    }


    /// <summary>
    /// 移动相机的方法
    /// </summary>
    public void RemoveCamera()
    {
        float cameraSize = Camera.main.orthographicSize;
        ClampNowCenterPosition(cameraSize); //夹紧一下

        if (cameraSize < 8f)
        {
            if (nowCenterPosition.x == transform.position.x && transform.position.y == nowCenterPosition.y)
                return;

            float cameraMaxMove =
                Math.Max(cameraPosX, MAX_X - xGridNum * cameraSize / 10); //相机可以移动的最大距离


            if (BirdUse && nowCenterPosition.x < cameraMaxMove - 2 && nowCenterPosition.x != cameraPosX)
            {
                transform.position = nowCenterPosition; //如果小鸟正在使用设置当前位置
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,
                    nowCenterPosition,
                    1 * Time.deltaTime);
            }
        }
        else
        {
            /*print("nowCenterPosition " + nowCenterPosition);
            print("cameraPosX " + cameraPosX);*/
            transform.position = nowCenterPosition;
        }
    }

    /// <summary>
    /// 相机回到最初的位置
    /// </summary>
    public void Home()
    {
        nowCenterPosition.x = cameraPosX;
        nowCenterPosition.y = cameraPosY;
    }
}