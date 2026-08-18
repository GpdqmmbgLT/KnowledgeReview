using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对动画系统知识进行回顾（新内容：混合树BleedTree，）
/// </summary>
public class AnimatorTest : MonoBehaviour
{
    Animator anim;
    public float speed = 5;
    public bool statu;
    public float animNum;
    public float lengthBe;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        statu = false;
        animNum = 0;
        lengthBe = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        // float length = move.magnitude;
        // if (Input.anyKey)
        // {
        //     if (length >= lengthBe)
        //     {
        //         animNum = Mathf.Clamp(animNum + 0.05f, 0, 1);
        //     }
        //     else if (length <= lengthBe)
        //     {
        //         animNum = Mathf.Clamp(animNum - 0.05f, 0, 1);
        //     }
        // }
        // else
        // {
        //     animNum = 0;
        // }
        float targetSpeed = Mathf.Clamp01(move.magnitude);
        animNum = Mathf.MoveTowards(animNum, targetSpeed, 2 * Time.deltaTime);
        anim.SetFloat("Blend", animNum);

        //lengthBe = length;
        transform.Translate(move * speed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(move);
    }

}
