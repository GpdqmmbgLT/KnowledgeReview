using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class PlayerData
{
    public Vector3 position;//玩家位置
    public float blood;//玩家血量
    public int coin;//玩家金币 

}
public class DataManager_Player : MonoBehaviour, ISaveable
{
    static DataManager_Player instance;
    public static DataManager_Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager_Player();
            }
            return instance;
        }
    }
    private Vector3 _position;//玩家位置
    private float _blood;//玩家血量
    private int _coin;//玩家金币
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _position = Vector3.forward;
        _blood = 50.6f;
        _coin = 100;
    }
    /// <summary>
    /// 打印输出本类变量
    /// </summary>
    public void ShowData()
    {
        Debug.Log("玩家位置:" + _position);
        Debug.Log("玩家血量:" + _blood);
        Debug.Log("玩家金币:" + _coin);
    }
    public string SaveData()
    {
        PlayerData PlayerData = new PlayerData();
        PlayerData.position = _position;
        PlayerData.blood = _blood;
        PlayerData.coin = _coin;
        return JsonUtility.ToJson(PlayerData);
    }
    public void Load(string jsonText)
    {
        try
        {
            PlayerData PlayerData = JsonUtility.FromJson<PlayerData>(jsonText);
            _position = PlayerData.position;
            _blood = PlayerData.blood;
            _coin = PlayerData.coin;
        }
        catch (Exception e)
        {
            Debug.Log("恢复数据异常!异常数据类型为:PlayerData\n" + e);
        }
    }

}
