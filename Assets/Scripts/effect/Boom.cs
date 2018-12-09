using UnityEngine;

public class Boom : MonoBehaviour
{
    /// <summary>
    /// 销毁爆炸特效
    /// </summary>
    public void DestroyBoom()
    {
        Destroy(gameObject); //销毁自身
    }
    
    
}