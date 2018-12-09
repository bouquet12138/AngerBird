using UnityEngine;
using Random = UnityEngine.Random;

public class BirdFly : MonoBehaviour
{
    private float startX, endX, nowX;
    private float a;

    private RectTransform rectTransform;

    private void Awake()
    {
        startX = Random.Range(-0.8f, 0.6f); //起始位置

        endX = Random.Range(startX + 0.4f, startX + 1f); //结尾的位置
        nowX = startX; //当前的X

        float vertexY = Random.Range(-0.1f, 1.2f); //顶点y坐标
        float vertexX = (startX + endX) / 2; //顶点x坐标

        a = -vertexY / ((startX - vertexX) * (startX - vertexX));
        rectTransform = GetComponent<RectTransform>(); //得到RectTransform
    }

    // Update is called once per frame
    void Update()
    {
        nowX += 0.004f; //startX 加加
        float y = a * (nowX - startX) * (nowX - endX);
        y -= 0.5f;

        rectTransform.anchoredPosition = new Vector2(nowX * Screen.width, y * Screen.height); //延抛物线运动

        if (nowX >= endX)
            Destroy(gameObject); //销毁自己
    }
}