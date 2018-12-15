using UnityEngine;

namespace varia
{
    public class Dice : MonoBehaviour
    {
        public AudioClip[] CollisionAudios; //碰撞时的声音
        public Sprite[] Sprites; //图片数组
        private SpriteRenderer spriteRenderer; //图片渲染器

        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            if (Sprites != null && Sprites.Length != 0)
            {
                int index = Random.Range(0, Sprites.Length);
                spriteRenderer.sprite = Sprites[index]; //换个面
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.relativeVelocity.magnitude > 5)
            {
                int index = Random.Range(0, CollisionAudios.Length);
                AudioUtil.sInstance.AudioPlay(CollisionAudios[index], transform.position); //播放碰撞的音效
                if (Sprites != null && Sprites.Length != 0)
                {
                    index = Random.Range(0, Sprites.Length);
                    spriteRenderer.sprite = Sprites[index]; //换个面
                }
            }
        }
    }
}