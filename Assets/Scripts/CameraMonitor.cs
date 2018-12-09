using UnityEngine;

public class CameraMonitor : MonoBehaviour
{
    public float movePercent; //移动百分比


    /*private void Awake()
    {
        /*float xGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数

        print("startX " + startX);#1#

      //  transform.position = new Vector2(startX, transform.position.y);
    }*/

    // Update is called once per frame
    void Update()
    {
        float currentPosition = Camera.main.transform.position.x * movePercent;

        if (transform.position.x != currentPosition)
        {
            transform.position = new Vector2(currentPosition, transform.position.y);
        }
    }
}