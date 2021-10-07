/*
 说明：状态的基础模版类   描述每个状态公共的属性、方法  等
 作者：
 时间：

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState{

    public EMStateIDType m_StateID;
    public StateMachine m_Machine;

    /// <summary>
    /// 构造函数  每个状态都有的ID，所属哪个状态机
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="stateID"></param>
    public BaseState(StateMachine machine,EMStateIDType stateID)
    {
        m_StateID = stateID;
        m_Machine = machine;
    }

    #region 抽象出来的行为
    public abstract void StateStart();//状态开始
    public abstract void StateUpdate();//状态持续中
    public abstract void StateEnd();//状态结束
    public abstract void Transition();//转换到其它状态的条件

    #endregion

    #region 字典   转换到某状态的条件——某状态ID
    public Dictionary<EMTransitionType, EMStateIDType> StateIdDic = new Dictionary<EMTransitionType, EMStateIDType>();
    
    /// <summary>
    /// 添加转换到某个状态的条件，
    /// </summary>
    /// <param name="transition"></param>
    /// <param name="stateId"></param>
    public void AddTransition(EMTransitionType transition,EMStateIDType stateId)
    {
        if (StateIdDic.ContainsKey(transition))
        {
            return;
        }
        StateIdDic.Add(transition, stateId);
    }
    
    /// <summary>
    /// 删除转换到某个状态的条件
    /// </summary>
    /// <param name="transition"></param>
    public void DeleteTransition(EMTransitionType transition)
    {
        if (!StateIdDic.ContainsKey(transition) )
        {
            return;
        }
        StateIdDic.Remove(transition);
    }

    /// <summary>
    /// 通过转换条件，获取某个状态ID（也就可以获取某个状态）
    /// </summary>
    /// <param name="transition"></param>
    /// <returns></returns>
    public EMStateIDType GetStateIDByTransition(EMTransitionType transition)
    {
        if (!StateIdDic.ContainsKey(transition))
        {
            return EMStateIDType.NullStateID;
        }
        return StateIdDic[transition];
    }
    #endregion

}
