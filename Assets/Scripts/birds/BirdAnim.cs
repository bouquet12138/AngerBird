using UnityEngine;

public class BirdAnim : MonoBehaviour
{
    private Animator birdAnimator;
    public AudioClip birdCall;

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    void Awake()
    {
        birdAnimator = GetComponent<Animator>(); //得到动画控制器
    }

    /// <summary>
    /// 改变当前的运动方式
    /// </summary>
    public void ChangeSport()
    {
        int value = Random.Range(0, 10);
        birdAnimator.SetInteger("sportState", value);
    }

    public void ChangeExpression()
    {
        int value = Random.Range(0, 3);
        if (value == 1) //1是张开嘴的情况
        {
            int v = Random.Range(0, 10);

            if (v % 3 == 0)
                AudioUtil.sInstance.AudioPlay(birdCall, transform.position); //播放小鸟叫的声音
        }

        birdAnimator.SetInteger("expressionState", value);
    }

    /// <summary>
    /// 改变为飞行动画
    /// </summary>
    public void Fly()
    {
        birdAnimator.SetBool("fly", true); //改为飞的状态
        birdAnimator.SetBool("showSkill", false); //不再展示技能了
        birdAnimator.SetBool("hurt", false); //不是受伤状态
    }

    /// <summary>
    /// 设为受伤动画
    /// </summary>
    public virtual void hurt()
    {
        birdAnimator.SetBool("hurt", true); //受伤了
        birdAnimator.SetBool("showSkill", false); //不再展示技能了
        birdAnimator.SetBool("fly", false); //不再飞了
    }

    /// <summary>
    /// 展示技能的动画
    /// </summary>
    public virtual void ShowSkill()
    {
        birdAnimator.SetBool("showSkill", true); //展示技能
        birdAnimator.SetBool("fly", false); //不再飞了
        birdAnimator.SetBool("hurt", false); //未受伤
    }


    /// <summary>
    /// 准备就绪
    /// </summary>
    public void Ready()
    {
        birdAnimator.SetLayerWeight(birdAnimator.GetLayerIndex("SportLayer"), 0);
        transform.position = Vector3.zero; //位置归零
        transform.localEulerAngles = new Vector3(0, 0, 0); //旋转归0
    }
}