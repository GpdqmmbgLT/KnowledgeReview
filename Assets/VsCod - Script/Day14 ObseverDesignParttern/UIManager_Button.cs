using System;
using UnityEngine;
/// <summary>
/// 控制本canvas的按钮事件绑定
/// </summary>
public class UIManager_Button : MonoBehaviour
{
  [Header("挂载的DataManager_PlayerEleme脚本")]
  public DataManager_PlayerEleme dataManager_PlayerEleme;
  public int damageNum = 5;//增加/减少的伤害值
  public void AddBlood()
  {
    dataManager_PlayerEleme.PlayerBlood += damageNum;
  }
  public void ReduceBlood()
  {
    dataManager_PlayerEleme.PlayerBlood -= damageNum;
  }
  public void AddHunger()
  {
    dataManager_PlayerEleme.PlayerHunger += damageNum;
  }
  public void ReduceHunger()
  {
    dataManager_PlayerEleme.PlayerHunger -= damageNum;
  }
  public void AddStrength()
  {
    dataManager_PlayerEleme.PlayerStrength += damageNum;
  }
  public void ReduceStrength()
  {
    dataManager_PlayerEleme.PlayerStrength -= damageNum;
  }
}