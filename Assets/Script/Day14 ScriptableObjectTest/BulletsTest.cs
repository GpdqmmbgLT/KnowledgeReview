using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsTest : MonoBehaviour
{
    public int damage;//子弹伤害
    public int bulletSpeed;//子弹飞行速度
    public Color bulletColor;//子弹颜色
    public Camera cameras;//目标相机
    public float minMoveDistance;//子弹追踪鼠标的最小追踪距离
    public Action<GameObject> recyclePool;//回收池子的事件
    //public Vector3 targetDirection;
    float timer;//计时器
    void Start()
    {
        minMoveDistance = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //把鼠标位置转换为世界坐标的点
        Vector3 mousePos = cameras.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
        //如果子弹距离鼠标的距离大于x才进行追踪移动
        if (Vector3.Distance(mousePos, transform.position) > minMoveDistance)
        {
            Vector3 targetDirection = (mousePos - transform.position).normalized;
            transform.Translate(bulletSpeed * Time.deltaTime * targetDirection, Space.World);
        }

        if (timer >= 3)
        {
            gameObject.SetActive(false);

        }
    }
    void OnDisable()
    {
        timer = 0;
        recyclePool?.Invoke(gameObject);
    }
    /// <summary>
    /// 初始化子弹数据
    /// </summary>
    /// <param name="damage">子弹伤害</param>
    /// <param name="bulletSpeed">子弹飞行速度</param>
    /// <param name="bulletColor">子弹颜色</param>
    public void Init(int damage, int bulletSpeed, Color bulletColor, Camera camera)
    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        GetComponent<Renderer>().material.color = bulletColor;
        cameras = camera;
        Debug.Log($"子弹伤害：{damage}  子弹飞行速度：{bulletSpeed}  子弹颜色值：{bulletColor}");
    }
}
