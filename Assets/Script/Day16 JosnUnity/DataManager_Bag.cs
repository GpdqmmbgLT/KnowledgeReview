using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BagData
{
    public List<string> itemID;
}
public class DataManager_Bag : MonoBehaviour, ISaveable
{
    static DataManager_Bag instance;
    public static DataManager_Bag Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager_Bag();
            }
            return instance;
        }
    }
    private List<string> _itemID = new List<string>();//物品ID列表
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _itemID.Add("1001");
        _itemID.Add("1002");
        _itemID.Add("1003");
        _itemID.Add("1004");
    }
    /// <summary>
    /// 打印输出本类变量
    /// </summary>
    public void ShowData()
    {
        if (_itemID.Count == 0)
        {
            Debug.Log("_itemID数量为0,停止输出");
            return;
        }

        foreach (var item in _itemID)
        {
            Debug.Log("装备ID:" + item);
        }
    }
    public string SaveData()
    {
        BagData bagData = new BagData();
        bagData.itemID = _itemID;
        return JsonUtility.ToJson(bagData);
    }
    public void Load(string jsonText)
    {
        try
        {
            BagData bagData = JsonUtility.FromJson<BagData>(jsonText);
            _itemID = bagData.itemID;
        }
        catch (Exception e)
        {
            Debug.Log("恢复数据异常!异常数据类型为:BagData" + e);
        }
    }

}
