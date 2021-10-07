using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
/// <summary>
/// 物品实体
/// </summary>
public class Goods
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public string GoodsID { get; set; }
    /// <summary>
    /// 物品名称
    /// </summary>
    public string GoodsName { get; set; }
    /// <summary>
    /// 物品品质
    /// </summary>
    public int GoodsQuality { get; set; }
    /// <summary>
    /// 物品类别
    /// </summary>
    public int GoodsCategory { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 别名,保存图片的
    /// </summary>
    public string Nickname { get; set; }
    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// 作用效果
    /// </summary>
    public int Effect { get; set; }
    /// <summary>
    /// 作用数值
    /// </summary>
    public int Value { get; set; }

    public int Num { get; set; }
}
