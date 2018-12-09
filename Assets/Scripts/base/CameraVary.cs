using System;
using UnityEngine;

public class CameraVary : MonoBehaviour
{
    public bool birdUse;
    private float xGridNum; //水平网格数
    public static CameraVary sInstance; //实例
    private float cameraPosX, cameraPosY; //相机应该在的位置
    public Vector3 nowCenterPosition; //当前小鸟的位置


    private void Awake()
    {
        sInstance = this; //初始化一下
        xGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数
        transform.position =
            new Vector3(Environment.MAX_GRID_NUM - xGridNum, transform.position.y, transform.position.z); //先设置一下镜头的位置
        nowCenterPosition = transform.position; //记录一下当前中心

        Invoke("Home", 2f); //先停一会儿
    }


    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            float originalSize = Camera.main.orthographicSize;
            float size = originalSize;
            size -= scrollWheel;
            size = Mathf.Clamp(size, 5, 10); //夹紧一下

            if (size != originalSize)
            {
                Camera.main.orthographicSize = size; //设置一下
                cameraPosX = (size - 5) / 5 * xGridNum / 4;
                cameraPosY = (size - 5) * 0.6f; //相机的Y
            }
        }

        if (Input.GetMouseButton(0) && !birdUse) //左键按下 并且小鸟没在使用
        {
            float mouseX = Input.GetAxis("Mouse X");
            nowCenterPosition.x -= mouseX * 10; //移动距离增大一点
        }

        RemoveCamera();
    }

    public void SetBirdPosition(Vector3 _nowBirdPosition)
    {
        nowCenterPosition.x = _nowBirdPosition.x;
    }

    /// <summary>
    /// 夹紧当前的中心
    /// </summary>
    private void ClampNowCenterPosition(float cameraSize)
    {
        float posX = nowCenterPosition.x; //当前X

        //print("posX " + posX);
        float cameraMaxMove =
            Math.Max(0, Environment.MAX_GRID_NUM - xGridNum * cameraSize / 5 + cameraPosX); //相机可以移动的最大距离 

        posX = Mathf.Clamp(posX, cameraPosX, cameraMaxMove + cameraPosX); //夹紧一下

        //print("cameraMaxMove " + cameraMaxMove + "  cameraPosX " + cameraPosX + "posX " + posX);

        nowCenterPosition.x = posX;
        nowCenterPosition.y = cameraPosY;

        if (nowCenterPosition.x > cameraPosX - 0.01f && nowCenterPosition.x < cameraPosX + 0.01f)
        {
            nowCenterPosition.x = cameraPosX;
            nowCenterPosition.y = cameraPosY;
        }
    }


    /// <summary>
    /// 移动相机的方法
    /// </summary>
    private void RemoveCamera()
    {
        float cameraSize = Camera.main.orthographicSize;
        ClampNowCenterPosition(cameraSize); //夹紧一下

        if (cameraSize < 8f)
        {
            if (nowCenterPosition.x == transform.position.x && transform.position.y == nowCenterPosition.y)
                return;

            transform.position = Vector3.Lerp(transform.position,
                nowCenterPosition,
                Time.deltaTime);
        }
        else
        {
            transform.position = nowCenterPosition;
        }
    }

    /// <summary>
    /// 相机回到最初的位置
    /// </summary>
    public void Home()
    {
        birdUse = false;
        nowCenterPosition.x = cameraPosX;
    }
    
}