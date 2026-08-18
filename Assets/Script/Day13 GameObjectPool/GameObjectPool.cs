using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池类
/// </summary>
public class GameObjectPool : MonoBehaviour
{
    Queue<GameObject> objQue = new Queue<GameObject>();//已经实例化的存储对象
    public int Count
    {
        get
        {
            return objQue.Count;//返回池中可用对象
        }
    }
    /// <summary>
    /// 取出对象并实例化
    /// </summary>
    /// <param name="prefabe">预制体</param>
    /// <param name="position">出现位置</param>
    /// <param name="rotation">旋转角度</param>
    /// <param name="parents">制定父物体</param>
    /// <returns></returns>
    public GameObject GetObj(GameObject prefabe, Vector3 position, Quaternion rotation, Transform parents)
    {

        if (Count == 0)
        {
            GameObject newObj = Instantiate(prefabe, position, rotation, parents);
            //newObj.GetComponent<Bullets>().recyclePool = RecycleObj;//获取子弹身上的脚本注册委托事件
            newObj.GetComponent<BulletsTest>().recyclePool = RecycleObj;
            return newObj;
        }
        GameObject newObjs = objQue.Dequeue();
        newObjs.transform.position = position;
        newObjs.SetActive(true);
        return newObjs;
    }

    public void RecycleObj(GameObject obj)
    {
        Debug.Log("正在进行回收");
        objQue.Enqueue(obj);
        Debug.Log("对象池可用数量是：" + Count);
    }
}
