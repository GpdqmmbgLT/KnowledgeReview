using System;
using UnityEngine;
[Serializable]
public class TimeData
{
  public int gameDays;
  public float curruntTime;
}
public class DataManager_Time : MonoBehaviour, ISaveable
{
  static DataManager_Time instance;
  public static DataManager_Time Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new DataManager_Time();
      }
      return instance;
    }
  }
  private int _gameDays;
  private float _curruntTime;
  void Awake()
  {
    instance = this;
  }
  void Start()
  {
    _gameDays = 5;
    _curruntTime = 75.6f;
  }
  /// <summary>
  /// 打印输出本类变量
  /// </summary>
  public void ShowData()
  {
    Debug.Log("游戏天数:" + _gameDays);
    Debug.Log("游戏时间:" + _curruntTime);
  }
  public string SaveData()
  {
    TimeData timeData = new TimeData();
    timeData.gameDays = _gameDays;
    timeData.curruntTime = _curruntTime;
    return JsonUtility.ToJson(timeData);
  }
  public void Load(string jsonText)
  {
    try
    {
      TimeData timeData = JsonUtility.FromJson<TimeData>(jsonText);
      _curruntTime = timeData.curruntTime;
      _gameDays = timeData.gameDays;
    }
    catch (Exception e)
    {
      Debug.Log("恢复数据异常!异常数据类型为:TimeData\n" + e);
    }
  }
}