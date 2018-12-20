using UnityEngine;

public class GameCursor : MonoBehaviour
{
    private const int NORMAL = 0;
    private const int DOWN = 1;
    private const int UI_NORMAL = 2;
    private const int UI_DOWN = 3;

    private int currentCursor; //当前鼠标

    public Texture2D uiNormalCursor; //UI图标的正常光标
    public Texture2D uiDownCursor; //UI图标的正常光标

    public bool isUi; //是否是Ui

    public Texture2D normalCursor; //正常光标
    public Texture2D downCursor; //按下时的光标

    public CursorMode cm = CursorMode.Auto; //渲染形式，auto为平台自适应显示

    public static GameCursor sInstance; //实例

    // Use this for initialization
    void Awake()
    {
        sInstance = this;
        if (isUi)
        {
            Cursor.SetCursor(uiNormalCursor, Vector2.zero, cm); //UI 图标
            currentCursor = NORMAL;
        }
        else
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, cm); //正常图标
            currentCursor = UI_NORMAL;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isUi)
            {
                if (currentCursor != UI_NORMAL)
                {
                    currentCursor = UI_NORMAL;
                    Cursor.SetCursor(uiDownCursor, Vector2.zero, cm);
                }
            }
            else
            {
                if (currentCursor != DOWN)
                {
                    currentCursor = DOWN;
                    Cursor.SetCursor(downCursor, Vector2.zero, cm);
                }
            }
        }
        else
        {
            if (isUi)
            {
                if (currentCursor != UI_NORMAL)
                {
                    currentCursor = UI_NORMAL;
                    Cursor.SetCursor(uiNormalCursor, Vector2.zero, cm);
                }
            }
            else
            {
                if (currentCursor != NORMAL)
                {
                    currentCursor = NORMAL; //当前状态为正常
                    Cursor.SetCursor(normalCursor, Vector2.zero, cm);
                }
            }
        }
    }
}