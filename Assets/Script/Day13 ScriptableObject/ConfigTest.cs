using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DayStatus
{
    黑夜 = 1,
    白天 = 2
}
[CreateAssetMenu(fileName = "Configtest", menuName = "Config/Test1")]
public class ConfigTest : ScriptableObject
{
    public DayStatus dayStatus;
    public int num = 20;
    public string text = "Text";
    public GameObject bullet;
    public float time = 5;
}
