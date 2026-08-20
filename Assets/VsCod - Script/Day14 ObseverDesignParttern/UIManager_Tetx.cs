using System;
using TMPro;
using UnityEngine;
/// <summary>
/// 控制canvas各属性文本显示
/// </summary>
public class UIManager_Text : MonoBehaviour
{
  [Header("挂载的DataManager_PlayerEleme脚本")]
  public DataManager_PlayerEleme dataManager_PlayerEleme;
  [Header("控制血量的TextMeshProUGUI组件")]
  public TextMeshProUGUI bloodText;
  [Header("控制饥饿度的TextMeshProUGUI组件")]
  public TextMeshProUGUI hungerText;
  [Header("控制体力的TextMeshProUGUI组件")]
  public TextMeshProUGUI strengthText;
  Action anyStatus;
  void OnEnable()
  {
    //创建并订阅事件anyStatus
    anyStatus += RefreshData;
    ObserverEvent.Instance.SubscribeEvent(EventStatus.anyStatus, anyStatus);
  }

  void OnDisable()
  {
    //退订事件anyStatus
    ObserverEvent.Instance.UnSubscribeEvent(EventStatus.anyStatus, anyStatus);
  }
  /// <summary>
  /// 刷新三大属性的（血量，饥饿值，体力）Text显示
  /// </summary>
  public void RefreshData()
  {
    bloodText.text = "Blood:" + dataManager_PlayerEleme.PlayerBlood.ToString("F2");
    hungerText.text = "Hunger:" + dataManager_PlayerEleme.PlayerHunger;
    strengthText.text = "Strength:" + dataManager_PlayerEleme.PlayerStrength;
  }

}