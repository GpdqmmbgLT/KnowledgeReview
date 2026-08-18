using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 静态类，处理集合并返回对应数据
/// </summary>
public static class DataStatistics
{
    /// <summary>
    /// 获取集合中所有数字之和
    /// </summary>
    /// <param name="numbers">集合序列</param>
    /// <returns>该序列中所有数字只和</returns>
    public static int GetSum(IEnumerable<int> numbers)
    {
        int sum = 0;
        foreach (var item in numbers)
        {
            sum += item;
        }
        return sum;
    }
    /// <summary>
    /// 获取集合中的平均值(两位小数）
    /// </summary>
    /// <param name="numbers">集合序列</param>
    /// <returns>该序列中的平均值(两位小数）</returns>
    public static float GetAverage(IEnumerable<int> numbers)
    {
        int sum = 0;
        foreach (var item in numbers)
        {
            sum += item;
        }
        return MathF.Round(sum / numbers.Count(), 2);
    }
    /// <summary>
    /// 获取集合中的最大值
    /// </summary>
    /// <param name="numbers">集合序列</param>
    /// <returns>该序列中的最大值</returns>
    public static int GetMax(IEnumerable<int> numbers)
    {
        int max = 0;
        bool status = true;
        foreach (var item in numbers)
        {
            if (status)
            {
                max = item;
                status = !status;
            }
            if (max < item)
            {
                max = item;
            }
        }
        return max;
    }
    /// <summary>
    /// 获取集合中的最小值
    /// </summary>
    /// <param name="numbers">集合序列</param>
    /// <returns>该序列中的最小值</returns>
    public static int GetMin(IEnumerable<int> numbers)
    {
        int min = 0;
        bool status = true;
        foreach (var item in numbers)
        {
            if (status)
            {
                min = item;
                status = !status;
            }
            if (min > item)
            {
                min = item;
            }
        }
        return min;
    }
    /// <summary>
    /// 传入几何返回偶数序列组成的新集合
    /// </summary>
    /// <param name="numbers">集合对象</param>
    /// <returns>偶数序列组成的新序列</returns>
    public static IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
    {
        List<int> EvenNumbers = new List<int>();
        int i = 1;
        foreach (var item in numbers)
        {
            if (item % 2 == 0)
            {
                EvenNumbers.Add(item);
            }
            i++;
        }
        return EvenNumbers;
    }
}
public class ListAndArray : MonoBehaviour
{
    public int[] numArray;
    public List<int> numList;
    // Start is called before the first frame update
    void Start()
    {
        numArray = new int[] { 5, 12, 7, 3, 18, 9, 21, 4 };
        numList = new List<int> { 10, 25, 8, 15, 30, 6, 19, 11 };
        GetNmbersResult(numArray);
        GetNmbersResult(numList);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 汇总集合操作类的方法并打印输出结果
    /// </summary>
    /// <param name="numbers">集合对象</param>
    public void GetNmbersResult(IEnumerable<int> numbers)
    {
        Debug.Log("------------------------------------------------------------------");
        StringBuilder text = new StringBuilder();
        Debug.Log($"该集合的总和是:{DataStatistics.GetSum(numbers)}");
        Debug.Log($"该集合的平均值是:{DataStatistics.GetAverage(numbers)}");
        Debug.Log($"该集合的最大值是:{DataStatistics.GetMax(numbers)}");
        Debug.Log($"该集合的最小值是:{DataStatistics.GetMin(numbers)}");
        text.Append("[ ");
        foreach (var item in DataStatistics.GetEvenNumbers(numbers))
        {
            text.Append(item + " ");
        }
        text.Append("]");
        Debug.Log($"该集合的偶数集合是:{text}");
    }
}
