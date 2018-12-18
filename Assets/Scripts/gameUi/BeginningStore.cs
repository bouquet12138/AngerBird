using utils;
using UnityEngine;

namespace gameUi
{
    public class BeginningStore : MonoBehaviour
    {
        private RectTransform mRectTransform; //位置组件

        private bool mIsAnim; //是否正在进行动画
        private float mStartX; //开始的X, 现在的X

        private void Awake()
        {
            mRectTransform = GetComponent<RectTransform>(); //大小距离的组件
        }

        private void Start()
        {
            InitPosition();
            Invoke("StartAnim", 4f);
        }

        /// <summary>
        /// 开启动画
        /// </summary>
        private void StartAnim()
        {
            mIsAnim = true; //动画开始
        }

        /// <summary>
        /// 初始化位置
        /// </summary>
        private void InitPosition()
        {
            float width = mRectTransform.sizeDelta.x;
            float height = mRectTransform.sizeDelta.y; //图片的高

            float scale = Screen.height * 1.2f / height;

            width *= scale;
            mRectTransform.localScale = new Vector3(scale, scale, 1); //设置缩放
            mStartX = (width - Screen.width) / 2; //开始的位置
            mRectTransform.anchoredPosition =
                new Vector2((width - Screen.width) / 2, mRectTransform.anchoredPosition.y);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (mIsAnim)
            {
                float nowX = mRectTransform.anchoredPosition.x - Time.deltaTime / 10 * mStartX * 2;

                if (nowX < -mStartX)
                {
                    Invoke("CompleteStore", 1.2f); //动画结束了
                    nowX = -mStartX;
                    mIsAnim = false; //动画结束
                }

                mRectTransform.anchoredPosition = new Vector2(nowX, mRectTransform.anchoredPosition.y);
            }
        }

        /// <summary>
        /// 故事完成
        /// </summary>
        public void CompleteStore()
        {
            SceneLoadUtil.LoadGameScene(); //加载游戏场景
        }
    }
}