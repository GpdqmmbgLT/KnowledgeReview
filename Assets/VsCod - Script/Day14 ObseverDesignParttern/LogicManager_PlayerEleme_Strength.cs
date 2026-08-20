using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// 对于玩家饥饿度变化时的逻辑事件传递
/// </summary>
public class LogicManager_PlayerEleme_Strength : MonoBehaviour
{
  [Header("体力不足面板物体")]
  public GameObject strength_Panel;
  [Header("动画参数名")]
  public string animationName;
  [Header("玩家动画组件")]
  public Animator animator;
  Action strengthLessThan20, strengthMoreThan20;//提前记录的事件
  void OnEnable()
  {
    //存入事件，创建并订阅
    strengthLessThan20 += () => { ChangeAnimation(false); };
    strengthMoreThan20 += () => { ChangeAnimation(true); };
    ObserverEvent.Instance.SubscribeEvent(EventStatus.strengthLessThan20, strengthLessThan20);
    ObserverEvent.Instance.SubscribeEvent(EventStatus.strengthMoreThan20, strengthMoreThan20);
  }
  void OnDisable()
  {
    //退订事件
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.strengthLessThan20, strengthLessThan20);
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.strengthMoreThan20, strengthMoreThan20);
  }
  /// <summary>
  /// 改变动画状态
  /// </summary>
  /// <param name="isRun">是否开启跑步</param>
  public void ChangeAnimation(bool isRun)
  {
    animator.SetBool(animationName, isRun);
  }
}