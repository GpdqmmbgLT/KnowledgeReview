using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
/// <typeparam name="T">有无参构造的特定类型</typeparam>
public class ObjectPool<T> where T : new()
{
    public Queue<T> values = new Queue<T>();
    public int Count
    {
        get
        {
            return values.Count;
        }
    }
    /// <summary>
    /// 取出对象池
    /// </summary>
    /// <returns>T对象</returns>
    public T Create()
    {
        if (Count != 0)
        {
            T t = values.Dequeue();
            Debug.Log(t != null ? "取出成功！" : "取出失败!");
            return t;
        }
        else
        {
            T t = Activator.CreateInstance<T>();
            Debug.Log(t != null ? "创建成功并成功取出！" : "创建失败!");
            return t;//通过反射的方法动态创建实例对象

        }

    }
    /// <summary>
    /// 回收对象池
    /// </summary>
    /// <param name="obj">待回收参数</param>
    public void Recycle(T obj)
    {
        if (values.Contains(obj))
        {
            Debug.Log("对象已经存在,无需回收");
            return;
        }
        values.Enqueue(obj);
        Debug.Log("回收成功！");

    }
}

public class Bullet
{
    public int id;//子弹编号
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            if (value >= 1)
            {
                id = value;
            }
        }
    }

    /// <summary>
    /// 模拟手枪开火发射子弹
    /// </summary>
    public void Fire()
    {
        Debug.Log($"编号为:{ID}的子弹已经发射");
    }
    /// <summary>
    /// 模拟子弹回收
    /// </summary>
    public void Reset()
    {
        Debug.Log($"编号为:{ID}的子弹已经回收");
    }
}
public class GenericReview : MonoBehaviour
{
    public GameObject bullet;//子弹
    ObjectPool<Bullet> bulletPool;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new ObjectPool<Bullet>();
        Bullet bullet1 = bulletPool.Create();
        bulletPool.Recycle(bullet1);
        bullet1.ID = 1;
        bullet1.Reset();
        Bullet bullet2 = bulletPool.Create();
        bullet2.Reset();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
