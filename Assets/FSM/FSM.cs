using System;
using System.Collections.Generic;

public interface IFSMState
{
	void Enter();
	void LogicalUpdate();
	void Exit();
}


public class FSM<T> where T : IFSMState
{
	public Dictionary<Type,T> StateTable { get; protected set; }
	protected T curState;


	public FSM()
	{
		StateTable = new Dictionary<Type, T>();
		curState = default;
	}

	public void AddState(T state)
	{
		Type type = state.GetType();
		if (StateTable.ContainsKey(type))
		{
			StateTable[type] = state;
		}
		else
		{
			StateTable.Add(state.GetType(), state);
		}
	}

	public void StartState(Type startState)
	{
		if (StateTable.ContainsKey(startState))
		{
			curState = StateTable[startState];
			curState.Enter();
		}
		else
		{
			throw new KeyNotFoundException($"开始_不存在的Key：{startState}");
		}
	}

	public void ChangeState(Type state)
	{
		if (StateTable.ContainsKey(state))
		{
			curState.Exit();
			curState = StateTable[state];
			curState.Enter();
		}
		else
		{
			throw new KeyNotFoundException($"切换_不存在的Key：{state}");
		}
	}

	public void OnUpdate()
	{
		curState.LogicalUpdate();
	}
}