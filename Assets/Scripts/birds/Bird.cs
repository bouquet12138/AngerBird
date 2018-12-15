using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace birds
{
    public class Bird : MonoBehaviour
    {
        private static readonly int BIRD_IDLE = 0; //空闲
        private static readonly int BIRD_DOWN = 1; //按下
        private static readonly int BIRD_READY_FLY = 2; //准备飞行
        private static readonly int BIRD_FLY = 3; //飞行
        private static readonly int BIRD_HAS_SHOW_SKILL = 4; //已经展示技能
        protected static readonly int BIRD_COLLIDER = 5; //碰撞

        protected int CurrentState = BIRD_IDLE;

        private float radius; //小鸟的半径
        public GameObject boom; //爆炸效果
        protected BirdAnim birdAnim; //得到小鸟身上的动画

        public GameObject score;
        public int addScore = 10000; //要添加的成绩

        public AudioClip slingShotAudio; //弹弓的声音
        public AudioClip selectedAudio; //小鸟选择
        public AudioClip flyAudio; //小鸟飞
        public List<AudioClip> collisionAudios; //碰撞的声音们
        public AudioClip destroyAudio; //销毁的声音

        protected Path path; //path 对象

        private int rotationDir = 1; //旋转方向
        private float animTime; //动画进行的时间
        private Vector3 startPos; //开始的位置
        private float a, deltaX; //一元二次函数的a 起始位置和终止位置相差的X
        private bool readyAnimIsEnd; //准备动画是否结束
        private readonly float READY_TIME = 0.5f; //准备需要多长时间 


        [HideInInspector] public SpringJoint2D springJoint2D; //弹性组件
        [HideInInspector] public Rigidbody2D rigidBody2D; //刚体
        [HideInInspector] public Collider2D TriggerCollider; //触发碰撞器

        public float maxDis = 1.5f; //可以拉动的最大距离

        private LineRenderer leftLine; //左边线
        private LineRenderer rightLine; //右边线

        private Transform leftPosition; //左边枝子的位置
        private Transform rightPosition; //右边枝子的位置


        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Awake()
        {
            CircleCollider2D[] circleCollider2Ds = gameObject.GetComponents<CircleCollider2D>();

            foreach (CircleCollider2D myCollider2D in circleCollider2Ds)
            {
                if (!myCollider2D.isTrigger)
                {
                    radius = myCollider2D.radius;
                }
                else
                {
                    TriggerCollider = myCollider2D; //得到触发碰撞器
                }
            }

            path = transform.parent.parent.Find("path").GetComponent<Path>(); //路径
            birdAnim = transform.GetChild(0).GetComponent<BirdAnim>(); //得到动画
            springJoint2D = GetComponent<SpringJoint2D>(); //得到弹性组件
            rigidBody2D = GetComponent<Rigidbody2D>(); //得到这个小鸟的刚体组件

            leftPosition = transform.parent.parent.Find("branch").Find("leftBranch").GetChild(0); //左边的位置
            rightPosition = transform.parent.parent.Find("branch").Find("rightBranch").GetChild(0); //右边的位置

            leftLine = leftPosition.GetComponent<LineRenderer>();
            rightLine = rightPosition.GetComponent<LineRenderer>();

            float xGridNum = Screen.width / (Screen.height / 10f); //水平网格数
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        private void OnMouseDown()
        {
            if (!enabled)
                return;

            if (CurrentState == BIRD_IDLE && readyAnimIsEnd && !GameManager.sInstance.gameIsOver)
            {
                CurrentState = BIRD_DOWN; //当前状态改为按下

                CameraVary.sInstance.BirdUse = true; //小鸟正在使用就不要响应事件了
                rigidBody2D.bodyType = RigidbodyType2D.Dynamic; //动态
                AudioUtil.sInstance.AudioPlay(slingShotAudio, transform.position); //播放弹弓声音
                rigidBody2D.isKinematic = true; //开启动力学
                AudioUtil.sInstance.AudioPlay(selectedAudio, transform.position); //播放选择的声音
            }
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private void OnMouseUp()
        {
            if (!enabled)
                return;
            //如果小鸟按下
            if (CurrentState == BIRD_DOWN && readyAnimIsEnd && !GameManager.sInstance.gameIsOver)
            {
                CurrentState = BIRD_READY_FLY; //小鸟当前状态为准备飞行状态

                rigidBody2D.isKinematic = false; //关闭动力学

                rightLine.SetPosition(0, rightPosition.position); //右边枝子的位置
                rightLine.SetPosition(1, leftPosition.position); //左边枝子的位置

                leftLine.SetPosition(0, leftPosition.position); //左边皮带起点
                leftLine.SetPosition(1, rightPosition.position); //右边枝子的位置

                path.AddEmptyPath(); //添加一个空路径

                Invoke("Fly", 0.1f); //0.1秒
            }
        }

        /// <summary>
        /// 起飞
        /// </summary>
        private void Fly()
        {
            GameCursor.sInstance.isUi = true; //改变抓抓的样式
            birdAnim.Fly(); //飞去吧
            AudioUtil.sInstance.AudioPlay(flyAudio, transform.position); //播放飞行声音
            springJoint2D.enabled = false; //禁用弹簧
            Invoke("FlyNext", 4.2f); //4.2秒后消失

            CurrentState = BIRD_FLY; //小鸟开始飞行了
        }

        /// <summary>
        /// 飞出去之后
        /// </summary>
        protected virtual void FlyNext()
        {
            CameraVary.sInstance.BirdUse = false; //小鸟结束使用了 用户可以滑动了
            CameraVary.sInstance.Home(); //相机归位
            GameManager.sInstance.NextBird(); //替换下一只小鸟
            Instantiate(boom, transform.position, Quaternion.identity); //生成爆炸效果
            AudioUtil.sInstance.AudioPlay(destroyAudio, transform.position); //播放销毁的声音
            Destroy(gameObject); //移除小鸟
        }

        /// <summary>
        /// 视图更新的时候
        /// </summary>
        protected void Update()
        {
            ReadyAnim();
            DrawLine();
            ShowPath();
            MoveCamera();

            if (Input.GetMouseButtonDown(0) && CurrentState == BIRD_FLY) //鼠标左键按下 并且小鸟正在飞
            {
                ShowSkill(); //展示技能吧
            }
        }

        /// <summary>
        /// 准备动画
        /// </summary>
        private void ReadyAnim()
        {
            if (!readyAnimIsEnd)
            {
                if (transform.position == GameManager.sInstance.originPos)
                {
                    path.RemoveFirstChild(); //移除第一条路径
                    readyAnimIsEnd = true;
                }
                else
                {
                    if (animTime == 0)
                    {
                        startPos = transform.position;
                        deltaX = GameManager.sInstance.originPos.x - transform.position.x;
                        if (deltaX > 0)
                            rotationDir = -1; //向后旋转                        
                        float deltaY = GameManager.sInstance.originPos.y - transform.position.y;
                        a = deltaY / (deltaX * (-deltaX * 0.6f)); //计算一下a
                    }

                    animTime += Time.deltaTime;
                    float percent = animTime / READY_TIME;

                    float nowX = deltaX * percent;
                    float nowY = a * nowX * (nowX - 1.6f * deltaX) + startPos.y;
                    nowX += startPos.x; //加上开始的坐标

                    transform.position = new Vector3(nowX, nowY, transform.position.z);

                    transform.Rotate(0, 0, Time.deltaTime / READY_TIME * 360 * rotationDir);

                    if (animTime >= READY_TIME)
                    {
                        transform.position = GameManager.sInstance.originPos; //归下位
                        transform.localEulerAngles = new Vector3(0, 0, 0); //旋转归0
                        path.RemoveFirstChild(); //移除第一条路径
                        readyAnimIsEnd = true; //准备动画结束了
                    }
                }
            }
        }

        /// <summary>
        /// 如果用户正按下的话 绘制皮筋
        /// </summary>
        private void DrawLine()
        {
            if (CurrentState == BIRD_DOWN) //如果当前状态为按下 那么画线
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position += new Vector3(0, 0, -Camera.main.transform.position.z); //设置位置

                if (Vector3.Distance(transform.position, rightPosition.position) > maxDis)
                {
                    Vector3 pos = (transform.position - rightPosition.position).normalized;
                    pos *= maxDis; //乘上最大距离
                    transform.position = pos + rightPosition.position;
                }

                Line(); //画线
            }
        }

        /// <summary>
        /// 绘制皮筋
        /// </summary>
        private void Line()
        {
            rightLine.SetPosition(0, rightPosition.position); //右边枝子的位置
            rightLine.SetPosition(1, transform.position); //小鸟的位置

            leftLine.SetPosition(0, leftPosition.position); //左边皮带起点
            leftLine.SetPosition(1, transform.position); //小鸟的位置
        }

        /// <summary>
        /// 展示小鸟飞行路径
        /// </summary>
        private void ShowPath()
        {
            if (CurrentState == BIRD_READY_FLY || CurrentState == BIRD_FLY ||
                CurrentState == BIRD_HAS_SHOW_SKILL)
            {
                path.AddPath(transform.position); //添加路径
            }
        }

        /// <summary>
        /// 移动摄像机
        /// </summary>
        private void MoveCamera()
        {
            if (CameraVary.sInstance.BirdUse)
            {
                CameraVary.sInstance.SetBirdPosition(transform.position, radius); //将小鸟的位置告诉一下摄像机
                CameraVary.sInstance.RemoveCamera(); //移动相机
            }
        }

        /// <summary>
        /// 碰撞盒进入
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (!enabled) //不可用就不要响应碰撞事件了
                return;

            if (CurrentState == BIRD_FLY || CurrentState == BIRD_HAS_SHOW_SKILL) //如果小鸟正在飞或者展示了技能了
            {
                CurrentState = BIRD_COLLIDER; //小鸟状态变为碰撞
                CameraVary.sInstance.BirdUse = false; //小鸟结束使用了 用户可以滑动了
                GameCursor.sInstance.isUi = false; //变回正常手
                birdAnim.hurt(); //受伤了
            }

            if (other.relativeVelocity.magnitude > 6) //相对速度大于8
            {
                Instantiate(boom, transform.position, Quaternion.identity); //生成一个碰撞特效

                int audioIndex = Random.Range(0, collisionAudios.Count);
                AudioUtil.sInstance.AudioPlay(collisionAudios[audioIndex], transform.position); //播放碰撞声音
            }
        }

        /// <summary>
        /// 展示技能
        /// </summary>
        protected virtual void ShowSkill()
        {
            CurrentState = BIRD_HAS_SHOW_SKILL; //状态变为技能已展示
            GameCursor.sInstance.isUi = false; //变回手
        }

        /// <summary>
        /// 胜利效果
        /// </summary>
        public void Win()
        {
            Score.sInstance.AddScore(addScore); //加分
            Instantiate(score, transform.position, Quaternion.identity); //生成加分效果
        }

        /// <summary>
        /// 准备就绪
        /// </summary>
        public void Ready()
        {
            birdAnim.Ready(); //准备就绪
        }
    }
}