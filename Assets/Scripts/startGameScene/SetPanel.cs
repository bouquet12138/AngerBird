using UnityEngine;

public class SetPanel : MonoBehaviour
{
    private Animator animator;

    /// <summary>
    /// 初始化时
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>(); //得到动画状态机
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    public void OpenOrClosePanel(bool open)
    {
        animator.SetBool("open", open);
    }
}