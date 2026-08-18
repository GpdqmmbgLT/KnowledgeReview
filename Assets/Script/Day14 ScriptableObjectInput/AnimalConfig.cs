using System;
using UnityEngine;


public class AnimalConfig : MonoBehaviour
{
    public string filePath;
    public AnimalConfigData animalConfigData;
    void Start()
    {
        filePath = Application.dataPath + "/Script/Day14 ScriptableObjectInput/AnimalConfig.csv";
        animalConfigData = ScriptableObject.CreateInstance<AnimalConfigData>();
        if (animalConfigData != null)
        {
            Editor_AniamlConfig.GetConfig(filePath, animalConfigData);
            Editor_AniamlConfig.ShowData(animalConfigData);
        }
        else
        {
            Debug.Log("未找到组件：" + typeof(AnimalConfigData));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
