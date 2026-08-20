using System;
using UnityEngine;
/// <summary>
/// 对于玩家血量变化时的逻辑事件传递
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class LogicManager_PlayerEleme_Blood : MonoBehaviour
{
  [Header("闪烁的红色面板物体")]
  public GameObject redPage_Panel;
  [Header("游戏结束面板")]
  public GameObject gameOver_Panel;
  [Header("死亡音效")]
  public AudioClip deathVoice;
  AudioSource audios;
  Action bloodLessThan30, bloodMoreThan30, bloodBecomeZero;//提前记录的事件
  void OnEnable()
  {
    //存入事件，创建并订阅
    bloodLessThan30 += () => { OpenRedPanel(true); };
    bloodMoreThan30 += () => { OpenRedPanel(false); };
    bloodBecomeZero += PlayerDeath;
    ObserverEvent.Instance.SubscribeEvent(EventStatus.bloodLessThan30, bloodLessThan30);
    ObserverEvent.Instance.SubscribeEvent(EventStatus.bloodMoreThan30, bloodMoreThan30);
    ObserverEvent.Instance.SubscribeEvent(EventStatus.bloodBecomeZero, bloodBecomeZero);
  }
  void Start()
  {
    audios = GetComponent<AudioSource>();
  }
  void OnDisable()
  {
    //退订事件
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.bloodLessThan30, bloodLessThan30);
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.bloodMoreThan30, bloodMoreThan30);
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.bloodBecomeZero, bloodBecomeZero);

  }
  /// <summary>
  /// 打开/关闭红色闪烁面板
  /// </summary>
  /// <param name="isOpen">是否开启面板</param>
  public void OpenRedPanel(bool isOpen)
  {
    redPage_Panel.SetActive(isOpen);
  }
  /// <summary>
  /// 玩家死亡-（打开死亡面板，播放死亡音效）
  /// </summary>
  public void PlayerDeath()
  {
    gameOver_Panel.SetActive(true);
    audios.PlayOneShot(deathVoice);
  }
}