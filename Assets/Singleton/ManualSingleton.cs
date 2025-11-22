using System;

public abstract class ManualSingleton<T> where T : ManualSingleton<T>,new()
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if(instance == null) throw new NullReferenceException("无引用，需要在Set后再调用");
			return instance;
		}
	}

	public static void SetInstance(T newInstance)
	{
		if (instance != null || newInstance == null) return;
		instance = newInstance;
	}
}