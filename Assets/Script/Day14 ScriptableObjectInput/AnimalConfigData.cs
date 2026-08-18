using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AnimalConfig", menuName = "Config/AnimalConfig")]
public class AnimalConfigData : ScriptableObject
{
  public List<AnimalParameters> animalParameters = new List<AnimalParameters>();
}