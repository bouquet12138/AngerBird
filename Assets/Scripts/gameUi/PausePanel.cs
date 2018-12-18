using utils;
using UnityEngine;

namespace gameUi
{
    /// <summary>
    /// 暂停面板 
    /// </summary>
    public class PausePanel : MonoBehaviour
    {
        private Animator animator;
        public GameObject pauseButton; //暂停按钮
        private bool allDisplay;

        public AudioClip resumeClip; //重新开始的声音

        /// <summary>
        /// 初始化时
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>(); //得到动画状态机
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            pauseButton.SetActive(false); //暂停按钮不可用
            animator.SetBool("pause", true);
        }

        /// <summary>
        /// 重新尝试
        /// </summary>
        public void Retry()
        {
            if (allDisplay)
            {
                allDisplay = false;
                Time.timeScale = 1; // timeScale 恢复正常
                SceneLoadUtil.ReLoadGameScene(); //加载游戏场景
            }
        }

        /// <summary>
        /// 主页
        /// </summary>
        public void Home()
        {
            if (allDisplay)
            {
                allDisplay = false;
                Time.timeScale = 1; // timeScale 恢复正常
                SceneLoadUtil.LoadLevelScene(); //加载关卡选择场景
            }
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Resume()
        {
            if (allDisplay)
            {
                Time.timeScale = 1;
                animator.SetBool("pause", false);
                allDisplay = false;
                AudioUtil.sInstance.AudioPlay(resumeClip); //播放确定的声音
            }
        }

        /// <summary>
        /// 暂停动画播放完
        /// </summary>
        public void PauseAnimEnd()
        {
            allDisplay = true; //全部展示了
            Time.timeScale = 0;
        }

        /// <summary>
        ///继续动画播放完 
        /// </summary>
        public void ResumeAnimEnd()
        {
            pauseButton.SetActive(true); //又可以使用了  
        }
    }
}