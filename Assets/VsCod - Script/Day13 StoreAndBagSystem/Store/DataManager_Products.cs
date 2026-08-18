using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class DataManager_Products : MonoBehaviour
{
    public const int minPrice = 1;
    public const int maxPrice = 100;

    string productName;
    public string ProductName { get => productName; set => productName = value; }

    private int productId;
    public int ProductID
    {
        get
        {
            return productId;
        }
        set
        {
            if (value > 0)
            {
                productId = value;
            }
        }
    }
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = Mathf.Clamp(value, minPrice, maxPrice);
        }
    }

    string productStory;
    public string ProductStory { get => productStory; set => productStory = value; }

    /// <summary>
    /// 初始化商品数据
    /// </summary>
    /// <param name="name">商品名称</param>
    /// <param name="id">商品ID</param>
    /// <param name="price">商品价格</param>
    /// <param name="story">商品故事</param>
    public void Init(string name, int price, int id, string story)
    {
        ProductName = name;
        ProductID = id;
        Price = price;
        productStory = story;
    }
}
