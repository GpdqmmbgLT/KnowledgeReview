using System;
using System.Collections.Generic;
using UnityEngine;
public class F_DataManager_StoreBagSystem
{
  public const float contentIncrement = 310;//滚动条的高度增量
  private float curruntcontentIncrement = 310;//当前滚动条的高度
  public float CurruntcontentIncrement
  {
    get
    {
      return curruntcontentIncrement;
    }
    set
    {
      curruntcontentIncrement = value;
    }
  }
  protected const float gap_x = 396;//左右相邻商品的X间隔长度
  protected const float gap_y = 286;//上下相邻商品的Y间隔长度
  protected readonly Vector2 firstPo = new Vector2(-595, -132);//第一个商品的位置
  protected float po_X = -595;//最新一个商品的X坐标
  protected float po_Y = -132;//最新一个商品的Y坐标
  /// <summary>
  /// 获取最新一个商品的坐标(先计算作坐标，再添加进字典，因此本方法逻辑计算的事下一个商品的位置坐标)
  /// </summary>
  /// <param name="count">当前商品/货物的数量</param>
  /// <returns></returns>
  public virtual Vector2 GetPo(int count)
  {
    //当商品数量为0时，直接返回首个坐标
    if (count == 0)
    {
      return firstPo;
    }
    //当商品数量取余为0时说明换行，因此x重置y自增,滚动条高度自增
    if (count % 4 == 0)
    {
      CurruntcontentIncrement += contentIncrement;
      po_X = firstPo.x;
      po_Y -= gap_y;
    }
    else//其他情况下说明只是x改变，因此x自增
    {
      po_X += gap_x;
    }
    return new Vector2(po_X, po_Y);
  }
}