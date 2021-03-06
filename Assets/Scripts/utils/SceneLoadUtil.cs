using UnityEngine.SceneManagement;

namespace utils
{
    public class SceneLoadUtil
    {
        /// <summary>
        /// 用于加载场景的工具类
        /// </summary>
        private SceneLoadUtil()
        {
        }

        /// <summary>
        /// 加载 加载页面
        /// </summary>
        public static void LoadLoadingScene()
        {
            SceneManager.LoadScene(0); //加载0号场景
        }

        /// <summary>
        /// 加载 开始页面
        /// </summary>
        public static void LoadStartScene()
        {
            SceneManager.LoadScene(1); //加载0号场景
        }

        /// <summary>
        /// 加载 地图选择页面
        /// </summary>
        public static void LoadLevelScene()
        {
            SceneManager.LoadScene(2); //加载2号场景
        }

        /// <summary>
        /// 加载 游戏页面
        /// </summary>
        public static void LoadGameScene()
        {
            SceneManager.LoadScene(3); //加载3号场景
        }

        /// <summary>
        /// 异步加载游戏场景
        /// </summary>
        public static void AsyncLoadGameScene()
        {
            SceneManager.LoadSceneAsync(4); //加载游戏场景
        }

        /// <summary>
        /// 重新加载游戏场景
        /// </summary>
        public static void ReLoadGameScene()
        {
            SceneManager.LoadScene(4); //加载游戏场景
        }

        /// <summary>
        /// 加载故事场景
        /// </summary>
        public static void LoadStoreScene()
        {
            SceneManager.LoadScene(5); 
        }
    }
}