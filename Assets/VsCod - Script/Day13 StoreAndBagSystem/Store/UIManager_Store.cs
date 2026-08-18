using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInputParameters
{
  public string name;
  public int id;
  public int price;
  public string story;
  /// <summary>
  /// 初始化需要4个参数
  /// </summary>
  /// <param name="name">商品名称</param>
  /// <param name="price">商品价格</param>
  /// <param name="id">商品ID</param>
  /// <param name="story">商品故事</param>
  public PlayerInputParameters(string name, int price, int id, string story)
  {
    this.name = name;
    this.id = id;
    this.price = price;
    this.story = story;
  }
}
public class UIManager_Store : MonoBehaviour
{
  [Header("商品详情面板物体")]
  public GameObject productInformation_Panel;
  [Header("商品页面物体")]
  public GameObject storePage_Panel;
  [Header("初始化产品信息页面")]
  public GameObject initProduct_Panel;
  [Header("商品预制体")]
  public GameObject product_Prefab;
  [Header("商品实例化后存放的父物体")]
  public GameObject productsFather;
  [Header("滚动条的rect属性")]
  RectTransform contentRect;
  [Header("输入框组件")]
  public TMP_InputField[] inputs;
  [Header("输入框本物体的父级面板")]
  public GameObject[] fatherObjs;
  [Header("键值对 输入组件-父级面板")]
  public Dictionary<TMP_InputField, GameObject> inputFiled_FatherPanel = new Dictionary<TMP_InputField, GameObject>();
  [Header("默认的输入框面板颜色")]
  public Color32 defultPanelColor;
  void Start()
  {
    contentRect = productsFather.GetComponent<RectTransform>();
    AddInputFiled_FatherPanel();
    //Text(2);
  }

  public void Text(int num)
  {
    int[] nums = { 1, 2, 3, 4, 5 };
    string numms = "";
    foreach (var item in nums)
    {
      numms += item + " ";
    }
    Debug.Log("改变前:" + numms);
    int temp = 0;
    for (int i = num; i < nums.Length - 1; i++)
    {
      temp = nums[i];
      nums[i] = nums[i + 1];
      nums[i + 1] = temp;
    }
    numms = "";
    foreach (var item in nums)
    {
      numms += item + " ";
    }
    Debug.Log("改变后:" + numms);
  }
  /// <summary>
  /// 将传入的输入框和面板父物体添加到字典中
  /// </summary>
  public void AddInputFiled_FatherPanel()
  {
    for (int i = 0; i < 4; i++)
    {
      inputFiled_FatherPanel.Add(inputs[i], fatherObjs[i]);
    }
  }
  /// <summary>
  /// 打开/关闭 创建页面/商品页面
  /// </summary>
  public void Button_OpenExitPage()
  {
    storePage_Panel.SetActive(!storePage_Panel.activeSelf);
    initProduct_Panel.SetActive(!initProduct_Panel.activeSelf);
  }

