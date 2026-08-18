using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilitys
{
    Eat = 1,
    Watch = 2,
    Sleep = 3
}
public enum Difficutys
{
    Simple = 1,
    Midle = 2,
    Difficult = 3
}
public class DataManager_Players
{
    private static DataManager_Players instance;
    public static DataManager_Players Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager_Players();
            }
            return instance;
        }
    }
    private string playerName;//玩家名称
    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            if (value != null && value != "")
            {
                playerName = value;
            }
            else
            {
                playerName = "DefultName";
            }
        }
    }
    private string playerProsession;//玩家职业
    public string PlayerProfession
    {
        get => playerProsession;
        set
        {
            if (value != null && value != "")
            {
                playerProsession = value;
            }
            else
            {
                playerProsession = "DefultProfession";
            }
        }
    }
    public Abilitys[] playerAbility;//玩家能力
    public Difficutys gameDifficuty;//游戏难度
}
