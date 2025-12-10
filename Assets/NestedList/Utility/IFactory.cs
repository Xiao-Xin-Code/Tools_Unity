using System;
using QMVC;

public interface IFactory : IUtility
{

}



public class ItemBaseFactory : IFactory
{

	public ItemBaseController GetPrefab(Type type)
	{
		return type switch
		{
			_ => throw new NotImplementedException()
		};
	}

	public ItemBaseController GetPrefab<T>() where T : ItemBaseController
	{
		return typeof(T) switch
		{
			_ => throw new NotImplementedException()
		};
	}
}