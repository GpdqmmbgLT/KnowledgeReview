using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager_Submit : MonoBehaviour
{
  [Header("提交页面物体")]
  public GameObject submitPage_Panel;
  [Header("信息展示页面物体")]
  public GameObject showInformationPage_Panel;
  [Header("输入框")]
  public TMP_InputField nameInput;
  [Header("输入框的背景物体")]
  public GameObject nameInput_Panle;
  [Header("下拉列表")]
  public TMP_Dropdown professionInput;
  [Header("多选开关数组(玩家能力)")]
  public Toggle[] abilitysInput;
  [Header("多选开关列表的面板物体")]
  public GameObject abilitysInputPanel;
  [Header("难度选择开关组")]
  public ToggleGroup gameDifficutyInput;
  [Header("当前背景板的颜色")]
  public Color32 curruntPanelColor;
  /// <summary>
  /// 开/关 提交/信息展示页面
  /// </summary>
  public void Button_OpenExitPanel()
  {
    submitPage_Panel.SetActive(!submitPage_Panel.activeSelf);
    showInformationPage_Panel.SetActive(!showInformationPage_Panel.activeSelf);
  }
  /// <summary>
  /// 提交按钮逻辑
  /// </summary>
  public void Button_Submit()
  {
    if (!CheckInformation())
    {
      Debug.Log("提交失败,请完整所有信息后重试");
      return;
    }
    //恢复背景面板的颜色
    nameInput_Panle.GetComponent<Image>().color = curruntPanelColor;
    abilitysInputPanel.GetComponent<Image>().color = curruntPanelColor;
    //根据选择的开关获取对应的文本值
    List<string> bilitys = new List<string>();
    foreach (var item in abilitysInput)
    {
      if (item.isOn)
      {
        bilitys.Add(item.GetComponentInChildren<TextMeshProUGUI>().text);
      }
    }
    //获取开关组中被选中的开关的子物体的文本值
    string gameDifficuty = gameDifficutyInput.ActiveToggles().FirstOrDefault().GetComponentInChildren<TextMeshProUGUI>().text;
    AssignParameters(nameInput.text, professionInput.options[professionInput.value].text, bilitys, gameDifficuty);
    Debug.Log("提交成功!");
    Button_OpenExitPanel();

  }
  /// <summary>
  /// 赋值
  /// </summary>
  /// <param name="name">玩家姓名</param>
  /// <param name="profession">玩家职业</param>
  /// <param name="abilitys">玩家能力列表</param>
  /// <param name="dificutys">游戏难度</param>
  public void AssignParameters(string name, string profession, List<string> abilitys, string dificutys)
  {
    DataManager_Players.Instance.PlayerName = name;
    DataManager_Players.Instance.PlayerProfession = profession;
    DataManager_Players.Instance.playerAbility = new Abilitys[abilitys.Count];
    for (int i = 0; i < abilitys.Count; i++)
    {
      DataManager_Players.Instance.playerAbility[i] = Enum.Parse<Abilitys>(abilitys[i]);
    }
    DataManager_Players.Instance.gameDifficuty = Enum.Parse<Difficutys>(dificutys);
  }
  /// <summary>
  /// 检查信息是否已经填写,若没有填写完整把背景设置为红色
  /// </summary>
  /// <returns>
  /// true - 都填写
  /// fasle - 没有填写完整
  /// </returns>
  public bool CheckInformation()
  {
    bool isComplete = true;
    Debug.Log("文本是" + nameInput.text);
    //如果输入框没有填写就设置为红色
    if (nameInput.text == null || nameInput.text == "")
    {
      Debug.Log("文本是" + nameInput.text);
      nameInput_Panle.GetComponent<Image>().color = Color.red;
      isComplete = false;
    }
    else//否则恢复颜色
    {
      nameInput_Panle.GetComponent<Image>().color = curruntPanelColor;
    }
    //如果有至少一个能力被选中就结束并恢复颜色，否则背景设置为红色
    for (int i = 0; i < abilitysInput.Length; i++)
    {
      if (abilitysInput[i].isOn)
      {
        Debug.Log($"开关{abilitysInput[i].gameObject.name}:{abilitysInput[i].isOn}");
        abilitysInputPanel.GetComponent<Image>().color = curruntPanelColor;
        break;
      }
      if (!abilitysInput[abilitysInput.Length - 1].isOn)
      {
        abilitysInputPanel.GetComponent<Image>().color = Color.red;
        isComplete = false;
      }
    }
    return isComplete;
  }

}