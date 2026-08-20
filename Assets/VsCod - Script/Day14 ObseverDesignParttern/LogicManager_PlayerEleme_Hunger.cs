using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// 对于玩家饥饿度变化时的逻辑事件传递
/// </summary>

public class LogicManager_PlayerEleme_Hunger : MonoBehaviour
{
  [Header("挂载的DataManager_PlayerEleme脚本")]
  public DataManager_PlayerEleme dataManager_PlayerEleme;
  [Header("饥饿图片物体")]
  public GameObject hungerImg;
  public float damage_Blood = 5;//扣血值
  Action hungerMoreThan20, hungerLessThan20, hungerBecomeZero;//提前记录的事件
  void OnEnable()
  {
    //存入事件，创建并订阅
    hungerLessThan20 += () => { OpenRedPanel(true); };
    hungerMoreThan20 += () => { OpenRedPanel(false); };
    hungerBecomeZero += () => { StartCoroutine(HungerDeath()); };
    ObserverEvent.Instance.SubscribeEvent(EventStatus.hungerLessThan20, hungerLessThan20);
    ObserverEvent.Instance.SubscribeEvent(EventStatus.hungerMoreThan20, hungerMoreThan20);
    ObserverEvent.Instance.SubscribeEvent(EventStatus.hungerBecomeZero, hungerBecomeZero);
  }
  void OnDisable()
  {
    //退订事件
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.hungerLessThan20, hungerLessThan20);
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.hungerBecomeZero, hungerBecomeZero);
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.hungerMoreThan20, hungerMoreThan20);
  }
  /// <summary>
  /// 打开/关闭饥饿图标物体
  /// </summary>
  /// <param name="isOpen">是否开启面板</param>
  public void OpenRedPanel(bool isOpen)
  {
    //暂停所有协程
    StopAllCoroutines();
    hungerImg.SetActive(isOpen);
  }
  /// <summary>
  /// 协程，玩家持续扣血
  /// </summary>
  public IEnumerator HungerDeath()
  {
    //每两秒扣血damage_Blood点
    while (true)
    {
      dataManager_PlayerEleme.PlayerBlood -= damage_Blood * 0.5f * Time.deltaTime;
      yield return null;
    }
  }
}