using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店配置实体
/// </summary>
public class Shop {
    /// <summary>
    /// 物品ID/装备ID
    /// </summary>
    public string Goodes { get; set; }
    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// 品质:0蓝色,1紫色,2金色,3彩色
    /// </summary>
    public int Quality { get; set; }
    /// <summary>
    /// 数量
    /// </summary>
    public int Num { get; set; }
    /// <summary>
    /// 购买方式:0钻石,1金币,2资源
    /// </summary>
    public int BuyFashion { get; set; }
    /// <summary>
    /// 种类,0常见资源售卖,1消耗品,2材料,3装备
    /// </summary>
    public int Species { get; set; }
    /// <summary>
    /// 是否启用0不启用,1启用
    /// </summary>
    public int Enable { get; set; }
    /// <summary>
    /// 别称:关联图片
    /// </summary>
    public string Nickname { get; set; }

    public string Name { get; set; }
    public string Details { get; set; }

}
