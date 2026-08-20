using System;
using System.Collections.Generic;
using UnityEngine;
public enum EventStatus
{
  bloodLessThan30,
  bloodMoreThan30,
  bloodBecomeZero,
  hungerLessThan20,
  hungerMoreThan20,
  hungerBecomeZero,
  strengthLessThan20,
  strengthMoreThan20,
  anyStatus
}
/// <summary>
/// 观察者事件系统，包含（注册，订阅，退订，触发）四项功能
/// </summary>
public class ObserverEvent
{
  private static ObserverEvent instance;
  public static ObserverEvent Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new ObserverEvent();
      }
      return instance;
    }
  }
  //事件存储字典
  private Dictionary<EventStatus, Action> eventData = new Dictionary<EventStatus, Action>();
  /// <summary>
  /// 订阅事件
  /// </summary>
  /// <param name="eventStatus">事件类型</param>
  /// <param name="action">追加事件</param>
  public void SubscribeEvent(EventStatus eventStatus, Action action)
  {
    //如果已经注册了该事件，则直接追加到订阅列表
    if (eventData.ContainsKey(eventStatus))
    {
      eventData[eventStatus] += action;
    }
    //如果不包含该事件，则创建新事件后追加到订阅列表
    else
    {
      RegisterEvent(eventStatus, action);
    }
    Debug.Log("订阅成功!:" + eventStatus);
  }
  /// <summary>
  /// 退订事件
  /// </summary>
  /// <param name="eventStatus">事件类型</param>
  /// <param name="action">追加事件</param>
  public void UnSubscribeEvent(EventStatus eventStatus, Action action)
  {
    if (eventData.ContainsKey(eventStatus))
    {
      eventData[eventStatus] -= action;
      Debug.Log("退订成功!:" + eventStatus);
    }
    else
    {
      Debug.Log("暂无此事件,无需退订!" + eventStatus);
    }
  }
  /// <summary>
  /// 注册事件
  /// </summary>
  /// <param name="eventStatus">事件类型</param>
  /// <param name="action">首发事件</param>
  private void RegisterEvent(EventStatus eventStatus, Action action)
  {
    if (eventData.ContainsKey(eventStatus))
    {
      Debug.Log("已经注册过该事件:" + eventStatus);
      return;
    }
    eventData.Add(eventStatus, action);
    Debug.Log("事件注册成功!:" + eventStatus);
  }
  /// <summary>
  /// 触发事件
  /// </summary>
  /// <param name="eventStatus">事件类型</param>
  public void TriggerEvent(EventStatus eventStatus)
  {
    eventData[eventStatus]?.Invoke();
  }
}