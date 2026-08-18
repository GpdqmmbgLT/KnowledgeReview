using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对协程的知识进行回顾（不同类型，返回类型）
/// 注：每次开启协程是新的引用，要控制当前协程需要保存引用
/// </summary>
public class CoroutineReview : MonoBehaviour
{
    public Action action;
    public Coroutine co_waitTime;
    public Coroutine co_Unitl;
    void Start()
    {
        co_waitTime = StartCoroutine(Co_waitTime());
        co_Unitl = StartCoroutine(Co_Unitl());
    }
    /// <summary>
    /// 等待玩家按下任意键继续游戏
    /// </summary>
    /// <returns>判断是否按下任意键</returns>
    public IEnumerator Co_Unitl()
    {
        Debug.Log("游戏加载成功！按下任意键继续");
        yield return new WaitUntil(() => Input.anyKeyDown);//当返回结果为true的时候继续
        Debug.Log("游戏开始！");
        StopCoroutine(co_waitTime);//暂停输出时间协程
        yield return new WaitForSeconds(2);
        Debug.Log("按下任意键结束游戏");
        co_waitTime = StartCoroutine(Co_waitTime());//开始输出时间协程
        yield return new WaitWhile(() => !Input.anyKeyDown);//当返回结果为false的时候继续
        Debug.Log("游戏结束！");
        StopAllCoroutines();//暂停所有协程
    }
    /// <summary>
    /// 每隔一秒输出当前设备的时间（循环）
    /// </summary>
    /// <returns>等待1秒</returns>
    public IEnumerator Co_waitTime()
    {
        while (true)
        {
            Debug.Log(DateTime.Now);
            yield return new WaitForSeconds(1);
        }
    }
    /*     public Coroutine coroutine(Action action1,Action action2)
        {

        }
     */
}
