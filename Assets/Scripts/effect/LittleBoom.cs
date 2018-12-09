using UnityEngine;

public class LittleBoom : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {

        if (particleSystem.isStopped)
        {
            transform.parent.GetComponent<Boom>().DestroyBoom(); //销毁爆炸特效   
        }
    }
}