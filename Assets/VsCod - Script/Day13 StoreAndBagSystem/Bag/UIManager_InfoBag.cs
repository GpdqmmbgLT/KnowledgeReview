using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager_InfoBag : F_UIManager_Information
{
  [Header("是否使用页面")]
  public GameObject isUsePage_Panel;
  [Header("挂载的 LogicManager_Bag 脚本")]
  public LogicManager_Bag logicManager_Bag;
  /// <summary>
  /// 方法重写——改变文字显示
  /// </summary>
  /// <param name="id">商品/货物id</param>
  protected override void ChangeText(int id)
  {
    infornation_Text.text = $"<color=red>Product</color> Count: {DataManager_PlayerBag.Instance.PlayerBagData[id].pruductCount}\n\n" +
    $"<color=red>Product</color> Name: {DataManager_PlayerBag.Instance.PlayerBagData[id].productData.ProductName}\n\n" +
    $"<color=red>Pruduct</color> ID: {DataManager_PlayerBag.Instance.PlayerBagData[id].productData.ProductID}\n\n" +
    $"<color=red>Product</color> Price: {DataManager_PlayerBag.Instance.PlayerBagData[id].productData.Price}\n\n" +
    $"<color=red>Product</color> Story: {DataManager_PlayerBag.Instance.PlayerBagData[id].productData.ProductStory}";
  }
  /// <summary>
  /// 方法重新——添加点击事件
  /// </summary>
  /// <param name="id">货物ID</param>
  protected override void BuyButtonEvent(int id)
  {
    logicManager_Bag.GetGoodsElement(id);
    isUsePage_Panel.SetActive(!isUsePage_Panel.activeSelf);
  }
}