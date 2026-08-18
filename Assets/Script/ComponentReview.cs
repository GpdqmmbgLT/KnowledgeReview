using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentReview : MonoBehaviour
{
    public Light lights;
    // Start is called before the first frame update
    void Start()
    {
        ComponentGetTest();
    }

    /// <summary>
    /// 对获取组件和添加组件的四种api进行测试
    /// </summary>
    public void ComponentGetTest()
    {
        //添加组件
        gameObject.AddComponent<Rigidbody>();
        //获取组件
        Light tempLights = gameObject.GetComponent<Light>();
        if (tempLights != null)
        {
            lights = tempLights;
        }
        else
        {
            lights = gameObject.AddComponent<Light>();
        }
        Debug.Log(lights.name);
        //获取子物体组件(也会获取自身的)
        Rigidbody childRig = gameObject.GetComponentInChildren<Rigidbody>();
        Debug.Log(ReturnMesagge<Rigidbody>(childRig, gameObject, "成功获取子物体组件"));
        //获取子物体组件(也会获取自身的)
        Rigidbody parRig = gameObject.GetComponentInParent<Rigidbody>();
        Debug.Log(ReturnMesagge<Rigidbody>(parRig, gameObject, "成功获取父物体组件"));

    }

    /// <summary>
    /// 判断组件是否为空并返回对应提示信息
    /// </summary>
    /// <typeparam name="T">仅可组件</typeparam>
    /// <param name="t">仅可组件</param>
    /// <param name="obj">本物体</param>
    /// <param name="mes">输出信息</param>
    /// <returns>返回不同情况下的信息</returns>
    public string ReturnMesagge<T>(T t, GameObject obj, string mes) where T : Component
    {
        return ((t != null) && (t != obj.GetComponent<T>())) ? mes : "组件不存在/组件为自身";
    }
}
