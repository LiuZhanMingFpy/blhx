using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 拥有装备实体
/// </summary>
public class HaveEquipmet
{
    /// <summary>
    /// 装备ID
    /// </summary>
    public string EquipmentID { get; set; }
    /// <summary>
    /// 装备名称
    /// </summary>
    public string EquipmentName { get; set; }
    /// <summary>
    /// 装备品质
    /// </summary>
    public int EquipmentQuality { get; set; }
    /// <summary>
    /// 装备类别
    /// </summary>
    public int EquipmentCategory { get; set; }
    /// <summary>
    /// 备注说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 攻击力
    /// </summary>
    public int Atk { get; set; }
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public int AtkCD { get; set; }
    /// <summary>
    /// 附加能力
    /// </summary>
    public int AdditionalAbility { get; set; }
    /// <summary>
    /// 附加数值
    /// </summary>
    public int AdditionalValue { get; set; }
    /// <summary>
    /// 贩卖价格
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// 别称
    /// </summary>
    public string Nickname { get; set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    public string BackPackID { get; set; }

}
