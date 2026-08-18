using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class F_UIManager_Information : MonoBehaviour
{
  [Header("商品详情页面的根面板")]
  public GameObject information_Panel;
  [Header("详细信息的文本组件")]
  public TextMeshProUGUI infornation_Text;
  [Header("购买/使用按钮")]
  public Button buyUse_Button;
  [Header("是否购买/使用物体面板")]
  public GameObject isBuyUse_Panel;
  public static event Func<int> GetIDFunc;//获取产品信息id的委托
  protected void OnEnable()
  {
    //改变文字显示
    if (GetIDFunc != null)
    {
      int id = GetIDFunc.Invoke();
      AddEvent(id);
      ChangeText(id);
    }
    else
    {
      Debug.Log("没有进入");
    }
  }
  /// <summary>
  /// 传入委托事件
  /// </summary>
  /// <param name="id">货物ID</param>
  public void AddEvent(int id)
  {
    //注册事件
    buyUse_Button.onClick.RemoveAllListeners();
    buyUse_Button.onClick.AddListener(() => { BuyButtonEvent(id); });
  }
  /// <summary>
  /// 改变文字显示
  /// </summary>
  /// <param name="id">商品/货物id</param>
  protected virtual void ChangeText(int id)
  {
    infornation_Text.text = $"<color=red>Product Name</color>: {DataManager_Store.Instance.productsData[id].ProductName}\n\n" +
    $"<color=red>Pruduct ID</color>: {DataManager_Store.Instance.productsData[id].ProductID}\n\n" +
    $"<color=red>Product Price</color>: {DataManager_Store.Instance.productsData[id].Price}\n\n" +
    $"<color=red>Product Story</color>:{DataManager_Store.Instance.productsData[id].ProductStory}";
  }
  protected void OnDisable()
  {
    //清空事件与文本
    GetIDFunc = null;
    infornation_Text.text = null;
  }
  /// <summary>
  /// 关闭信息页面
  /// </summary>
  public void Button_ExitInformation_Panel()
  {
    information_Panel.SetActive(false);
  }
  /// <summary>
  /// 购买按钮的注册事件(打开面板，注册id)
  /// </summary>
  /// <param name="id">当前产品id</param>
  protected virtual void BuyButtonEvent(int id)
  {
    isBuyUse_Panel.SetActive(true);
    LogicManager_Buy.AddproductIDFunc += () => { return id; };
  }

}