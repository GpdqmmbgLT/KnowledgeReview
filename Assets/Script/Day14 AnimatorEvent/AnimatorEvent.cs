using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    AnimatorStateInfo animatorStateInfo;//定义层级状态类
    public AudioClip[] clips;
    public AudioSource audioSource;
    public Animator animator;
    int[] VictoryHash = new int[2];//定义动画的哈希值，效率更高
    public float coolingTime;
    public string shutAnimatorName;//胜利动画的名字
    float timer_AnimPlay;//触发动画的计时器
    float timer_LogMessage;//打印信息的计时器
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        animator = gameObject.GetComponent<Animator>();
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        coolingTime = 3.6f;//胜利动画的动画时长
        shutAnimatorName = "victory";
        VictoryHash[0] = Animator.StringToHash(shutAnimatorName);
        VictoryHash[1] = Animator.StringToHash("idle");
        timer_AnimPlay = 3.6f;
        timer_LogMessage = 6;

    }

    // Update is called once per frame
    void Update()
    {   //传入默认状态层
        //0 - 是默认的层级
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        timer_AnimPlay += Time.deltaTime;
        timer_LogMessage += Time.deltaTime;
        PlayerControl();
        if (timer_AnimPlay >= coolingTime && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("按下了空格");
            animator.SetTrigger("TrigVictory");
            timer_AnimPlay = 0;
        }
        if (timer_LogMessage >= coolingTime - 0.6f)
        {
            LogAnimatorMessage();
            timer_LogMessage = 0;
        }

    }
    /// <summary>
    /// 玩家可操控的动画功能
    /// Q - 动画速度降为0.3倍数
    /// E - 动画速度恢复为正常
    /// W - 跳转到某个动画的某个时间
    /// </summary>
    public void PlayerControl()
    {
        if (Input.GetKeyDown(KeyCode.Q)) animator.speed = 0.3f;
        if (Input.GetKeyDown(KeyCode.W)) animator.speed = 1;
        if (Input.GetKeyDown(KeyCode.E)) animator.Play(shutAnimatorName, 0, 0.5f);//跳转到Vectory动画的1.75秒处
    }
    /// <summary>
    /// 定时打印输出当前动画状态
    /// </summary>
    public void LogAnimatorMessage()
    {
        float curruntTime = animatorStateInfo.normalizedTime;//获取动画进度
        string curruntName = "";//当前播放的动画名称
        if (VictoryHash[0] == animatorStateInfo.shortNameHash)
        {
            curruntName = shutAnimatorName;
        }
        else if (VictoryHash[1] == animatorStateInfo.shortNameHash)
        {
            curruntName = "idle";
        }

        Debug.Log($"当前动画名称:{curruntName}  当前动画进度:{curruntTime}");
    }
    /// <summary>
    /// 动画事件-播放挥刀音效
    /// </summary>
    /// <param name="shutId">音效索引</param>
    public void RiseHands(int shutId)
    {
        if (clips[shutId] == null)
        {
            Debug.Log("音效未赋值!请检查后重试");
            Debug.Log($"请检查音效列表第{shutId + 1}条 索引[shutId]");
            return;
        }
        audioSource.PlayOneShot(clips[shutId]);
    }
    /// <summary>
    /// 动画事件-显示伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void HitEnemy(float damage)
    {
        Debug.Log($"对敌方造成{damage}点伤害");
    }
    /// <summary>
    /// 动画事件-播放收刀音效
    /// </summary>
    public void ReturnBack()
    {
        if (clips[1] == null)
        {
            Debug.Log("音效未赋值,请检查后重试!");
            Debug.Log("请检查音效列表第二条 索引[1]");
            return;
        }
        audioSource.PlayOneShot(clips[1]);
    }
}
