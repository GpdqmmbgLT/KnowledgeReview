using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCastChecked : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OverlapSpheres();
    }

    /// <summary>
    /// 静态检测-目标范围内的所有碰撞体
    /// </summary>
    public void OverlapSpheres()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);//虚拟化球体，返回碰撞到的所有碰撞体
        foreach (var item in colliders)
        {
            Debug.Log("物体名称:" + item.gameObject.name + "  颜色:" + item.gameObject.GetComponent<Renderer>().material.color);//遍历打印碰撞信息
        }
    }
}
