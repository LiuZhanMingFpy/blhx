/*
 说明：状态系统  相当于动画逻辑的状态机
 作者：
 时间：
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态ID
/// </summary>
public enum EMStateIDType {
    NullStateID,//空
    PatrolStateID,//巡逻状态
    ChaseStateID,//追逐状态

}

/// <summary>
/// 转换条件
/// </summary>
public enum EMTransitionType {
    SeePlayer,//看到主角
    ArriveEnd,//到达终点
}


/// <summary>
/// 状态机
/// </summary>
public class StateMachine  {

    public EMStateIDType m_CurrentStateID;//当前状态的ID
    public BaseState m_CurrentState; //当前的状态

    #region 字典  某状态的ID  ——某状态
    public Dictionary<EMStateIDType, BaseState> StateDic = new Dictionary<EMStateIDType, BaseState>();
    
    /// <summary>
    /// 通过ID获取对应状态
    /// </summary>
    /// <param name="stateId"></param>
    /// <returns></returns>
    public BaseState GetStateByID(EMStateIDType stateId)
    {
        if (!StateDic.ContainsKey(stateId))
        {
            return null;
        }
        return StateDic[stateId];
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="stateId"></param>
    /// <param name="state"></param>
    public void AddState(EMStateIDType stateId,BaseState state)
    {
        if (StateDic.ContainsKey(stateId)|| stateId == EMStateIDType.NullStateID)
        {
            return;
        }
        if (m_CurrentState == null)//第一个添加的状态被作为运行的默认状态
        {
            m_CurrentStateID = stateId;
            m_CurrentState = state;
            m_CurrentState.StateStart();
        }
        StateDic.Add(stateId, state);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="state"></param>
    public void DeleState(BaseState state)
    {
        if (state == null||!StateDic.ContainsValue(state))
        {
            return;
        }
        StateDic.Remove(state.m_StateID);
    }
    #endregion

    /// <summary>
    /// 每帧刷新状态机
    /// </summary>
    public void UpdateMachine()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.StateUpdate();
            m_CurrentState.Transition();
        }
    }

    /// <summary>
    /// 通过某条件转换到其它状态
    /// </summary>
    /// <param name="transition"></param>
    public void TranslateToOtherState(EMTransitionType transition)//通过此条件转换到某状态
    {
        //通过条件知道要转换的状态
        EMStateIDType stateId = m_CurrentState.GetStateIDByTransition(transition);
        if (stateId!=EMStateIDType.NullStateID)
        {
            //当前状态结束
            m_CurrentState.StateEnd();

            //新的状态开始
            m_CurrentStateID = stateId;
            m_CurrentState = StateDic[stateId];
            m_CurrentState.StateStart();
        }
    }

}
