using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DataManager_Message))]
public class UIManager_Message : MonoBehaviour
{
  [Header("主页面面板物体")]
  public GameObject mainPage_Panel;
  [Header("消息页面面板")]
  public GameObject messagePage_Panel;
  [Header("消息按钮预制体物体")]
  public GameObject messgaeButton_Prefab;
  [Header("消息按钮的父物体")]
  public GameObject messageButtonFather;
  [Header("消息按钮的父物体的rect")]
  public RectTransform messageButtonFatherRect;
  [Header("显示总消息数量的文本")]
  public TextMeshProUGUI messageCount_text;
  void Start()
  {
    mainPage_Panel.SetActive(true);
    messagePage_Panel.SetActive(false);
    if (DataManager_Message.Instance == null)
    {
      DataManager_Message.Instance = GetComponent<DataManager_Message>();
    }
  }
  /// <summary>
  /// 打开/关闭 主/消息页面
  /// </summary>
  public void Button_OpenExitPage()
  {
    mainPage_Panel.SetActive(!mainPage_Panel.activeSelf);
    messagePage_Panel.SetActive(!messagePage_Panel.activeSelf);
  }
  /// <summary>
  /// 返回随机三种颜色文本 red/blue/black
  /// </summary>
  /// <returns></returns>
  public string GetMessageColor()
  {
    int num = Random.Range(1, 4);
    switch (num)
    {
      case 1:
        return "red";
      case 2:
        return "blue";
      case 3:
        return "black";
      default:
        return "black";
    }
  }
  /// <summary>
  /// 设置物体的RectTransform位置
  /// </summary>
  /// <param name="obj">消息对象</param>
  /// <param name="rect_x">X偏移</param>
  /// <param name="rect_y">Y偏移</param>
  public void SetRectTransform(GameObject obj, float rect_x, float rect_y)
  {
    RectTransform rect = obj.GetComponent<RectTransform>();
    rect.anchoredPosition = new Vector2(rect_x, rect_y);
  }
  /// <summary>
  /// 添加点击事件并改变显示文本
  /// </summary>
  /// <param name="obj">信息对象</param>
  public void AddMessageEventAndText(GameObject obj)
  {
    //创建要显示的消息内容
    string colorText = GetMessageColor();
    string messageText = $"<color={colorText}>This is the {DataManager_Message.Instance.MessageNum} message</color>";
    Button button = obj.GetComponent<Button>();
    TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
    //修改文本显示内容
    if (text != null)
    {
      text.text = messageText;
    }
    else
    {
      Debug.Log($"没有找到第{DataManager_Message.Instance.MessageNum}条的TextMeshProUGUI组件!!");
    }
    //修改点击事件内容
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(
      () => { Debug.Log(text + "\n本次文本颜色为" + colorText); }
    );

  }
  /// <summary>
  /// 增加信息，创建实例并改变rect属性，Text文本内容，点击事件
  /// </summary>
  public void Button_AddMessage()
  {
    if (DataManager_Message.Instance.messagePool.Count >= DataManager_Message.maxPoolCount)
    {
      Debug.Log("消息数量超过" + DataManager_Message.maxPoolCount + "!请删除多余消息后重试");
      return;
    }
    GameObject newMes = DataManager_Message.Instance.ObjectPool.GetObj(
      messgaeButton_Prefab, messageButtonFather.transform.position, Quaternion.identity, messageButtonFather.transform);
    DataManager_Message.Instance.messagePool.Enqueue(newMes);//加入到消息池
    SetRectTransform(newMes, DataManager_Message.Instance.nextMessagePo_X, DataManager_Message.Instance.NextMessagePo_Y);
    AddMessageEventAndText(newMes);
    DataManager_Message.Instance.UpdateMessageAttribute();
    ShowMessageCount();
    //调整content的高度
    UpdateContentHegiht();
  }
  /// <summary>
  /// 移除最早的一条消息
  /// </summary>
  public void Button_RemoveEarliestMessage()
  {
    //从消息池里面移除第一条，加入到对象池中
    if (DataManager_Message.Instance.messagePool.Count == 0)
    {
      Debug.Log("消息池中没有消息,不必移除");
      return;
    }
    GameObject removeObj = DataManager_Message.Instance.messagePool.Dequeue();
    removeObj.SetActive(false);
    DataManager_Message.Instance.ObjectPool.RecycleObj(removeObj);
    //更新其余消息的位置，依次往前移动一个位置
    DataManager_Message.Instance.RankMessage();
    ShowMessageCount();
    //调整content的高度
    UpdateContentHegiht();
  }
  /// <summary>
  /// 刷新信息数量显示
  /// </summary>
  public void ShowMessageCount()
  {
    messageCount_text.text = $"<color=red>MessageCount:{DataManager_Message.Instance.messagePool.Count}</color>";
  }
  /// <summary>
  /// 刷新content的高度 = 消息数量*100
  /// </summary>
  public void UpdateContentHegiht()
  {
    int messageCount = DataManager_Message.Instance.messagePool.Count;
    if (messageCount <= 5)
    {
      messageButtonFatherRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 600f);
    }
    else
    {
      messageButtonFatherRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (DataManager_Message.Instance.messagePool.Count + 1) * 108f);
    }
  }
}