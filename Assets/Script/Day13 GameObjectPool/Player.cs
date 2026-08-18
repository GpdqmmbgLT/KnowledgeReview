using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("子弹预制体")]
    public GameObject prefabe;
    [Header("摄像机")]
    public Camera cameras;
    [Header("子弹生成的父物体")]
    public GameObject createToObject;
    GameObjectPool pool;//对象池
    float timer;//计时器
    void Start()
    {
        pool = gameObject.AddComponent<GameObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer >= 1)
        {
            Vector3 mousPo = Input.mousePosition;
            mousPo.z = 2;
            GameObject newObj = pool.GetObj(prefabe, cameras.ScreenToWorldPoint(mousPo), Quaternion.identity, createToObject.transform);
            newObj.GetComponent<Bullets>().movePosition = (cameras.ScreenToWorldPoint(mousPo) - cameras.transform.position).normalized;
            timer = 0;
        }
    }
}
