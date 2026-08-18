using System;
using UnityEngine;
using UnityEditor;
using System.IO;
/// <summary>
/// 动物参数类，定义动物的基本属性
/// </summary>
[Serializable]
public class AnimalParameters
{
  public int animalID;
  public string animalName;
  public int animalAge;
  public Color32 animalColor;
  public string animalVoice;

  /// <summary>
  /// 创建动物属性类
  /// </summary>
  /// <param name="animalID">动物编号</param>
  /// <param name="animalName">动物名称</param>
  /// <param name="animalAge">动物年龄</param>
  /// <param name="animalColor">动物毛发颜色</param>
  /// <param name="animalVoice">动物叫声</param>
  public AnimalParameters(int animalID, string animalName, int animalAge, Color32 animalColor, string animalVoice)
  {
    this.animalID = animalID;
    this.animalName = animalName;
    this.animalAge = animalAge;
    this.animalColor = animalColor;
    this.animalVoice = animalVoice;
  }
}
public static class Editor_AniamlConfig
{
  [MenuItem("Tools/Myself Import AnimalConfig From CSV")]
  /// <summary>
  /// 编辑器下使用，导入资源到编辑器中
  /// </summary>
  public static void CreatAssess()
  {
    //csv文件路径
    string filePath = Application.dataPath + "/Script/Day14 ScriptableObjectInput/AnimalConfig.csv";
    //配置文件在Assess下的路径
    string configPath = "Assets/Script/Day14 ScriptableObjectInput/AnimalConfig.asset";
    //animalConfig对象获取引用
    AnimalConfigData anidata = AssetDatabase.LoadAssetAtPath<AnimalConfigData>(configPath);
    anidata.animalParameters.Clear();
    GetConfig(filePath, anidata);
    //标记资源为脏
    EditorUtility.SetDirty(anidata);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
    Debug.Log($"已导入 {anidata.animalParameters.Count} 条动物数据到 {configPath}");

  }
  /// <summary>
  /// 运行时读取数据并存储对象中
  /// </summary>
  /// <param name="filePath">文件地址</param>
  /// <param name="anidata">AnimalConfig对象</param>
  public static void GetConfig(string filePath, AnimalConfigData anidata)
  {
    if (!File.Exists(filePath))
    {
      Debug.Log("文件不存在!请检查路径");
      return;
    }
    if (Path.GetExtension(filePath).ToLower() != ".csv")
    {
      Debug.Log("不是csv文件");
      return;
    }
    try
    {
      string[] oringinData = File.ReadAllLines(filePath);
      foreach (var item in oringinData)
      {
        string[] cells = item.Split(",");
        if (cells.Length == 0 || cells[0] == "Numbers") continue;//去掉空行和第一行
        anidata.animalParameters.Add(new AnimalParameters(
            TypeChange<int>(cells[0]),
            cells[1],
            TypeChange<int>(cells[2]),
            new Color32(TypeChange<byte>(cells[3]),
                        TypeChange<byte>(cells[4]),
                        TypeChange<byte>(cells[5]),
                        TypeChange<byte>(cells[6])),
            cells[7]
        ));
      }
      Debug.Log("导入成功!");
    }
    catch (Exception e)
    {
      Debug.Log("文件读取异常！请重试");
      throw e;
    }
  }
  /// <summary>
  /// 打印动物信息
  /// </summary>
  /// <param name="anidata">存入的动物配置数据对象</param>
  public static void ShowData(AnimalConfigData anidata)
  {
    Debug.Log("编号\t名称\t年龄\t毛发颜色\t\t\t叫声");
    foreach (var item in anidata.animalParameters)
    {
      Debug.Log(item.animalID + "\t" + item.animalName + "\t" + item.animalAge + "\t" + item.animalColor + "\t" + item.animalVoice);
    }
  }
  /// <summary>
  /// 将字符串转换为任意类型
  /// </summary>
  /// <typeparam name="T">任意类型</typeparam>
  /// <param name="test">需要转换的对象</param>
  /// <returns>
  /// default - 默认值（转换失败）
  /// other  - 成功值
  /// </returns>
  public static T TypeChange<T>(string test)
  {
    if (test == "" || test == null)
    {
      Debug.Log("字符串为空");
      return default;
    }
    try
    {
      return (T)Convert.ChangeType(test, typeof(T));
    }
    catch
    {
      Debug.Log("转换失败！赋予默认值");
      return default;
    }
  }
}