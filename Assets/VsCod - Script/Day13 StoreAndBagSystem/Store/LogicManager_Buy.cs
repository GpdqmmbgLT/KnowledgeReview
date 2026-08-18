using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicManager_Buy : MonoBehaviour
{
    [Header("是否购买物体面板")]
    public GameObject isBuy_Panel;
    [Header("购买反馈物体面板")]
    public GameObject buyFeedback_Panel;
    [Header("购买反馈的文本组件")]
    public TextMeshProUGUI buyFeedback_Text;
    [Header("玩家的资金显示文本组件")]
    public TextMeshProUGUI playerCoin_Text;
    [Header("背包的UI管理脚本")]
    public UIManager_PlayerBag uIManager_PlayerBag;
    public static event Func<int> AddproductIDFunc;//func,接受物品id
    void Start()
    {
        RefeshCoin();
    }
    void OnDisable()
    {//清空事件
        AddproductIDFunc = null;
    }
    /// <summary>
    /// 激活/退出购买物体面板
    /// </summary>
    public void Button_OpenExitISBuyPage()
    {
        isBuy_Panel.SetActive(!isBuy_Panel.activeSelf);
    }
    /// <summary>
    /// 激活/退出购买反馈页面
    /// </summary>
    public void Button_ExitBuyFeedbackPage()
    {
        buyFeedback_Panel.SetActive(!buyFeedback_Panel.activeSelf);
    }
    /// <summary>
    /// 判断玩家资金当前是否能够购买
    /// </summary>
    /// <param name="playerCoin">玩家金币</param>
    /// <param name="productPrice">产品价格</param>
    /// <param name="feedbackText">反馈文字</param>
    /// <returns></returns>
    public bool CanBuyProduct(ref int playerCoin, int productPrice, out string feedbackText)
    {
        string text;//反馈文本
        //如果玩家资金充足，则提示购买成功，扣除对应金额
        if (playerCoin >= productPrice)
        {
            text = "Congradulation!Buy Successful";
            playerCoin -= productPrice;
            Debug.Log(text);
            feedbackText = text;
            return true;
        }
        //如果不足，则提示某购买失败
        else
        {
            text = "Sorry your coin do not enough!";
            Debug.Log(text);
            feedbackText = text;
            return false;
        }
    }
    /// <summary>
    /// 刷新资金显示
    /// </summary>
    public void RefeshCoin()
    {
        playerCoin_Text.text = "Coin:" + DataManager_PlayerBag.Instance.PlayerCoin;
    }
    public void Button_BuyProduct()
    {
        //如果func为空则返回
        if (AddproductIDFunc == null)
        {
            Debug.Log("没有收到id");
            return;
        }
        //如果id未找到则返回
        int id = AddproductIDFunc.Invoke();
        if (!DataManager_Store.Instance.productsData.ContainsKey(id))
        {
            Debug.Log("没有找到该id");
            return;
        }
        //获取价格与文本组件的Text，进行判断
        int price = DataManager_Store.Instance.productsData[id].Price;
        int refPlayerCoin = DataManager_PlayerBag.Instance.PlayerCoin;
        CanBuyProduct(ref refPlayerCoin, price, out string text);
        DataManager_PlayerBag.Instance.PlayerCoin = refPlayerCoin;
        buyFeedback_Text.text = text;
        RefeshCoin();
        Button_ExitBuyFeedbackPage();
        Button_OpenExitISBuyPage();
        //传入背包UI逻辑脚本中，创建新的预制体实例
        uIManager_PlayerBag.All_CreateBagProduct(id);
    }


}
