using UnityEngine;

public class SetBt : MonoBehaviour
{
    private Animator animator;
    private bool anim;
    public SetPanel setPanel; //设置面板

    /// <summary>
    /// 初始化时
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>(); //得到动画状态机
    }

    /// <summary>
    /// 打开或者关闭设置面板
    /// </summary>
    public void OpenOrCloseSet()
    {
        if (!anim) // 如果当前没有动画
        {
            anim = true;
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
            setPanel.OpenOrClosePanel(!isOpen);
        }
    }


    /// <summary>
    /// 动画结束
    /// </summary>
    public void AnimEnd()
    {
        anim = false;
    }
}