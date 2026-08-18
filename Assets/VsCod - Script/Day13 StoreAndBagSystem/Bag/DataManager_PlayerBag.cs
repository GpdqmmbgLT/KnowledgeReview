using System;
using System.Collections.Generic;
using UnityEngine;
public enum BagProductStatu
{
  未存在 = 1,
  重复 = 2,
  新增 = 3
}
public class BagPruductsParameters
{
  public readonly DataManager_Products productData;
  public int pruductCount;
  public GameObject goods;
  /// <summary>
  /// 背包字典value值,初始化需要3个参数
  /// </summary>
  /// <param name="productData">productData对象，商店字典的value</param>
  /// <param name="pruductCount">已拥有产品数量</param>
  /// <param name="goods">货物物体本身</param>
  public BagPruductsParameters(DataManager_Products productData, int pruductCount, GameObject goods)
  {
    this.productData = productData;
    this.pruductCount = pruductCount;
    this.goods = goods;
  }
}
public class DataManager_PlayerBag : F_DataManager_StoreBagSystem
{
  private static DataManager_PlayerBag instance;
  public static DataManager_PlayerBag Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new DataManager_PlayerBag();
      }
      return instance;
    }
  }
  const int maxPlayerCoin = 1000;
  private int playerCoin = 500;//玩家金币数量
  public int PlayerCoin
  {
    get
    {
      return playerCoin;
    }
    set
    {
      playerCoin = Mathf.Clamp(value, 0, maxPlayerCoin);
    }
  }
  //玩家背包字典数据
  private Dictionary<int, BagPruductsParameters> playerBagData = new Dictionary<int, BagPruductsParameters>();
  public Dictionary<int, BagPruductsParameters> PlayerBagData
  {
    get
    {
      return playerBagData;
    }
  }
  public int BagCount
  {
    get
    {
      return playerBagData.Count;
    }
  }
  List<GameObject> activePool = new List<GameObject>(10);//商品活跃对象池
  public List<GameObject> ActivePool
  {
    get
    {
      return activePool;
    }
  }
  public int ActivePool_Count//活跃对象池的数量
  {
    get
    {
      return ActivePool.Count;
    }
  }
  GameObjectPool_PlayerBag coolPool;//商品沉静对象池,在UIManager_PlayerBag中初始化
  public GameObjectPool_PlayerBag CoolPool
  {
    get
    {
      return coolPool;
    }
    set
    {
      if (coolPool == null)
      {
        coolPool = value;
      }
    }
  }
  public int CoolPool_Count//活跃对象池的数量
  {
    get
    {
      return CoolPool.Count;
    }
  }
  /// <summary>
  /// 重置所有元素的位置
  /// </summary>
  public void ResetAllPO()
  {
    int num = 0;
    foreach (var item in ActivePool)
    {
      num++;
      item.GetComponent<RectTransform>().anchoredPosition = GetPo(num);
    }
  }
  /// <summary>
  /// 方法重写——获取当前货物的坐标(由于本脚本逻辑为先添加到字典再获取位置，因此坐标判断依据要加一，所以该函数获取的位置是当前货物的位置，不是下一个的位置)
  /// </summary>
  /// <param name="bagCount">背包数量</param>
  /// <returns></returns>
  public override Vector2 GetPo(int bagCount)
  {
    //根据当前背包数量直接计算坐标
    int num = bagCount % 4;
    //如果bagCount%4为0，达到x最大横坐标,直接自增3被增量。其他情况下在原有基数的基础上增加bagCount / 4倍数的X间隔
    // y坐标在bagCount%4为0时候会执行换行计算，因此此时不进行y的判断，在其他情况进行计算（因为此计算方式下，上一行的末尾与下一行的前三个是同一个Y坐标）
    if (num == 0)
    {
      po_X = firstPo.x + gap_x * 3;
    }
    else
    {
      po_X = firstPo.x + gap_x * (num - 1);
      po_Y = firstPo.y - gap_y * (bagCount / 4);
    }
    ChangeContentHeight();
    return new Vector2(po_X, po_Y);
  }
  /// <summary>
  /// 改变滚动视图的高度
  /// </summary>
  public void ChangeContentHeight()
  {
    //直接根据当前背包数量计算滚动视图的高度
    CurruntcontentIncrement = (BagCount / 4 + 1) * contentIncrement;
  }
  /// <summary>
  /// 添加产品进入到字典背包
  /// </summary>
  /// <param name="pruductID">货物ID</param>
  /// <param name="goods">货物本身</param>
  /// <returns></returns>
  public BagProductStatu AddBagData(int pruductID, GameObject goods)
  {
    //如果商店未包含该key，则返回
    if (!DataManager_Store.Instance.productsData.ContainsKey(pruductID))
    {
      Debug.Log("未查询到该ID的商品!商品ID:" + pruductID);
      return BagProductStatu.未存在;
    }
    //如果背包字典已经包含该key，则直接自增所拥有数量
    if (PlayerBagData.ContainsKey(pruductID))
    {
      PlayerBagData[pruductID].pruductCount += 1; PlayerBagData[pruductID].productData.ProductName = "";
      Debug.Log("key已存在,所拥有数量自增");
      return BagProductStatu.重复;
    }
    //如果该商品第一次进入字典，则新建key-value
    else
    {
      PlayerBagData.Add(pruductID, new BagPruductsParameters(DataManager_Store.Instance.productsData[pruductID], 1, goods));
      return BagProductStatu.新增;
    }
  }
}