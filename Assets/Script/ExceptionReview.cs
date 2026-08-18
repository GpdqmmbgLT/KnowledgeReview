using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionReview : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExceptionText();
    }

    /// <summary>
    /// 测试异常代码并抛出
    /// </summary>
    public void ExceptionText()
    {
        try
        {
            string test = "abc";
            int num = int.Parse(test);
        }
        catch (System.Exception)
        {
            Debug.Log("错误，无法转化为数字");
            throw;
        }
    }
}
