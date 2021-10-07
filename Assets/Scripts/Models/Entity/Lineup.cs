using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 阵容实体
/// </summary>
public class Lineup
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string UserID { get; set; }
    /// <summary>
    /// 阵容ID
    /// </summary>
    public string LineupID { get; set; }
    /// <summary>
    /// 前排 上
    /// </summary>
    public string ForwardFront { get; set; }
    /// <summary>
    /// 前排中
    /// </summary>
    public string ForwardCenter { get; set; }
    /// <summary>
    /// 前排下
    /// </summary>
    public string ForwardGuard { get; set; }
    /// <summary>
    /// 后排上
    /// </summary>
    public string BehindFront { get; set; }
    /// <summary>
    /// 后排中
    /// </summary>
    public string BehindCenter { get; set; }
    /// <summary>
    /// 后排下
    /// </summary>
    public string BehindGuard { get; set; }
}
