using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
public interface ISaveable
{
  /// <summary>
  /// 转换为Json对象
  /// </summary>
  /// <returns>Json对象</returns>
  public string SaveData();
  /// <summary>
  /// 将Json对象转换回数据
  /// </summary>
  /// <param name="jsonText">已经存储为json的对象</param>
  public void Load(string jsonText);
}
/// <summary>
/// 总数据参数类 以key - name  value - JSONString为参数
/// </summary>
[Serializable]
public class AllDataparameter
{

  public string fromToName;//从哪个对象上获取的
  public string jsonString;//获取的json数据

  public AllDataparameter(string fromToName, string jsonString)
  {
    this.fromToName = fromToName;
    this.jsonString = jsonString;
  }
}
/// <summary>
/// 总数据类，以列表<总数据参数>为存储对象
/// </summary>
[Serializable]
public class AllData
{
  public List<AllDataparameter> allDatas = new List<AllDataparameter>();
}
public class DataManager_ToJson : MonoBehaviour
{
  public List<ISaveable> saveables = new List<ISaveable>();//定义接口数组
  public string savePath;//文件存储地址
  public float timer;
  public float coolingTime = 15;
  void Start()
  {
    savePath = Path.Combine(Application.persistentDataPath, "save.json");
    saveables.Add(FindObjectOfType<DataManager_Bag>());
    saveables.Add(FindObjectOfType<DataManager_Player>());
    saveables.Add(FindObjectOfType<DataManager_Time>());
  }
  void Update()
  {
    timer += Time.unscaledDeltaTime;
    if (Input.GetKeyDown(KeyCode.O) && timer >= coolingTime)
    {
      StartCoroutine(ReturnData());
      timer = 0;
    }
    if (Input.GetKeyDown(KeyCode.I) && timer >= coolingTime)
    {
      StartCoroutine(SavaData());
      timer = 0;
    }

  }
  /// <summary>
  /// 遍历所有可存储Json对象并存储进文件中
  /// </summary>
  /// <returns></returns>
  IEnumerator SavaData()
  {
    //等待一帧后执行
    yield return null;
    AllData allData = new AllData();
    //将对象名与Josn数据存储到列表中
    foreach (var item in saveables)
    {
      allData.allDatas.Add(new AllDataparameter(item.GetType().Name, item.SaveData()));
    }
    //如果存入数据不为0便进行存储操作
    if (allData.allDatas.Count != 0)
    {
      try
      {
        string allJson = JsonUtility.ToJson(allData, true);
        File.WriteAllText(savePath, allJson);
        Debug.Log($"存储成功！共存入{allData.allDatas.Count}条数据");
        Debug.Log("文件地址为:" + savePath);
        ShowData();
      }
      catch (Exception e)
      {
        Debug.Log("保存失败！" + e);
      }
    }
    else
    {
      Debug.Log("存入数据异常,中断本次存储操作");
    }
  }

  /// <summary>
  /// 读取存档，转换回数据
  /// </summary>
  /// <returns></returns>
  public IEnumerator ReturnData()
  {
    yield return null;
    if (!File.Exists(savePath))
    {
      Debug.Log("文件不存在!请检查路径");
      yield break;
    }
    try
    {
      string returnTest = File.ReadAllText(savePath);
      AllData returnData = JsonUtility.FromJson<AllData>(returnTest);//把json数据转换回对象
      //遍历对象，并与saveables接口列表进行姓名匹配，调用其Load函数
      foreach (var item in returnData.allDatas)
      {
        foreach (var ite in saveables)
        {
          if (item.fromToName == ite.GetType().Name)
          {
            ite.Load(item.jsonString);
            break;
          }
        }
      }
      ShowData();
    }
    catch (Exception e)
    {
      Debug.Log("读取存档失败!" + e);
    }
  }
  /// <summary>
  /// 展示所有对象的数据
  /// </summary>
  public void ShowData()
  {
    DataManager_Bag.Instance.ShowData();
    DataManager_Player.Instance.ShowData();
    DataManager_Time.Instance.ShowData();
  }
}