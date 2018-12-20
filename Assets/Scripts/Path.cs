using UnityEngine;

public class Path : MonoBehaviour
{
    private int mPathId = 0; //路径id
    public GameObject Path1, Path2, Path3; //路径1 路径2 路径3

    /// <summary>
    /// 移除第一个孩子
    /// </summary>
    public void RemoveFirstChild()
    {
        if (transform.childCount >= 2)
        {
            DestroyImmediate(transform.GetChild(0).gameObject); //移除第一个孩子
        }
    }

    /// <summary>
    /// 添加空路径
    /// </summary>
    public void AddEmptyPath()
    {
        GameObject emptyObject = new GameObject("Empty");
        emptyObject.transform.parent = transform; //加到path上
    }

    /// <summary>
    /// 添加路径
    /// </summary>
    /// <param name="_position"></param>
    public void AddPath(Vector3 _position)
    {
        mPathId++;
        // AddEmptyPath(); //添加一个空路径
        if (mPathId % 2 == 0)
        {
            GameObject generatePath = Instantiate(Path1, _position, Quaternion.identity); //生成路径1
            generatePath.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
            mPathId = 0; //归零 
        }
        else
        {
            GameObject generatePath = Instantiate(Path2, _position, Quaternion.identity); //生成路径2
            generatePath.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
        }
    }

    /// <summary>
    /// 生成一个特效路径
    /// </summary>
    /// <param name="_position">位置</param>
    public void AddSkillPath(Vector3 _position)
    {
        GameObject generatePath = Instantiate(Path3, _position, Quaternion.identity); //生成路径1
        generatePath.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
    }
}