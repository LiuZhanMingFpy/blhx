using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色池实体
/// </summary>
public class Role  {

    /// <summary>
    /// 角色ID
    /// </summary>
    public string RoleID { get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }
    /// <summary>
    /// 别称
    /// </summary>
    public string Nickname { get; set; }
    /// <summary>
    /// 耐久
    /// </summary>
    public int Durable { get; set; }
    /// <summary>
    /// 炮击
    /// </summary>
    public int TheShelling { get; set; }
    /// <summary>
    /// 防空
    /// </summary>
    public int AirDefense { get; set; }
    /// <summary>
    /// 反潜
    /// </summary>
    public int AntiSubmarine { get; set; }
    /// <summary>
    /// 装甲类型
    /// </summary>
    public int Armor { get; set; }
    /// <summary>
    /// 雷击
    /// </summary>
    public int TheTorpedo { get; set; }
    /// <summary>
    /// 航空
    /// </summary>
    public int Aviation { get; set; }
    /// <summary>
    /// 装填
    /// </summary>
    public int Loading { get; set; }
    /// <summary>
    /// 机动
    /// </summary>
    public int Motor { get; set; }
    /// <summary>
    /// 消耗
    /// </summary>
    public int Consumption { get; set; }
    /// <summary>
    /// 品质
    /// </summary>
    public int Quality { get; set; }
    /// <summary>
    /// 阵营
    /// </summary>
    public int Camp { get; set; }
    /// <summary>
    /// 位置
    /// </summary>
    public int Position { get; set; }
    /// <summary>
    /// 技能栏ID
    /// </summary>
    public string SkillbarID { get; set; }
    
}
