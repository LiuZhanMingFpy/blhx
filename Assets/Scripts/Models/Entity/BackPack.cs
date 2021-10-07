using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包实体
/// </summary>
public class BackPack
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string UserID { get; set; }
    /// <summary>
    /// 背包ID
    /// </summary>
    public string BackPackID { get; set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    public int QuantityNum { get; set; }
    /// <summary>
    /// 物品上限
    /// </summary>
    public int QuantityUpLimit { get; set; }
    /// <summary>
    /// 装备数量
    /// </summary>
    public int MaterialScienceNum { get; set; }
    /// <summary>
    /// 装备上限
    /// </summary>
    public int MaterialScienceUpLimit { get; set; }

}
