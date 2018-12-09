using System;
using UnityEngine;

namespace utils
{
    public class PlayerPrefUtil
    {
        /// <summary>
        /// 工具类构造器私有
        /// </summary>
        private PlayerPrefUtil()
        {
        }

        /// <summary>
        /// 设置是否有声音
        /// </summary>
        public static void setVoice(bool voice)
        {
            if (voice)
                PlayerPrefs.SetInt("voice", 1);
            else
                PlayerPrefs.SetInt("voice", 0);
        }


        /// <summary>
        /// 判断是否有声音
        /// </summary>
        public static bool isVoice()
        {
            int voice = PlayerPrefs.GetInt("voice", 1);

            return voice != 0;
        }


        /// <summary>
        /// 得到总星星数
        /// </summary>
        /// <returns>星星数</returns>
        public static int GetTotalStars()
        {
            return PlayerPrefs.GetInt("totalStars", 0);
        }

        /// <summary>
        /// 添加星星
        /// </summary>
        /// <param name="num">添加的星星数</param>
        public static void addTotalStar(int num)
        {
            int totalStar = GetTotalStars() + num;
            PlayerPrefs.SetInt("totalStars", totalStar);
        }

        /// <summary>
        /// 得到当前地图总星星数
        /// </summary>
        public static int GetNowMapTotalStars()
        {
            return PlayerPrefs.GetInt("totalStars" + GetNowMap(), 0);
        }

        /// <summary>
        /// 得到任意索引地图星星数
        /// </summary>
        public static int GetMapTotalStars(int index)
        {
            return PlayerPrefs.GetInt("totalStars" + index, 0);
        }

        /// <summary>
        /// 想当前地图添加星星
        /// </summary>
        public static void addNowMapTotalStar(int num)
        {
            int totalStar = GetNowMapTotalStars() + num;
            PlayerPrefs.SetInt("totalStars" + GetNowMap(), totalStar);
        }

        /// <summary>
        /// 得到当前地图
        /// </summary>
        public static int GetNowMap()
        {
            return PlayerPrefs.GetInt("nowMap");
        }

        /// <summary>
        /// 设置当前地图
        /// </summary>
        public static void SetNowMap(int index)
        {
            PlayerPrefs.SetInt("nowMap", index);
        }

        /// <summary>
        /// 得到当前关卡
        /// </summary>
        public static int GetNowLevel()
        {
            return PlayerPrefs.GetInt("nowLevel");
        }

        /// <summary>
        /// 得到当前关卡
        /// </summary>
        public static void SetNowLevel(int index)
        {
            PlayerPrefs.SetInt("nowLevel", index);
        }


        /// <summary>
        /// 得到当前地图 当前关卡的星星数
        /// </summary>
        /// <returns></returns>
        public static int GetNowMapLevelStarNum()
        {
            return GetMapLevelStarNum(GetNowMap(), GetNowLevel());
        }

        /// <summary>
        /// 得到当前地图 某关卡的星星数
        /// </summary>
        /// <returns></returns>
        public static int GetLevelStarNum(int level)
        {
            return GetMapLevelStarNum(GetNowMap(), level);
        }


        /// <summary>
        /// 得到当前游戏关卡的名字
        /// </summary>
        /// <returns></returns>
        public static string GetNowMapLevelName()
        {
            return "map" + GetNowMap() + "level" + GetNowLevel();
        }

        /// <summary>
        /// 得到某地图 当前某卡的星星数
        /// </summary>
        /// <returns></returns>
        public static int GetMapLevelStarNum(int map, int level)
        {
            String nowLevelString = "map" + map + "level" + level;
            return PlayerPrefs.GetInt(nowLevelString, 0);
        }

        /// <summary>
        /// 设置当前地图 当前关卡的星星数
        /// </summary>
        /// <returns></returns>
        public static void SetNowMapLevelStarNum(int starNum)
        {
            int nowMap = GetNowMap();
            int nowLevel = GetNowLevel();

            String nowLevelString = "map" + nowMap + "level" + nowLevel;
            PlayerPrefs.SetInt(nowLevelString, starNum);
        }

        /// <summary>
        /// 设置当前地图 当前关卡的最高分数
        /// </summary>
        /// <param name="score"></param>
        public static void SetNowMapLevelHighestScore(int score)
        {
            int nowMap = GetNowMap();
            int nowLevel = GetNowLevel();

            String nowLevelString = "map" + nowMap + "level" + nowLevel + "score";
            PlayerPrefs.SetInt(nowLevelString, score);
        }

        /// <summary>
        /// 得到当前地图 当前关卡的分数
        /// </summary>
        /// <param name="score"></param>
        public static int GetNowMapLevelScore()
        {
            int nowMap = GetNowMap();
            int nowLevel = GetNowLevel();

            String nowLevelString = "map" + nowMap + "level" + nowLevel + "score";
            return PlayerPrefs.GetInt(nowLevelString, 0);
        }
    }
}