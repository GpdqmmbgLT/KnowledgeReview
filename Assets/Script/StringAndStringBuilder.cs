using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 探讨string和stringBuilder的性能对比
/// </summary>
public class StringAndStringBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StringBuilderText();
        StringText();
    }

    /// <summary>
    /// StringBuilder在循环n次的字符串拼接的耗时统计
    /// </summary>
    public void StringBuilderText()
    {
        StringBuilder text = new StringBuilder("");
        DateTime start = DateTime.Now;
        for (int i = 0; i < 10000; i++)
        {
            text.Append(i);
        }
        DateTime finish = DateTime.Now;
        Debug.Log("StringBuilder最终耗时:" + (finish - start));
    }
    /// <summary>
    /// string在循环n次的字符串拼接中耗时统计
    /// </summary>
    public void StringText()
    {
        string text = "";
        DateTime start = DateTime.Now;
        for (int i = 0; i < 10000; i++)
        {
            text += i;
        }
        DateTime finish = DateTime.Now;
        Debug.Log("string最终耗时:" + (finish - start));
    }

}
