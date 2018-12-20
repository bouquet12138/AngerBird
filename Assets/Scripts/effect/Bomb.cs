using System;
using UnityEngine;

namespace effect
{
    /// <summary>
    /// 炸弹对象
    /// </summary>
    public class Bomb : MonoBehaviour
    {
        //public bool boom; //爆炸
        public float TargetRadius = 3f; //目标半径
        public float TotalTime = 0.5f; //爆炸需要的时间
        public int Power = 5; //力量等级
        public int MaxSpeed = 20; //最大速度
        public int MinSpeed = 0; //最小速度

        private CircleCollider2D mCollider; //碰撞体
        // private float nowTime; //当前运行时间


        private void Awake()
        {
            mCollider = GetComponent<CircleCollider2D>(); //得到碰撞体
            mCollider.radius = TargetRadius; //设为目标半径
            Invoke("DestroyThis", TotalTime); //过几秒销毁自己
        }

        /// <summary>
        /// 销毁这个爆炸效果
        /// </summary>
        public void DestroyThis()
        {
            Destroy(gameObject); //销毁自己
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            /*if (!boom)
            return;*/

            print("进入物体的名字 " + other.gameObject.name);

            Vector2 relativePosition = other.transform.position - transform.position; //相对位置

            int direX = relativePosition.x > 0 ? 1 : -1;
            int direY = relativePosition.y > 0 ? 1 : -1;


            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                float velocityX;
                if (relativePosition.x == 0)
                    velocityX = MaxSpeed;
                else
                    velocityX = Math.Abs(Power / relativePosition.x);

                float velocityY;
                if (relativePosition.y == 0)
                    velocityY = MaxSpeed;
                else
                    velocityY = Math.Abs(Power / relativePosition.y);


                if (other.gameObject.name == "littlePig")
                {
                    print("最终速度 X " + velocityX);
                    print("最终速度 y " + velocityY);
                }

                other.gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(Mathf.Clamp(velocityX, MinSpeed, MaxSpeed) * direX,
                        Mathf.Clamp(velocityY, MinSpeed, MaxSpeed)); //设置下速度
            }
        }
    }
}