﻿using effect;
using UnityEngine;

namespace varia
{
    public class TNT : MonoBehaviour
    {
        public Bomb BombObject; //爆炸效果
        public AudioClip BombClip; //爆炸的声音
        public int TargetRadius = 3;
        public int Power = 8;
        public int MaxSpeed = 30;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.relativeVelocity.magnitude > 2)
            {
                BombObject.TargetRadius = TargetRadius; //目标尺寸
                BombObject.Power = Power; //炸弹力量
                BombObject.MaxSpeed = MaxSpeed; //最大速度
                BombObject.TotalTime = 1; //销毁时间

                AudioUtil.sInstance.AudioPlay(BombClip, transform.position); //播放一个爆炸声音
                Instantiate(BombObject, transform.position, Quaternion.identity); //生成一个爆炸效果
                Destroy(gameObject); //销毁自己
            }
        }
    }
}