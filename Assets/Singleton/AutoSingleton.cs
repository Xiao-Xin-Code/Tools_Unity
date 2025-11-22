using System.Collections;
using UnityEngine;

public abstract class AutoSingleton<T> where T : AutoSingleton<T>,new()
{

	private static volatile T instance;//防止指令重排序，
	private static readonly object _lock = new object();

	public static T Instance
	{
		get
		{
			if(instance == null)
			{
				lock (_lock)
				{
					if(instance == null)
					{
						instance = new();
					}
				}
			}
			return instance;
		}
	}
}