using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Voice();
/// <summary>
/// 测试和复习委托/事件的知识（Delegate/Event）
/// </summary>
public class DelegateAndEvent : MonoBehaviour
{
    public Voice voice;
    private static event Voice voiceEvent;
    public Func<string, int> func;
    public Action<string> action;

    // Start is called before the first frame update
    void Start()
    {
        AddVoice();
    }
    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="events">需要订阅的方法</param>
    public static void SubscribeVoiceEvent(Voice events)
    {
        voiceEvent += events;
    }
    /// <summary>
    /// 退订事件
    /// </summary>
    /// <param name="events">需要退订的方法</param>
    public static void UnSubscribeVoiceEvent(Voice events)
    {
        voiceEvent -= events;
    }
    /// <summary>
    /// 广播事件
    /// </summary>
    public static void BroadcastVoiceEvent()
    {
        voiceEvent?.Invoke();
    }


    /// <summary>
    /// 添加委托事件
    /// </summary>
    public void AddVoice()
    {
        //添加叫声事件并广播
        voice += CatVoice;
        voice += DogVoice;
        voice.Invoke();

        //移除猫叫事件再次广播
        voice -= CatVoice;
        voice.Invoke();

        //Action事件添加
        //string mesagge = "呱呱叫";
        action += (mesagge) => { Debug.Log(mesagge); };
        action?.Invoke("呱呱呱");

        //Func事件添加
        func += (message) => { Debug.Log(message); return 0; };
        func?.Invoke("哗啦哗啦");
    }
    public void CatVoice()
    {
        Debug.Log("喵喵喵");
    }
    public void DogVoice()
    {
        Debug.Log("汪汪汪");
    }
    public void CarVoice()
    {
        Debug.Log("滴滴滴");
    }
}
