using UnityEngine;

public class Rubber : MonoBehaviour
{
    public AudioClip ballBounceAudio; //气球弹跳的声音

    /// <summary>
    /// 气球弹跳的声音
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 8)
            AudioUtil.sInstance.AudioPlay(ballBounceAudio, transform.position); //播放碰撞的音效
    }
}