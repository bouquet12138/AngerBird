using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject path1, path2, path3; //路径1 路径2 路径3

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
    /// <param name="pathId">路径id</param>
    /// <param name="position">位置</param>
    public void AddPath(int pathId, Vector3 position)
    {
        // AddEmptyPath(); //添加一个空路径
        if (pathId % 2 == 0)
        {
            GameObject gameObject = Instantiate(path1, position, Quaternion.identity); //生成路径1
            gameObject.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
        }
        else
        {
            GameObject gameObject = Instantiate(path2, position, Quaternion.identity); //生成路径2
            gameObject.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
        }
    }

    /// <summary>
    /// 添加一个特效path
    /// </summary>
    public void AddSkillPath(Vector3 position)
    {
        GameObject gameObject = Instantiate(path3, position, Quaternion.identity); //生成路径1
        gameObject.transform.parent = transform.GetChild(transform.childCount - 1); //添加到它最后一个孩子身上
    }
}