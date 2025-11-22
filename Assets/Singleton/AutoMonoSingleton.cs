using UnityEngine;

public class AutoMonoSingleton<T> : MonoBehaviour where T:AutoMonoSingleton<T>,new()
{

	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = new GameObject(typeof(T).Name).AddComponent<T>();
			} 
			return instance;
		}
	}

}