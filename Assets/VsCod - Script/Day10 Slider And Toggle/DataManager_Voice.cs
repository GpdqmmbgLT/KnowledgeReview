using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_Voice
{
    private static DataManager_Voice _instance;
    public static DataManager_Voice Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager_Voice();
            }
            return _instance;
        }
    }
    private float allVoice;//总音量
    public float AllVoice
    {
        get
        {
            allVoice = Mathf.Clamp(voiceNum * CurruntVoice, minVoice, maxVoice);
            return allVoice;
        }
    }
    private int voiceNum;//音量系数，只能为0/1，受到 isOpenVoice 控制
    private bool isOpenVoice;//是否开启音量
    public bool IsOpenVoice
    {
        get
        {
            return isOpenVoice;
        }
        set
        {
            //如果开启音量，系数为1.否则为0
            if (value)
            {
                voiceNum = 1;
            }
            else
            {
                voiceNum = 0;
            }
            isOpenVoice = value;
        }
    }
    public const float minVoice = 0, maxVoice = 1;//最小/大音量
    private float curruntVoice;//当前音量值
    public float CurruntVoice
    {
        get
        {
            return curruntVoice;
        }
        set
        {
            curruntVoice = Mathf.Clamp(value, minVoice, maxVoice);
        }
    }
    /// <summary>
    /// 初始化音量数据
    /// </summary>
    /// <param name="defaultIsOpenVoice">默认是否开启音量</param>
    /// <param name="defaultVoice">默认音量值</param>
    public void Init(bool defaultIsOpenVoice, float defaultVoice)
    {
        IsOpenVoice = defaultIsOpenVoice;
        curruntVoice = defaultVoice;
    }
}
