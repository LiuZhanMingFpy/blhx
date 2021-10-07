using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色拥有的技能表
/// </summary>
public class HaveSkill
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public string SkillID { get; set; }
    /// <summary>
    /// 角色ID
    /// </summary>
    public string RoleID { get; set; }
    /// <summary>
    /// 技能名字
    /// </summary>
    public string SkillName { get; set; }
    /// <summary>
    /// 技能效果
    /// </summary>
    public int SkillEffect { get; set; }
    /// <summary>
    /// 附加数值
    /// </summary>
    public int AdditionalValue { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 别名
    /// </summary>
    public string Nickname { get; set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

}
