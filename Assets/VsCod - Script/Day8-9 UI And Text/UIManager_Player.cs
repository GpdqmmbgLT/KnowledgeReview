using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Player : MonoBehaviour
{
    [Header("主页面面板物体")]
    public GameObject mainPanel;
    [Header("玩家信息页面面板物体")]
    public GameObject playerMessagePanel;
    [Header("展示玩家信息的文本")]
    public TextMeshProUGUI playerMessageText;
    string _playerName;//玩家姓名
    const int minLevel = 1, maxLevel = 20;//最大等级和最小等级常量
    int playerLevel;//玩家等级
    public int PlayerLevel
    {
        get
        {
            return playerLevel;
        }
        set
        {
            //返回确定在常量范围内的等级
            playerLevel = Mathf.Clamp(value, minLevel, maxLevel);
        }
    }
    float _playerBloodLimit;//玩家血限
    float playerBlood;//玩家血量
    public float PlayerBlood
    {
        get
        {
            return playerBlood;
        }
        set
        {
            playerBlood = Mathf.Clamp(value, 0, _playerBloodLimit);
        }
    }
    int playerCoin;//玩家金币
    public int PlayerCoin
    {
        get
        {
            return playerCoin;
        }
        set
        {
            playerCoin = value < 0 ? 0 : value;
        }
    }

    void Start()
    {
        Init("XiaoHongMao", 15, 89, 100, 100);
        CheckComponent<GameObject>(mainPanel, playerMessagePanel);
        mainPanel.SetActive(true);
        playerMessagePanel.SetActive(false);
    }
    /// <summary>
    /// 初始化玩家数据
    /// </summary>
    /// <param name="playerName">玩家名称</param>
    /// <param name="playerLevel">玩家等级</param>
    /// <param name="playerBlood">玩家血量</param>
    /// <param name="playerBloodLimit">玩家血量上限</param>
    /// <param name="playerCoin">玩家金币</param>
    public void Init(string playerName, int playerLevel, float playerBlood, float _playerBloodLimit, int playerCoin)
    {
        this._playerBloodLimit = _playerBloodLimit;
        _playerName = playerName;
        PlayerLevel = playerLevel;
        PlayerBlood = playerBlood;
        PlayerCoin = playerCoin;
        playerMessageText.text = RefreshMessage();
    }
    /// <summary>
    /// 打开/关闭 主页面/玩家信息页面
    /// </summary>
    public void Button_StartAndExitPlayerMeaaseg()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);
        playerMessagePanel.SetActive(!playerMessagePanel.activeSelf);
    }
    /// <summary>
    /// 刷新当前玩家信息并返回
    /// </summary>
    /// <returns>刷新后的文本</returns>
    public string RefreshMessage()
    {
        return $"<color=red>PlayerName</color> : {_playerName}\n<color=red>PlayerLevel</color> : {PlayerLevel}\n<color=red>PlayerBlood</color> : {PlayerBlood.ToString("F2")}\n<color=red>PlayerCoin</color> : {PlayerCoin}";
    }
    /// <summary>
    /// 增加玩家血量并刷新UI
    /// </summary>
    public void Button_CreatePlayerBlood()
    {
        PlayerBlood += 2.3f;
        playerMessageText.text = RefreshMessage();
    }
    /// <summary>
    /// 增加玩家等级并刷新UI
    /// </summary>
    public void Button_CreatePlayerLevel()
    {
        PlayerLevel += 1;
        playerMessageText.text = RefreshMessage();
    }
    /// <summary>
    /// 检测组件的赋值情况
    /// </summary>
    /// <typeparam name="T">组件/物体</typeparam>
    /// <param name="components">同种类组件/物体集合</param>
    public void CheckComponent<T>(params T[] components)
    {
        if (components.Length == 0)
        {
            return;
        }
        foreach (var item in components)
        {
            if (item == null)
            {
                Debug.Log($"请注意,{nameof(item)}未赋值!");
            }
        }
    }
}
