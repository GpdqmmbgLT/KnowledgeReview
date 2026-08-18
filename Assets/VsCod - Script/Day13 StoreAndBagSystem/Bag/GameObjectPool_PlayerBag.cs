using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池类
/// </summary>
public class GameObjectPool_PlayerBag : MonoBehaviour
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
  /// 取出对象并实例化激活
  /// </summary>
  /// <param name="prefabe">预制体</param>
  /// <param name="parents">制定父物体</param>
  /// <returns></returns>
  public GameObject GetObj(GameObject prefabe, Transform parents)
  {
    if (Count == 0)
    {
      GameObject newObj = Instantiate(prefabe, parents);
      return newObj;
    }
    GameObject newObjs = objQue.Dequeue();
    newObjs.SetActive(true);
    return newObjs;
  }
  /// <summary>
  /// 回收对象
  /// </summary>
  /// <param name="obj">待回收的对象</param>
  public void RecycleObj(GameObject obj)
  {
    Debug.Log("正在进行回收");
    obj.SetActive(false);
    objQue.Enqueue(obj);
    Debug.Log("对象池可用数量是：" + Count);
  }
}
