using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 子弹类，用于控制子弹碰撞，回收等工作
/// </summary>
public class Bullets : MonoBehaviour
{
    float speed = 7;
    float lifeTime = 3;
    public Vector3 movePosition;

    public Action<GameObject> recyclePool;//回收事件，由外部注册
    void OnEnable()
    {
        StartCoroutine(Recycle(lifeTime));
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * movePosition);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("子弹与" + collision.gameObject.name + "发生碰撞");
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        recyclePool?.Invoke(gameObject);//如果事件不为空进行回收
    }
    /// <summary>
    /// 子弹的回收
    /// </summary>
    /// <param name = "secends" > 秒数 </ param >
    /// <returns>等待N秒后继续执行</returns>
    public IEnumerator Recycle(float secends)
    {
        yield return new WaitForSeconds(secends);
        gameObject.SetActive(false);
    }

}
