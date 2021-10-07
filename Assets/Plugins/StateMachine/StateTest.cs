/*
状态模式的实现范例

描述：让一个对象的行为随着内部状态的改变而变化，而该对象也像是换了类一样
      当某个对象，状态改变时，虽然它“表现行为”会有所变化，但是对客户端来说，并不会因为这样的变化，而改变它的“操作方法”或“信息沟通”的方式。
      也就是说，这个对象与外界的对应方式不会有任何改变。但是，对象内部确实是会通过“更换状态类对象”的方式来进行状态的转换。
      当状态对象更换到另一种类时，对象就会通过新的状态类，表现出它在这个状态下该有的行为。但这一切只会发生在对象内部，对于客户端，完全不需要了解状态转换过程及对应方式

状态拥有者：
是一个具有状态属性的类，可以制定相关的接口，让外界能够得知状态的改变或通过操作让状态改变
是状态属性的类，例如：游戏角色有潜行、攻击、施法等状态;好友上线、脱机、忙碌等状态;

状态接口类：基类
制定状态的接口，负责规范状态拥有者在特定状态下要表现的行为

具体状态类：派生类
继承自状态接口类
实现状态拥有者在特定状态下该有的行为，例如：实现角色在潜行状态时该有的行动变缓、3D模型变半透明、不能被敌方角色察觉等行为
*/

using UnityEngine;

/// <summary>
/// 状态的拥有者 
/// </summary>
/*
拥有一个State属性用来描述当前的状态，外界可以通过Request方法，让Context类呈现当前状态下的行为。
SetState方法可以指定Context类当前的状态
*/
public class Context  {
    State m_State = null;//某状态
    public void Request(int value)
    {
        m_State.Handle(value);
    }
    public void SetState(State theState)
    {
        Debug.Log("Context.SetState:"+theState);
        m_State = theState;
    }
}

/// <summary>
/// 状态的抽象类
/// </summary>
///State状态接口用来定义每一个状态该有的行为
public abstract class State {

    protected Context m_Context = null;
    public State(Context theContext)//构造函数，知道状态的拥有者是谁
    {
        m_Context = theContext;
    }
    public abstract void Handle(int Value);
}

/// <summary>
/// 状态A
/// </summary>
public class ConcreateStateA : State
{
    public ConcreateStateA(Context theContext) : base(theContext) { }
    public override void Handle(int Value)
    {
        Debug.Log("ConcreateStateA.Handle");
        if (Value>10)
        {
            m_Context.SetState(new ConcreateStateB(m_Context));
        }
    }
}

/// <summary>
/// 状态B
/// </summary>
public class ConcreateStateB : State
{
    public ConcreateStateB(Context theContext) : base(theContext) { }

    public override void Handle(int Value)
    {
        Debug.Log("ConcreateStateB.Handle");
        if (Value > 20)
        {
            m_Context.SetState(new ConcreateStateC(m_Context));
        }
    }
}

/// <summary>
/// 状态C
/// </summary>
public class ConcreateStateC : State
{
    public ConcreateStateC(Context theContext) : base(theContext) { }

    public override void Handle(int Value)
    {
        Debug.Log("ConcreateStateC.Handle");
        if (Value > 30)
        {
            m_Context.SetState(new ConcreateStateA(m_Context));
        }
    }
}

/// <summary>
/// 测试状态类
/// </summary>
public class StateTest:MonoBehaviour {
    void Start()
    {
        Context theContext = new Context();
        theContext.SetState(new ConcreateStateA(theContext));//默认首先到A状态
        theContext.Request(5);//还是在A状态，条件不成立，没有切换状态
        theContext.Request(15);// >10条件成立，切换到B状态
        theContext.Request(25);// >20条件成立，切换到C状态
        theContext.Request(35);// >35条件成立，切换到A状态
    }
}

