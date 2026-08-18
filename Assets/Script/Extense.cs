using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FatherClass : MonoBehaviour
{
    public int num;
    readonly string text;
    internal float price;
    private int nums;
    protected string texts;
    public FatherClass(int num, string text, float price, int nums, string texts)
    {
        this.num = num;
        this.text = text;
        this.price = price;
        this.nums = nums;
        this.texts = texts;
    }
    public virtual void Method_One()
    {
        Debug.Log("Father-Method_One");
    }
    public void Method_Two()
    {
        Debug.Log("Father-Method_Two");
    }

}

public class Extense : FatherClass
{
    public Extense(int num, string text, float price, int nums, string texts) : base(num, text, price, nums, texts) { }
    public override void Method_One()
    {
        Debug.Log("Son-Method_One");
    }
    public new void Method_Two()
    {
        Debug.Log("Son-Method_Two");
    }
}
