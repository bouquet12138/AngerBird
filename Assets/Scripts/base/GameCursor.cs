using UnityEngine;

public class GameCursor : MonoBehaviour
{
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
        Cursor.SetCursor(normalCursor, Vector2.zero, cm);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isUi)
                Cursor.SetCursor(uiDownCursor, Vector2.zero, cm);
            else
                Cursor.SetCursor(downCursor, Vector2.zero, cm);
        }
        else
        {
            if (isUi)
                Cursor.SetCursor(uiNormalCursor, Vector2.zero, cm);
            else
                Cursor.SetCursor(normalCursor, Vector2.zero, cm);
        }
    }
}