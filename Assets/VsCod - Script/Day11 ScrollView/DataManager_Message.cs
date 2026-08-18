using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MessagePool))]
public class DataManager_Message : MonoBehaviour
{
    public static DataManager_Message Instance;
    public MessagePool ObjectPool;//对象池，管理对象
    public Queue<GameObject> messagePool = new Queue<GameObject>(50);//消息池，管理当前显示的信息
    private int messageNum = 1;//当前最大的消息编号
    public int MessageNum
    {
        get => messageNum;
        set => messageNum = value;
    }
    public const int maxPoolCount = 50;//最大池容量
    public float nextMessagePo_X = 502;//下一个消息的X坐标
    private float nextMessagePo_Y = -60;//下一个消息的Y坐标
    public float NextMessagePo_Y
    {
        get => nextMessagePo_Y;
        set => nextMessagePo_Y = value;
    }
    void Awake()
    {
        Instance = this;
        ObjectPool = GetComponent<MessagePool>();
    }
    /// <summary>
    /// 更新下一条新消息的属性
    /// </summary>
    public void UpdateMessageAttribute()
    {
        NextMessagePo_Y -= 110;
        MessageNum += 1;
    }
    /// <summary>
    /// 重置消息属性
    /// </summary>
    public void ResetMessageAttribute()
    {
        NextMessagePo_Y = -60;
        MessageNum = 1;
    }
    public void RankMessage()
    {
        foreach (var item in messagePool)
        {
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(502, rect.anchoredPosition.y + 110);//把所有消息列表的y向前移动
        }
        NextMessagePo_Y += 110;//下一条新消息的位置往前移动一个位置
    }
}
