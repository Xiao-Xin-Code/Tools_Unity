using System;
using QMVC;
using UnityEngine;

public interface IFactory : IUtility
{

}



public class ItemBaseFactory : IFactory
{

	public ItemBaseController GetPrefab(Type type)
	{
		return type switch
		{
			Type t when t == typeof(DefaultNodeFolder)=>Resources.Load<ItemBaseController>("Folder"),
			Type t when t == typeof(DefaultNodeFile)=>Resources.Load<ItemBaseController>("File"),
			_ => throw new NotImplementedException(type.FullName)
		};
	}

	public ItemBaseController GetPrefab<T>() where T : ItemBaseController
	{
		return typeof(T) switch
		{
			Type t when t == typeof(DefaultNodeFolder) => Resources.Load<ItemBaseController>(""),
			Type t when t == typeof(DefaultNodeFile) => Resources.Load<ItemBaseController>(""),
			_ => throw new NotImplementedException()
		};
	}
}