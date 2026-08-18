using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UIManager_Products : MonoBehaviour
{
  public DataManager_Products _dataProduct;//挂载的DataManager_Products脚本
  public Button button;
  /// <summary>
  /// 获取本物体的ID与DataManager_Products对象
  /// </summary>
  /// <param name="_Products">需要一个DataManager_Products参数</param>
  /// <returns>ID与DataManager_Products对象 若对象为空则返回0和null</returns>
  public int ProductsData(out DataManager_Products _Products)
  {
    _dataProduct = GetComponent<DataManager_Products>();
    if (_dataProduct == null)
    {
      Debug.Log("该组件:DataManager_Products未找到,返回null");
      _Products = null;
      return 0;
    }
    _Products = _dataProduct;
    return _dataProduct.ProductID;
  }
  /// <summary>
  /// 传入委托事件添加到点击事件中
  /// </summary>
  /// <param name="onclickEvent">按钮点击事件</param>
  public void AddOnclickEvent(UnityAction onclickEvent)
  {
    button = gameObject.GetComponent<Button>();
    if (button == null)
    {
      Debug.Log("Button为空!");
    }
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(onclickEvent);
  }
}