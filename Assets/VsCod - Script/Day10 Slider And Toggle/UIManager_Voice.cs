using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class UIManager_Voice : MonoBehaviour
{
  [Header("主页面面板物体")]
  public GameObject mianPage_Panel;
  [Header("设置界面面板物体")]
  public GameObject settingPage_Panel;
  [Header("音量显示百分比文本")]
  public TextMeshProUGUI voiceNum_Text;
  [Header("调节音量滑块")]
  public Slider voiceSetting_Slider;
  [Header("是否开启音量开关")]
  public Toggle isOpenVoice_Toggel;
  public AudioSource audioSource;
  void Start()
  {
    DataManager_Voice.Instance.Init(true, 0.5f);
    mianPage_Panel.SetActive(true);
    settingPage_Panel.SetActive(false);
    voiceSetting_Slider.value = DataManager_Voice.Instance.CurruntVoice;
    isOpenVoice_Toggel.isOn = DataManager_Voice.Instance.IsOpenVoice;
    voiceNum_Text.text = (int)(DataManager_Voice.Instance.CurruntVoice * 100) + "%";
    RefreshVoice();
    audioSource.Play();
  }
  /// <summary>
  /// 打开/关闭设置界面
  /// </summary>
  public void Button_OpenExitSetting()
  {
    mianPage_Panel.SetActive(!mianPage_Panel.activeSelf);
    settingPage_Panel.SetActive(!settingPage_Panel.activeSelf);
  }
  /// <summary>
  /// 玩家自主调节音量
  /// </summary>
  /// <param name="value">改变后的滑块值</param>
  public void Slider_SettingVoice(float value)
  {
    DataManager_Voice.Instance.CurruntVoice = value;
    RefreshVoice();
  }
  /// <summary>
  /// 是否开启音量
  /// </summary>
  /// <param name="value"></param>
  public void Toggel_IsOpenVoice(bool value)
  {
    DataManager_Voice.Instance.IsOpenVoice = value;
    RefreshVoice();
  }
  /// <summary>
  /// 重置音量
  /// </summary>
  public void Button_ResetVoice()
  {
    DataManager_Voice.Instance.CurruntVoice = DataManager_Voice.maxVoice;//重置为最大音量
    isOpenVoice_Toggel.isOn = true;//重置为开启音量
    voiceSetting_Slider.value = DataManager_Voice.Instance.CurruntVoice;
    isOpenVoice_Toggel.isOn = DataManager_Voice.Instance.IsOpenVoice;
    RefreshVoice();
  }
  /// <summary>
  /// 刷新音量与显示
  /// </summary>
  public void RefreshVoice()
  {
    audioSource.volume = DataManager_Voice.Instance.AllVoice;
    voiceNum_Text.text = (int)(DataManager_Voice.Instance.CurruntVoice * 100) + "%";
  }
}