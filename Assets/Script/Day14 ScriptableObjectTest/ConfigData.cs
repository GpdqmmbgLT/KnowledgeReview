using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
internal class WeaponParameters
{
    //public WeaponParameters(string )
    internal string weaponName;//武器名称
    internal int damage;//子弹伤害
    internal float fireRate;//武器发射冷却
    internal int bulletSpeed;//子弹飞行速度
    internal Color bulletColor;//子弹颜色


    public WeaponParameters(string weaponName, int damage, float fireRate, int bulletSpeed, Color bulletColor)
    {
        this.weaponName = weaponName;
        this.damage = damage;
        this.fireRate = fireRate;
        this.bulletSpeed = bulletSpeed;
        this.bulletColor = bulletColor;
    }
}
public class ConfigData : MonoBehaviour
{
    internal static Dictionary<int, WeaponParameters> weaponData = new Dictionary<int, WeaponParameters>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("进入");
        string path = Application.dataPath + "/Script/Day14 ScriptableObjectTest/WeaponConfig.csv";
        GetData(path);
        foreach (var item in weaponData.Keys)
        {
            Debug.Log(item + " " + weaponData[item].weaponName + " " + weaponData[item].bulletSpeed + " " + weaponData[item].damage + " ");
        }
    }
    /// <summary>
    /// 遍历CVR文件并尝试存储到数据结构中
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <exception cref="System.Exception">文件错误</exception>
    internal void GetData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        if (lines == null)
        {
            throw new System.Exception("文件错误!请检查后重试");
        }
        else
        {
            foreach (var item in lines)
            {
                string[] cells = item.Split(',');
                if (cells[0] == "Number") continue;
                if (cells.Length < 8) continue;
                weaponData.Add(GetT<int>(cells[0]),
                new WeaponParameters(
                    cells[1], GetT<int>(cells[2]), GetT<float>(cells[3]),
                    GetT<int>(cells[4]), new Color(GetT<int>(cells[5]) / 255f,
                    GetT<int>(cells[6]) / 255f, GetT<int>(cells[7])) / 255f
                ));
            }
        }
    }
    /// <summary>
    /// 尝试转换参数为任意类型
    /// </summary>
    /// <typeparam name="T">任意类型</typeparam>
    /// <param name="input">字符串</param>
    /// <returns>
    /// 成功 - 转换后的值
    /// 失败 - 默认值
    /// </returns>
    internal T GetT<T>(string input)
    {
        if (input == null)
        {
            return default;
        }
        try
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
        catch
        {
            return default;
        }
    }
}
