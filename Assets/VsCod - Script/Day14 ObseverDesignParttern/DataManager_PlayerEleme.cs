using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_PlayerEleme : MonoBehaviour
{
    private float playerBlood;
    public float PlayerBlood
    {
        get
        {
            return playerBlood;
        }
        set
        {
            playerBlood = Mathf.Clamp(value, 0, 100);
            if (playerBlood > 30)
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.bloodMoreThan30);
            }
            else if (playerBlood <= 30 && playerBlood != 0)
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.bloodLessThan30);

            }
            else
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.bloodBecomeZero);
            }
            ObserverEvent.Instance.TriggerEvent(EventStatus.anyStatus);
        }
    }
    private int playerHunger;//玩家饥饿度
    public int PlayerHunger
    {
        get
        {
            return playerHunger;
        }
        set
        {
            playerHunger = Mathf.Clamp(value, 0, 100);
            if (playerHunger > 20)
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.hungerMoreThan20);
            }
            else if (playerHunger <= 20 && playerHunger != 0)
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.hungerLessThan20);

            }
            else
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.hungerBecomeZero);
            }
            ObserverEvent.Instance.TriggerEvent(EventStatus.anyStatus);

        }
    }
    private float playerStrength;//玩家体力值
    public float PlayerStrength
    {
        get
        {
            return playerStrength;
        }
        set
        {
            playerStrength = Mathf.Clamp(value, 0.0f, 100.0f);
            if (playerStrength > 20)
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.strengthMoreThan20);
            }
            else
            {
                ObserverEvent.Instance.TriggerEvent(EventStatus.strengthLessThan20);
            }
            ObserverEvent.Instance.TriggerEvent(EventStatus.anyStatus);
        }
    }
    void Start()
    {
        Init(100, 100, 100);
    }
    /// <summary>
    /// 初始化玩家数据
    /// </summary>
    /// <param name="blood">玩家血量</param>
    /// <param name="hunger">玩家饥饿值</param>
    /// <param name="strength">玩家体力</param>
    public void Init(float blood, int hunger, float strength)
    {
        PlayerBlood = blood;
        PlayerHunger = hunger;
        PlayerStrength = strength;
    }
}
