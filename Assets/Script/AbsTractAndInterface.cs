using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Animal
{
    public abstract void MakeSound();
}
public interface IFlay
{
    void StartFlay();
}
public interface ISwim
{
    void StartSwim();
}
//鸭子类
public class Durk : Animal, ISwim, IFlay
{
    public override void MakeSound()
    {
        Debug.Log("嘎嘎叫");
    }
    public void StartSwim()
    {
        Debug.Log("鸭子游泳");
    }

    public void StartFlay()
    {
        Debug.Log("鸭子飞");
    }
}
//企鹅类
public class Penguin : Animal, ISwim
{
    public override void MakeSound()
    {
        Debug.Log("企鹅叫");
    }
    public void StartSwim()
    {
        Debug.Log("企鹅游泳");
    }
}
//蝙蝠类
public class Bat : Animal, IFlay
{
    public override void MakeSound()
    {
        Debug.Log("吱吱叫");
    }
    public void StartFlay()
    {
        Debug.Log("蝙蝠飞起来");
    }
}
//老鹰类
public class Eagle : Animal, IFlay
{
    public override void MakeSound()
    {
        Debug.Log("老鹰叫");
    }
    public void StartFlay()
    {
        Debug.Log("老鹰飞起来");
    }
}
//实现所有动物的杂技表演功能
public class AbsTractAndInterface : MonoBehaviour
{
    float timer;//计时器
    public Animal[] animals;
    void Start()
    {
        animals = new Animal[] { new Durk(), new Bat(), new Eagle(), new Penguin() };
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Tab) && timer >= 2)
        {
            StartPerformance();
            timer = 0;
        }
    }

    /// <summary>
    /// 实现动物的表演功能
    /// </summary>
    public void StartPerformance()
    {
        foreach (var temp in animals)
        {
            temp.MakeSound();
            if (temp is IFlay flay)
            {
                flay.StartFlay();
            }
            if (temp is ISwim swim)
            {
                swim.StartSwim();
            }
            Debug.Log("------------------------------------------------------------------");
        }
    }
}
