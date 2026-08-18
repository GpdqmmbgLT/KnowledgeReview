using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAndTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("有物体进入了触发器范围");
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("有物体离开了触发器范围");
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("有物体碰撞到我");
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("有物体离开了我");
    }

}
