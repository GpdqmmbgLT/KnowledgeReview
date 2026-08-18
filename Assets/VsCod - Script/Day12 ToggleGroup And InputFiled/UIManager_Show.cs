using TMPro;
using UnityEngine;
public class UIManager_Show : MonoBehaviour
{
  [Header("展示信息页面文本组件")]
  public TextMeshProUGUI playerInformation_Tetx;
  void OnEnable()
  {
    UpdateMessage();
  }
  /// <summary>
  /// 更新信息
  /// </summary>
  public void UpdateMessage()
  {
    string abilitys = "";
    foreach (var item in DataManager_Players.Instance.playerAbility)
    {
      abilitys += (item + " ");
    }
    playerInformation_Tetx.text =
    $"<color=red>Name</color>\t\t{DataManager_Players.Instance.PlayerName}\n\n" +
    $"<color=red>Profession</color>\t\t{DataManager_Players.Instance.PlayerProfession}\n\n" +
    $"<color=red>Ability</color>\t\t{abilitys}\n\n" +
    $"<color=red>Difficuty</color>\t\t{DataManager_Players.Instance.gameDifficuty}";
  }
}