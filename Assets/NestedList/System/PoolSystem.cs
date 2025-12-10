using System;
using System.Collections.Generic;
using QMVC;
using UnityEngine;

public class PoolSystem : AbstractSystem
{

	Dictionary<Type, MonoPool<ItemBaseController>> pools = new Dictionary<Type, MonoPool<ItemBaseController>>();

	public void AddPool<T>(ItemBaseController prefab) where T : ItemBaseController
	{
		Type type = typeof(T);
		if (!pools.ContainsKey(type))
		{
			Transform parent = new GameObject(type.FullName).transform;
			parent.SetParent(root);
			pools.Add(type, new MonoPool<ItemBaseController>(prefab, parent));
		}
	}

	public MonoPool<ItemBaseController> GetPool<T>() where T : ItemBaseController
	{
		Type type = typeof(T);
		if (pools.ContainsKey(type))
		{
			return pools[type];
		}
		else
		{
			return null;
		}
	}

	Transform root;

	protected override void OnInit()
	{
		root = new GameObject("PoolRoot").transform;
	}
}