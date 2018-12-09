using UnityEngine;

public class Balloon : MonoBehaviour
{
    public AudioClip balloonPopClip; //气球破掉的声音
    private HingeJoint2D hingeJoint2D; //铰链

    private void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>(); //铰链
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 15) //飞高之后
        {
            DestroyBallon(false); //销毁自己
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 8) //速度大于8
        {
            DestroyBallon(true); //销毁自己
        }
    }

    /// <summary>
    /// 销毁自己
    /// </summary>
    /// <param name="hasAudio">是否有声音</param>
    private void DestroyBallon(bool hasAudio)
    {
        if (transform.parent != null)
        {
            BalloonGroup balloonGroup = transform.parent.GetComponent<BalloonGroup>();
            if (balloonGroup != null)
            {
                balloonGroup.balloons.Remove(this); //移除自己
                balloonGroup.LoseBalance(); //失去平衡了
            }
        }

        if (hasAudio)
        {
            AudioUtil.sInstance.AudioPlay(balloonPopClip); //播放爆炸声音
        }

        Destroy(gameObject); //销毁自己
    }

    /// <summary>
    /// 同组的成员销毁
    /// </summary>
    public void GroupMemberDestroy()
    {
        JointAngleLimits2D jointAngleLimits2D = new JointAngleLimits2D {min = -180, max = 180};
        hingeJoint2D.limits = jointAngleLimits2D; //重新设置一下旋转角度
    }
}