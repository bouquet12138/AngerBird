using UnityEngine;

// 文件名称：YPercentUi.cs
// 功能描述：Y轴适配的Ui
// 编写作者：小韩
// 编写日期：2017.7.16 

namespace ui
{
    public class YPercentUi : MonoBehaviour
    {
        public float Scale; //百分比
        public bool UseScale = true; //是否应用百分比

        public float YPosition; //y轴的位置
        public bool UseYPosition; //是否y轴有位置

        public float XPosition; //y轴的位置
        public bool UseXPosition; //是否x轴位置

        private void Start()
        {
            SetScale();
            SetPosition();
        }


        /// <summary>
        /// 设置缩放
        /// </summary>
        private void SetScale()
        {
            float width = GetComponent<RectTransform>().sizeDelta.x;
            float height = GetComponent<RectTransform>().sizeDelta.y;


            if (UseScale)
            {
                float scale = Screen.height * Scale / height; //计算缩放比例

                width *= scale;
                height = Scale * Screen.height;

                GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //设置尺寸
            }
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        private void SetPosition()
        {
            int xPosition, yPosition;
            if (UseXPosition)
                xPosition = (int) (XPosition * Screen.height);
            else
                xPosition = (int) GetComponent<RectTransform>().anchoredPosition.x;

            if (UseYPosition)
                yPosition = (int) (YPosition * Screen.height);
            else
                yPosition = (int) GetComponent<RectTransform>().anchoredPosition.y;
            if (UseXPosition || UseYPosition)
                GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
        }
    }
}