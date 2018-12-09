using UnityEngine;
using UnityEngine.EventSystems;

public class MyUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameCursor.sInstance.isUi = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameCursor.sInstance.isUi = false; //鼠标移出去了
    }
}