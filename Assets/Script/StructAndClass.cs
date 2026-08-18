using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 定义结构体，测试不同类型是属于值类型/引用类型
/// </summary>
public struct StructAndClass_Struct
{
    public int num;
    public char chars;
    public string text;
}
public class StructAndClass_Class
{
    public int num;
    public char chars;
    public string text;
}

/// <summary>
/// 测试结构体和类的区别
/// </summary>
public class StructAndClass : MonoBehaviour
{
    public StructAndClass_Struct[] str = new StructAndClass_Struct[2];//声明一个结构体数组，长度为2
    public StructAndClass_Class[] cla = new StructAndClass_Class[2];//声明一个类数组，长度为2
    // Start is called before the first frame update
    void Start()
    {
        str[0].num = 1;
        str[0].text = "222";
        cla[0] = new StructAndClass_Class
        {
            num = 1
        };
        //NumExchangeTest();
        ClassDoParameter(cla[0]);
        StructDoParameter(ref str[0]);
        Debug.Log("结构体.num:" + str[0].num + "    " + "结构体.text:" + str[0].text + "    " + "类.num:" + cla[0].num);
    }
    /// <summary>
    /// 对类和结构体分别做修改，并查看修改后的值是什么情况
    /// </summary>
    public void NumExchangeTest()
    {
        str[0].num = 1;
        str[1] = str[0];
        str[1].num = 2;
        Debug.Log($"结构体1: num = {str[0].num}   结构体2: num = {str[1].num}\n");
        Debug.Log($"结构体1: 地址 = {str[0].GetHashCode()}   结构体2: 地址 = {str[1].GetHashCode()}\n");
        cla[0] = new StructAndClass_Class();
        cla[0].text = "一";
        cla[1] = cla[0];
        cla[1].text = "二";
        Debug.Log($"类1: text = {cla[0].text}   类2: text = {cla[1].text}\n");
        Debug.Log($"类1: 地址 = {cla[0].GetHashCode()}   类2: 地址 = {cla[1].GetHashCode()}\n");
    }

    /// <summary>
    /// 将类作为参数进行传递
    /// </summary>
    /// <param name="cla">类对象</param>
    public void ClassDoParameter(StructAndClass_Class cla)
    {
        cla.num = 2;
    }
    /// <summary>
    /// 将结构体作为参数进行传递
    /// </summary>
    /// <param name="str">结构体实例</param>
    public void StructDoParameter(ref StructAndClass_Struct str)
    {
        str.num = 2;
        str.text = "111";
    }
}