  /// <summary>
  /// 对玩家的输入内容进行判断
  /// </summary>
  public bool InputJudge()
  {
    if (inputFiled_FatherPanel.Count != 4)
    {
      Debug.Log("存入输入字典数据不为4,请检查 当前数量为" + inputFiled_FatherPanel.Count);
      return false;
    }
    bool inputIsNull = true;//标记本次检测是否成功通过            
    foreach (var item in inputFiled_FatherPanel.Keys) //对输入内容进行遍历
    {
      //每次遍历前先恢复原本的背景颜色
      inputFiled_FatherPanel[item].GetComponent<UnityEngine.UI.Image>().color = defultPanelColor;
      //遍历字典，如果某一项输入内容为空便把背景置为红色，bool状态标记为false
      if (item.text == null || item.text == "")
      {
        Debug.Log("输入内容为空!定位:" + item.gameObject.name);
        inputFiled_FatherPanel[item].GetComponent<UnityEngine.UI.Image>().color = Color.red;
        inputIsNull = false;
      }
    }
    return inputIsNull;
  }
  /// <summary>
  /// 对玩家输入的数据进行赋值并返回判断
  /// </summary>
  /// <param name="data">需要一个PlayerInputParameters参数</param>
  public bool AssignData(out PlayerInputParameters data)
  {
    try
    {
      List<string> datas = new List<string>(inputFiled_FatherPanel.Count);//定义存储列表
      int count = 0;//计数用于统计遍历到字典的多少项
      foreach (var item in inputFiled_FatherPanel.Keys)
      {
        count += 1;
        //如果进行到第三项，且已经包含keys则变红背景框结束循环
        if (count == 3 && DataManager_Store.Instance.productsData.ContainsKey(int.Parse(item.text)))
        {
          Debug.Log("ID重复!请重新输入");
          inputFiled_FatherPanel[item].GetComponent<UnityEngine.UI.Image>().color = Color.red;
          data = null;
          return false;
        }
        datas.Add(item.text);
      }
      data = new PlayerInputParameters(datas[0], int.Parse(datas[1]), int.Parse(datas[2]), datas[3]);
      return true;
    }
    catch (Exception e)
    {
      data = null;
      Debug.Log("赋值失败!请检查'价格'与'ID'是否为整形数字\n" + e);
      return false;
    }
  }
  /// <summary>
  /// 创建商品实例并初始化数据
  /// </summary>
  /// <param name="datas">需要一个PlayerInputParameters参数</param>
  public void CreateProduct(PlayerInputParameters datas)
  {
    //实例化一个产品
    GameObject product = Instantiate(product_Prefab, productsFather.transform);
    //获取实例化后的位置并判断滚动条高度
    product.GetComponent<RectTransform>().anchoredPosition = DataManager_Store.Instance.GetPo(DataManager_Store.Instance.productsTotal);
    contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DataManager_Store.Instance.CurruntcontentIncrement);
    //获取产品身上的脚本组件
    DataManager_Products data = product.GetComponent<DataManager_Products>();
    UIManager_Products ui = product.GetComponent<UIManager_Products>();
    //改变商品的展示名称
    product.GetComponentInChildren<TextMeshProUGUI>().text = datas.name;
    //初始化产品数据
    data.Init(datas.name, datas.price, datas.id, datas.story);
    //存入商店字典中
    DataManager_Products productData;
    int id = ui.ProductsData(out productData);
    if (productData != null && id != 0)
    {
      DataManager_Store.Instance.productsData.Add(id, productData);
      ui.AddOnclickEvent(() => OnclickEvent(datas.id));
    }
    else
    {
      Debug.Log("没有存入商店");
    }
  }
  /// <summary>
  /// 给商品添加点击事件
  /// </summary>
  /// <param name="productID">商品ID</param>
  public void OnclickEvent(int productID)
  {
    UIManager_InfoStore.GetIDFunc += () => { return productID; };
    productInformation_Panel.SetActive(true);//打开商品详情页面
  }
  /// <summary>
  /// 创建商品实例全流程（输入-判断-返回-实例化-初始化）
  /// </summary>
  public void Button_CreateProdct()
  {
    PlayerInputParameters data;
    //如果失败则直接返回，不会实例化
    if (!InputJudge() || !AssignData(out data))
    {
      Debug.Log("创建失败!");
      return;
    }
    CreateProduct(data);
    Debug.Log("创建成功!");
    ShowStoreMessage();
    Button_OpenExitPage();
  }
  /// <summary>
  /// 遍历输出商店字典已经存储的数据
  /// </summary>
  public void ShowStoreMessage()
  {
    foreach (var item in DataManager_Store.Instance.productsData.Keys)
    {
      Debug.Log
      (
        $"name:{DataManager_Store.Instance.productsData[item].ProductName}\t" +
        $"ID:{DataManager_Store.Instance.productsData[item].ProductID}\t" +
        $"Price:{DataManager_Store.Instance.productsData[item].Price}\t" +
        $"Story:{DataManager_Store.Instance.productsData[item].ProductStory}"
      );
    }
  }
}