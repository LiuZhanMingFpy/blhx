
using System;
using System.Collections.Generic;
using UnityEngine;
 
static public class EventCenter {

	static public Dictionary<EGameEvent, Delegate> mEventTable = new Dictionary<EGameEvent, Delegate>();

	static public void Cleanup()
	{
		List< EGameEvent > messagesToRemove = new List<EGameEvent>();
		foreach (KeyValuePair<EGameEvent, Delegate> pair in mEventTable) {
				messagesToRemove.Add( pair.Key );
		}
		foreach (EGameEvent message in messagesToRemove) {
			mEventTable.Remove( message );
		}
	}

    static public void OnListenerAdding(EGameEvent eventType, Delegate listenerBeingAdded) {

        if (!mEventTable.ContainsKey(eventType)) {
            mEventTable.Add(eventType, null );
        }
 
        Delegate d = mEventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType()) {
            throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }
 
    static public void OnListenerRemoving(EGameEvent eventType, Delegate listenerBeingRemoved) {

        if (mEventTable.ContainsKey(eventType)) {
            Delegate d = mEventTable[eventType];
 
            if (d == null) {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
            } else if (d.GetType() != listenerBeingRemoved.GetType()) {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
        } else {
            throw new ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
        }
    }
 
    static public void OnListenerRemoved(EGameEvent eventType) {
        if (mEventTable[eventType] == null) {
            mEventTable.Remove(eventType);
        }
    }
 
    static public void OnBroadcasting(EGameEvent eventType) {
#if REQUIRE_LISTENER
        if (!mEventTable.ContainsKey(eventType)) {
        }
#endif
    }
 
    static public BroadcastException CreateBroadcastSignatureException(EGameEvent eventType) {
        return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
    }
 
    public class BroadcastException : Exception {
        public BroadcastException(string msg)
            : base(msg) {
        }
    }
 
    public class ListenerException : Exception {
        public ListenerException(string msg)
            : base(msg) {
        }
    }
 
	//No parameters
    static public void AddListener(EGameEvent eventType, Action handler) {
        OnListenerAdding(eventType, handler);
        mEventTable[eventType] = (Action)mEventTable[eventType] + handler;
    }
 
	//Single parameter
	static public void AddListener<T>(EGameEvent eventType, Action<T> handler) {
        OnListenerAdding(eventType, handler);
        mEventTable[eventType] = (Action<T>)mEventTable[eventType] + handler;
    }
 
	//Two parameters
	static public void AddListener<T, U>(EGameEvent eventType, Action<T, U> handler) {
        OnListenerAdding(eventType, handler);
        mEventTable[eventType] = (Action<T, U>)mEventTable[eventType] + handler;
    }//添加监听者
 
	//Three parameters
	static public void AddListener<T, U, V>(EGameEvent eventType, Action<T, U, V> handler) {
        OnListenerAdding(eventType, handler);
        mEventTable[eventType] = (Action<T, U, V>)mEventTable[eventType] + handler;
    }
	
	//Four parameters
	static public void AddListener<T, U, V, X>(EGameEvent eventType, Action<T, U, V, X> handler) {
        OnListenerAdding(eventType, handler);
        mEventTable[eventType] = (Action<T, U, V, X>)mEventTable[eventType] + handler;
    }

	//No parameters
    static public void RemoveListener(EGameEvent eventType, Action handler) {
        OnListenerRemoving(eventType, handler);   
        mEventTable[eventType] = (Action)mEventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
 
	//Single parameter
	static public void RemoveListener<T>(EGameEvent eventType, Action<T> handler) {
        OnListenerRemoving(eventType, handler);
        mEventTable[eventType] = (Action<T>)mEventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
 
	//Two parameters
	static public void RemoveListener<T, U>(EGameEvent eventType, Action<T, U> handler) {
        OnListenerRemoving(eventType, handler);
        mEventTable[eventType] = (Action<T, U>)mEventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
 
	//Three parameters
	static public void RemoveListener<T, U, V>(EGameEvent eventType, Action<T, U, V> handler) {
        OnListenerRemoving(eventType, handler);
        mEventTable[eventType] = (Action<T, U, V>)mEventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
	
	//Four parameters
	static public void RemoveListener<T, U, V, X>(EGameEvent eventType, Action<T, U, V, X> handler) {
        OnListenerRemoving(eventType, handler);
        mEventTable[eventType] = (Action<T, U, V, X>)mEventTable[eventType] - handler;
        OnListenerRemoved(eventType);
    }
	

	//No parameters
    static public void Broadcast(EGameEvent eventType) {

        OnBroadcasting(eventType);
 
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d)) {
            Action Action = d as Action;
 
            if (Action != null) {
                Action();
            } else {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }//发布事件

 
	//Single parameter
    static public void Broadcast<T>(EGameEvent eventType, T arg1) {

        OnBroadcasting(eventType);
 
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d)) {
            Action<T> Action = d as Action<T>;
 
            if (Action != null) {
                Action(arg1);
            } else {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
	}
 
	//Two parameters
    static public void Broadcast<T, U>(EGameEvent eventType, T arg1, U arg2) {

        OnBroadcasting(eventType);
 
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d)) {
            Action<T, U> Action = d as Action<T, U>;
 
            if (Action != null) {
                Action(arg1, arg2);
            } else {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }
 
	//Three parameters
    static public void Broadcast<T, U, V>(EGameEvent eventType, T arg1, U arg2, V arg3) {

        OnBroadcasting(eventType);
 
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d)) {
            Action<T, U, V> Action = d as Action<T, U, V>;
 
            if (Action != null) {
                Action(arg1, arg2, arg3);
            } else {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }
	
	//Four parameters
    static public void Broadcast<T, U, V, X>(EGameEvent eventType, T arg1, U arg2, V arg3, X arg4) {

        OnBroadcasting(eventType);
 
        Delegate d;
        if (mEventTable.TryGetValue(eventType, out d)) {
            Action<T, U, V, X> Action = d as Action<T, U, V, X>;
 
            if (Action != null) {
                Action(arg1, arg2, arg3, arg4);
            } else {
                throw CreateBroadcastSignatureException(eventType);
            }
        }
    }	
}
