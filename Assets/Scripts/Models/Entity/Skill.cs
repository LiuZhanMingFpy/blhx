using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能池实体
/// </summary>
public class Skill 
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public string SkillID { get; set; }
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }
    /// <summary>
    /// 技能效果
    /// </summary>
    public string SkillEffect { get; set; }
    /// <summary>
    /// 附加数值
    /// </summary>
    public string AdditionalValue { get; set; }
    /// <summary>
    /// 技能说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 别名
    /// </summary>
    public string Nickname { get; set; }

}
