using System;
using UnityEngine;

public class ManualMonoSingleton<T> : MonoBehaviour where T : ManualMonoSingleton<T>
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null) throw new NullReferenceException("无引用，需要在Set后再调用");
			return instance;
		}
	}

	public static void SetInstance(T newInstance)
	{
		if (newInstance == null || instance != null) return;
		instance = newInstance;
	}
}