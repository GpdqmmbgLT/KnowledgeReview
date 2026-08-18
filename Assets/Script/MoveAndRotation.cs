using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 对移动，跳跃，旋转进行深挖（Translate，Rigidbody，CharacterControler）
/// </summary>
public class MoveAndRotation : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotateSpeed = 60;
    public bool isGround = true;
    public float g = -9.18f;
    public float jumpHeight = 6;
    public LayerMask ground;
    public float checkHeight = 2;
    Rigidbody rig;
    CharacterController cha;
    Vector3 jumpDeriction = new Vector3();

    //public float maxRotateAngle = 70;
    void Start()
    {
        rig = AddComponent<Rigidbody>();
        //cha = AddComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerMove();
        OnPlayerRotate();
        //OnpalyerJump_controler();//会和Translate冲突
    }
    void FixedUpdate()
    {
        OnPlayerJump_Rigidbody();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <returns>返回组件值</returns>
    public T AddComponent<T>() where T : Component
    {
        T t = GetComponent<T>();
        if (t == null)
        {
            t = gameObject.AddComponent<T>();
        }
        return t;
    }
    /// <summary>
    /// 实现玩家的跳跃功能-角色控制器
    /// </summary>
    public void OnpalyerJump_controler()
    {
        if (isGround && jumpDeriction.y < 0)//如果玩家在地面并且重力小于0，就施加恒定的重量-2
        {
            jumpDeriction.y = -2;
        }
        //当玩家按下空格并且当前处于地面上就执行跳跃功能
        if (Input.GetKeyDown(KeyCode.Space) && IsGround(transform, ground))
        {
            jumpDeriction.y = Mathf.Sqrt(jumpHeight * -2 * g);//根据物理公式计算初速度
        }
        jumpDeriction.y += g * Time.deltaTime;
        cha.Move(jumpDeriction * Time.deltaTime);
    }
    /// <summary>
    /// 实现玩家的跳跃功能-刚体
    /// </summary>
    public void OnPlayerJump_Rigidbody()
    {
        //当玩家按下空格并且当前处于地面上就执行跳跃功能
        if (Input.GetKeyDown(KeyCode.Space) && IsGround(transform, ground))
        {
            rig.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);//施加一个瞬间冲量
        }
        //每秒施加重力
        //rig.AddForce(new Vector3(0, g, 0) * Time.fixedDeltaTime);
    }


    /// <summary>
    /// 检测目标对象是否在目标图层上
    /// </summary>
    /// <param name="mask">要检测的目标图层</param>
    /// <param name="target">要检测的目标对象</param>
    /// <returns></returns>
    public bool IsGround(Transform target, LayerMask mask)
    {
        bool isGrounds = Physics.CheckSphere(target.position, checkHeight, mask);
        Debug.Log(isGrounds);
        return isGrounds;
    }

    /// <summary>
    /// 实现玩家移动的功能
    /// </summary>
    public void OnPlayerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");//获取键盘水平轴
        float vertical = Input.GetAxis("Vertical");//获取键盘垂直轴
        transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 实现玩家的跳跃功能
    /// </summary>
    public void OnPlayerRotate()
    {
        float horizontal = Input.GetAxis("Mouse X");//获取鼠标水平轴
        transform.Rotate(new Vector3(0, horizontal, 0) * rotateSpeed * Time.deltaTime, Space.Self);
    }
}
