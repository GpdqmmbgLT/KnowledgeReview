using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_PlayerBag : MonoBehaviour
{
    [Header("产品预制体")]
    public GameObject productPrefab;
    [Header("背包货物的父物体")]
    public GameObject goodsFather;
    RectTransform goodsFather_Rect;//背包货物的父物体的rect组件
    [Header("背包物品详情页面面板物体")]
    public GameObject bagGoodsInformation_Panel;
    [Header("背包面板物体")]
    public GameObject bagPage_Panel;
    [Header("商店面板物体")]
    public GameObject storePage_Panel;
    void Start()
    {
        DataManager_PlayerBag.Instance.CoolPool = GetComponent<GameObjectPool_PlayerBag>();
        goodsFather_Rect = goodsFather.GetComponent<RectTransform>();
    }
    /// <summary>
    /// 按钮-打开/关闭 商店/背包物体
    /// </summary>
    public void Button_OpenExitBagPage()
    {
        bagPage_Panel.SetActive(!bagPage_Panel.activeSelf);
        storePage_Panel.SetActive(!storePage_Panel.activeSelf);
    }
    /// <summary>
    /// 创建新的货物按钮物体
    /// </summary>
    public void All_CreateBagProduct(int goodsID)
    {
        if (DataManager_PlayerBag.Instance.AddBagData(goodsID, new GameObject()) == BagProductStatu.新增)
        {
            GameObject goods = CreateBagProduct();
            //由于没有提前把goods物体存入，临时该逻辑只能这样赋值
            DataManager_PlayerBag.Instance.PlayerBagData[goodsID].goods = goods;
            BagProductSetting(goods, goodsID);
            goodsFather_Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DataManager_PlayerBag.Instance.CurruntcontentIncrement);
        }
    }
    /// <summary>
    /// 实例化一个新的货物
    /// </summary>
    /// <returns>新货物的GameObject</returns>
    public GameObject CreateBagProduct()
    {
        GameObject goods = DataManager_PlayerBag.Instance.CoolPool.GetObj(productPrefab, goodsFather.transform);//实例化一个物体
        DataManager_PlayerBag.Instance.ActivePool.Add(goods);//添加到活跃对象池中
        return goods;
    }
    /// <summary>
    /// 对货物的属性进行设置（位置，文本显示，点击事件）
    /// </summary>
    /// <param name="goods">货物</param>
    /// <param name="productID">货物ID</param>
    public void BagProductSetting(GameObject goods, int productID)
    {
        SetPositon(goods);
        RefreshText(goods, productID);
        SetEvent(goods, productID);
    }
    /// <summary>
    /// 设置货物的位置
    /// </summary>
    /// <param name="goods"></param>
    public void SetPositon(GameObject goods)
    {
        //获取预制体身上的rect并且更新位置
        RectTransform rect = goods.GetComponent<RectTransform>();
        rect.anchoredPosition = DataManager_PlayerBag.Instance.GetPo(DataManager_PlayerBag.Instance.BagCount);
    }
    /// <summary>
    /// 刷新货物的文本显示
    /// </summary>
    /// <param name="goods">货物</param>
    /// <param name="productID">货物ID</param>
    public void RefreshText(GameObject goods, int productID)
    {
        //获取预制体身上的Text并且更新按钮外显文本
        TextMeshProUGUI text = goods.GetComponentInChildren<TextMeshProUGUI>();
        text.text = DataManager_PlayerBag.Instance.PlayerBagData[productID].productData.ProductName;
    }
    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    /// <param name="goods">货物</param>
    /// <param name="id">货物id</param>
    public void SetEvent(GameObject goods, int id)
    {
        //获取预制体身上的Button并且更新按钮事件
        Button button = goods.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { OnclickEvent(id); });
    }
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="id"></param>
    public void OnclickEvent(int id)
    {
        //打开详情页面并且传递ID参数
        UIManager_InfoBag.GetIDFunc += () => { return id; };
        bagGoodsInformation_Panel.SetActive(!bagGoodsInformation_Panel.activeSelf);
    }

}
