using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对射线检测进行回顾（Physics.Raycast,
/// </summary>
public class RaycastReview : MonoBehaviour
{
    public GameObject sphere;//预制体球体
    public LayerMask wall;//图层-墙
    public Camera mainCamera;//主相机
    public GameObject sphereFather;//球体的父物体
    // Update is called once per frame
    void Update()
    {
        //Physics_Raycast();
        Physics_Spherecast();
    }
    void FixedUpdate()
    {
        //ThrowShpere();
    }
    /// <summary>
    /// 虚拟化球体与目标图层碰撞且触发指定事件便生成指定预制体
    /// </summary>
    public void Physics_Spherecast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //获取当前碰撞范围内的所有碰撞信息
            RaycastHit[] raycastHits = Physics.SphereCastAll(ray.origin, 2, ray.direction, 100);
            if (raycastHits.Length > 0)
            {
                foreach (var item in raycastHits)
                {
                    Debug.Log("物体名称:" + item.collider.gameObject.name + "  颜色:" + item.collider.gameObject.GetComponent<Renderer>().material.color);//遍历打印碰撞信息
                }
            }
        }

    }
    /// <summary>
    /// 射线与目标图层碰撞且触发指定事件便生成指定预制体
    /// </summary>
    public void Physics_Raycast()
    {
        //以鼠标的位置发射一条射线
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newBallPo = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        Debug.DrawRay(newBallPo, (newBallPo - mainCamera.transform.position).normalized * 10, Color.green);
        //如果和目标图层发生碰撞并且点击鼠标左键便条件成立
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, wall) && Input.GetMouseButtonDown(0))
        {
            //实例化一个预制体球体
            Instantiate(sphere, hit.point, Quaternion.identity, sphereFather.transform);
        }
    }
    /// <summary>
    /// 点击屏幕向前方发射一个球体
    /// </summary>
    public void ThrowShpere()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //将屏幕上的坐标转换为世界坐标
            Vector3 newBallPo = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4f));
            //实例化一个预制体球体
            GameObject newSphere = Instantiate(sphere, newBallPo, Quaternion.identity, sphereFather.transform);
            Rigidbody newSphereRig = newSphere.AddComponent<Rigidbody>();
            newSphereRig.useGravity = true;
            newSphereRig.AddForce((newBallPo - mainCamera.transform.position).normalized * 5, ForceMode.Impulse);

        }
    }
}
