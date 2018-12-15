using UnityEngine;

public class CameraMonitor : MonoBehaviour
{
    public float movePercent; //移动百分比

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