using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回顾Transform的相关知识（位置，旋转，缩放）
/// </summary>
public class TransformReview : MonoBehaviour
{
    Vector3 worldpo, localpo, lpcalsc;
    Quaternion worldro, localro;
    // Update is called once per frame
    void Update()
    {
        //TransformTest();
        CubeTransformTest();
    }

    /// <summary>
    /// 操作立方体（移动，旋转，坐标改变）
    /// </summary>
    public void CubeTransformTest()
    {
        //坐标移动
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }
        //自我旋转
        transform.Rotate(0, 30 * Time.deltaTime, 0);
        //获取局部坐标，改为世界坐标
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.localPosition;
        }
    }
    /// <summary>
    /// 测试Transform下的各种api(本地/局部坐标 ，本地/局部旋转，本地缩放)
    /// </summary>
    public void TransformTest()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            worldpo = gameObject.transform.position;//获取本物体的世界坐标
            gameObject.transform.position = new Vector3(worldpo.x, worldpo.y, worldpo.z + 1);//改变物体的世界z坐标 
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            localpo = gameObject.transform.localPosition;//获取本物体的本地坐标
            gameObject.transform.localPosition = new Vector3(localpo.x, localpo.y + 1, localpo.z);//改变物体的本地y坐标 
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            worldro = gameObject.transform.rotation;//获取本物体的世界旋转
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);//改变物体世界旋转 
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            localro = gameObject.transform.localRotation;//获取本物体的本地旋转
            gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);//改变物体的本地旋转
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            lpcalsc = gameObject.transform.localScale;//获取本物体的本地缩放
            gameObject.transform.localScale = new Vector3(lpcalsc.x + 0.1f, lpcalsc.y + 0.1f, lpcalsc.z + 0.1f);//改变物体的本地缩放
        }

    }
}
