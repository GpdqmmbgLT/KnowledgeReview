using System;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class LogicManager_Bag : MonoBehaviour
{
  [Header("详细信息页面物体")]
  public GameObject InformationPage_Panel;
  [Header("是否使用货物页面物体")]
  public GameObject isUsePage_Panel;
  [Header("使用信息反馈页面物体")]
  public GameObject useInformationPage_Panel;
  [Header("详细信息页面文本组件")]
  public TextMeshProUGUI text;
  [Header("货物的父物体")]
  public RectTransform bagFather_Rect;
  int goodsID = 0;//货物的id
  GameObject goods = null;//被选中的货物
  void OnDisable()
  {
    goodsID = 0;//重置货物ID
    goods = null;//重置货物
  }
  /// <summary>
  /// 获取货物的id并货物货物本身
  /// </summary>
  /// <param name="id">货物ID</param>
  public void GetGoodsElement(int id)
  {
    goodsID = id;
    goods = DataManager_PlayerBag.Instance.PlayerBagData[id].goods;
  }
  /// <summary>
  /// 关闭详细信息页面
  /// </summary>
  public void Button_ExitInfoPage()
  {
    InformationPage_Panel.SetActive(false);
  }
  /// <summary>
  /// 打开/关闭是否使用页面(按钮：使用，返回)
  /// </summary>
  public void Button_OpenExitIsUsePanel()
  {
    isUsePage_Panel.SetActive(!isUsePage_Panel.activeSelf);
  }
  /// <summary>
  /// 打开/关闭详情信息页面（按钮：确认，方法：UseLogic()）
  /// </summary>
  public void Button_OpenExitInfoPage()
  {
    useInformationPage_Panel.SetActive(!useInformationPage_Panel.activeSelf);
  }
  /// <summary>
  /// 使用的相关逻辑（按钮：使用）
  /// </summary>
  public void Button_Use()
  {
    if (goodsID == 0 || goods == null)
    {
      Debug.Log("Error!未接收到ID参数/货物物体,请检查赋值顺序以及逻辑!");
    }
    if (!DataManager_PlayerBag.Instance.PlayerBagData.ContainsKey(goodsID))
    {
      Debug.Log("Error!未查询到该商品ID!");
      return;
    }
    UseLogic(DataManager_PlayerBag.Instance.PlayerBagData[goodsID], goodsID);
  }
  /// <summary>
  /// 使用的相关逻辑
  /// </summary>
  /// <param name="data">BagPruductsParameters</param>
  /// <param name="id">货物ID</param>
  public void UseLogic(BagPruductsParameters data, int id)
  {
    int count = data.pruductCount;
    string useInfo = "";
    //如果剩余数量大于1，则直接扣除对应数量
    if (count > 1)
    {
      useInfo = "Succcsesful Use:" + data.productData.ProductName + "\nHave Count:" + count;
    }
    //如果剩余数量等于1，在扣除数量之后回收进对象池，且从背包数据中移除后更新其余货物位置并刷新视图高度
    else if (count == 1)
    {
      useInfo = $"<color=red>{data.productData.ProductName}</color>:" + "Have Finish";
      //交换激活元素的位置（类似于冒泡排序）
      // 这个是错的，没有考虑到在列表中的顺序没有改变，因此总是会变成相邻的元素交换位置。
      /* Vector2 temp = new Vector2(0, 0);
      Debug.Log(DataManager_PlayerBag.Instance.ActivePool.IndexOf(goods) + " " + DataManager_PlayerBag.Instance.ActivePool.Count);
      for (int i = DataManager_PlayerBag.Instance.ActivePool.IndexOf(goods); i < DataManager_PlayerBag.Instance.ActivePool.Count - 1; i++)
      {
        temp = DataManager_PlayerBag.Instance.ActivePool[i].GetComponent<RectTransform>().anchoredPosition;
        DataManager_PlayerBag.Instance.ActivePool[i].GetComponent<RectTransform>().anchoredPosition = DataManager_PlayerBag.Instance.ActivePool[i + 1].GetComponent<RectTransform>().anchoredPosition;
        DataManager_PlayerBag.Instance.ActivePool[i + 1].GetComponent<RectTransform>().anchoredPosition = temp;
      } */

      //在背包字典中移除，并判断Content高度是否改变
      DataManager_PlayerBag.Instance.PlayerBagData.Remove(id);
      //活跃对象池移除并回收进沉静对象池
      DataManager_PlayerBag.Instance.ActivePool.Remove(goods);
      DataManager_PlayerBag.Instance.CoolPool.RecycleObj(goods);
      //更新所有元素的位置，重新判断Content高度，关闭信息详情页面
      DataManager_PlayerBag.Instance.ResetAllPO();
      bagFather_Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DataManager_PlayerBag.Instance.CurruntcontentIncrement);
      InformationPage_Panel.SetActive(false);
    }
    //数量自减，改变文本显示并且激活使用反馈面板
    data.pruductCount--;
    Debug.Log(useInfo);
    text.text = useInfo;
    Button_OpenExitIsUsePanel();
    Button_OpenExitInfoPage();

  }
}