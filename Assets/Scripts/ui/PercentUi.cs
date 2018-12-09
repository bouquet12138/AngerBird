using UnityEngine;

public class PercentUi : MonoBehaviour
{
    public bool relativeFather; //相对于父亲

    public float yScale; //竖直位置占的百分比
    public bool useYScale = true; //y轴百分比

    public float xScale; //水平位置占的百分比
    public bool useXScale; //x轴百分比

    public float yPosition; //y轴的位置
    public bool useYPosition; //是否y轴有位置

    public float xPosition; //y轴的位置
    public bool useXPosition; //是否x轴位置

    private float parentWidth, parentHeight; //父亲的宽 高

    private void Start()
    {
        InitParentSize();
        SetScale();
        SetPosition();
    }

    /// <summary>
    /// 初始化父亲的尺寸
    /// </summary>
    private void InitParentSize()
    {
        if (relativeFather)
        {
            if (transform.parent != null)
            {
                parentWidth = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
                parentHeight = transform.parent.GetComponent<RectTransform>().sizeDelta.y;
                print(parentWidth + " parentWidth");
                print(parentHeight + " parentHeight");
            }
            else
            {
                parentWidth = Screen.width;
                parentHeight = Screen.height;
            }
        }
        else
        {
            parentWidth = Screen.width;
            parentHeight = Screen.height;
        }
    }


    /// <summary>
    /// 设置缩放
    /// </summary>
    private void SetScale()
    {
        float width = GetComponent<RectTransform>().sizeDelta.x;
        float height = GetComponent<RectTransform>().sizeDelta.y;


        if (useYScale && !useXScale)
        {
            float scale = parentHeight * yScale / height; //计算缩放比例

            width *= scale;
            height = yScale * parentHeight;

            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //设置尺寸
        }
        else if (!useYScale && useXScale)
        {
            float scale = parentWidth * xScale / width; //计算缩放比例

            height *= scale;
            width = xScale * parentWidth;

            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height); //设置尺寸
        }
        else if (useYScale && useXScale)
        {
            GetComponent<RectTransform>().sizeDelta =
                new Vector2(parentWidth * xScale, parentHeight * yScale); //设置尺寸
        }
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    private void SetPosition()
    {
        int _xPosition, _yPosition;
        if (useXPosition)
            _xPosition = (int) (xPosition * parentWidth);
        else
            _xPosition = (int) GetComponent<RectTransform>().anchoredPosition.x;

        if (useYPosition)
            _yPosition = (int) (yPosition * parentHeight);
        else
            _yPosition = (int) GetComponent<RectTransform>().anchoredPosition.y;
        if (useXPosition || useYPosition)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(_xPosition, _yPosition);
    }
}