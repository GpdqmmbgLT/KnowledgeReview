using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light SunLight;//太阳光
    public Light LampLight;//点光源
    public Light FlashLight;//手电筒
    public Dictionary<KeyCode, Action> keyCheck;
    // Start is called before the first frame update
    void Start()
    {
        LightChange();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in keyCheck.Keys)
        {
            if (Input.GetKeyDown(item))
            {
                keyCheck[item]?.Invoke();
            }
        }
    }
    /// <summary>
    /// 初始化按键检测字典，添加按键和对应的委托事件
    /// </summary>
    public void LightChange()
    {
        keyCheck = new Dictionary<KeyCode, Action>
        {
            {KeyCode.Alpha1, ()=>{SunLight.enabled = !SunLight.enabled;}},//控制太阳光的开关
            {KeyCode.Alpha2, ()=>{LampLight.enabled = !LampLight.enabled;}},//控制吊灯的开关
            {KeyCode.Alpha3, ()=>{FlashLight.enabled = !FlashLight.enabled;}},//控制手电筒的开关
            {KeyCode.Q,()=>{LampLight.intensity = Mathf.Clamp(LampLight.intensity + 0.5f,0.5f,4);}},//增加吊灯的亮度
            {KeyCode.E,()=>{LampLight.intensity = Mathf.Clamp(LampLight.intensity - 0.5f,0.5f,4);}},//减少吊灯的亮度
            {KeyCode.R,()=>{FlashLight.color = UnityEngine.Random.ColorHSV();}},//随机改变手电筒的颜色
            {KeyCode.T,()=>{FlashLight.spotAngle = Mathf.Clamp(FlashLight.spotAngle + 5,10,50);}},//增加手电筒的照射角度
            {KeyCode.Y,()=>{FlashLight.spotAngle = Mathf.Clamp(FlashLight.spotAngle - 5,10,50);}},//减少手电筒的照射角度
            {KeyCode.U,()=>{FlashLight.range = Mathf.Clamp(FlashLight.range + 2,5,20);}},//增加手电筒的照射范围
            {KeyCode.I,()=>{FlashLight.range = Mathf.Clamp(FlashLight.range - 2,5,20);}},//减少手电筒的照射范围
        };
    }
}
