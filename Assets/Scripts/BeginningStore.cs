using utils;
using UnityEngine;

public class BeginningStore : MonoBehaviour
{
    private RectTransform rectTransform; //位置组件
    private AudioSource audioSource; //声音源组件

    private float width; //故事的宽度

    private bool anim; //正在进行动画
    private float startX; //开始的X, 现在的X

    private void Awake()
    {
        int nowLevel = PlayerPrefUtil.GetNowLevel();

        if (nowLevel != 0)
        {
            gameObject.SetActive(false); //禁用
        }
        else
        {
            Camera.main.GetComponent<CameraVary>().enabled = false; //禁用掉
            rectTransform = GetComponent<RectTransform>(); //大小距离的组件
            InitPosition();
            Invoke("StartAnim", 4f);
        }
    }

    private void Start()
    {
        GameCursor.sInstance.isUi = true; //是UI
        audioSource = GetComponent<AudioSource>(); //声音源

        if (AudioUtil.sInstance.hasVoice)
            audioSource.Play(); //播放音乐  
    }

    /// <summary>
    /// 开启动画
    /// </summary>
    private void StartAnim()
    {
        anim = true;
    }

    /// <summary>
    /// 初始化位置
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InitPosition()
    {
        float width = rectTransform.sizeDelta.x;
        float height = rectTransform.sizeDelta.y; //图片的高

        float scale = Screen.height * 1.2f / height;

        width *= scale;
        rectTransform.localScale = new Vector3(scale, scale, 1); //设置缩放
        startX = (width - Screen.width) / 2; //开始的位置
        rectTransform.anchoredPosition = new Vector2((width - Screen.width) / 2, rectTransform.anchoredPosition.y);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim)
        {
            float nowX = rectTransform.anchoredPosition.x - Time.deltaTime / 10 * startX * 2;

            if (nowX < -startX)
            {
                Invoke("CompleteStore", 1.2f); //动画结束了
                nowX = -startX;
                anim = false; //动画结束
            }

            rectTransform.anchoredPosition = new Vector2(nowX, rectTransform.anchoredPosition.y);
        }
    }

    /// <summary>
    /// 故事完成
    /// </summary>
    public void CompleteStore()
    {
        GameCursor.sInstance.isUi = false; //不是UI了
        Camera.main.GetComponent<CameraVary>().enabled = true; //启用
        Destroy(gameObject); //销毁自己
    }
}