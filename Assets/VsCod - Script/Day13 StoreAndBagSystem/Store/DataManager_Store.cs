using System;
using System.Collections.Generic;
using UnityEngine;
public class DataManager_Store : F_DataManager_StoreBagSystem
{
  private static DataManager_Store instance;
  public static DataManager_Store Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new DataManager_Store();
      }
      return instance;
    }
  }
  public Dictionary<int, DataManager_Products> productsData = new Dictionary<int, DataManager_Products>();  //商品信息存储
  public int productsTotal//商品总数
  {
    get
    {
      return productsData.Count;
    }
  }
}